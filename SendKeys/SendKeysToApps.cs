using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Meebey.SmartIrc4net;
using Microsoft.Test.Input;

namespace SendKeys
{
    public partial class SendKeysToApps : Form
    {
        private readonly IrcClient c = new IrcClient();

        private bool iddle = true;
        private int timerInterval = 5;


        /// <summary>
        ///     Default constructor
        /// </summary>
        public SendKeysToApps()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Send keystrokes to application after finding it with its windows title and activating it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendKeys_Click(object sender, EventArgs e)
        {
            c.ActiveChannelSyncing = true;
            c.OnConnected += c_OnConnected;
            c.Connect("irc.twitch.tv", 6667);
        }

        private void c_OnConnected(object sender, EventArgs e)
        {
            c.Login(new[] {"rcarubbi"}, "rcarubbi", 0, "rcarubbi", "oauth:jj47a9vg0cwb5s70lgo5roxxrqj53so");

            c.RfcJoin("#rcarubbi");


            c.OnChannelMessage += c_OnChannelMessage;
            var t = new Thread(Listen);

            t.Start();
        }


        private void Listen()
        {
            c.Listen(true);
        }

        private void c_OnChannelMessage(object sender, IrcEventArgs e)
        {
            SendKey(e.Data.Message);
        }


        private void SendKey(string text)
        {
            var iHandle = NativeWin32.FindWindow(null, txtTitle.Text);
            NativeWin32.SetForegroundWindow(iHandle);
            var keys = new List<KeyValuePair<Key, byte>>();

            keys = MapCommandToKey(text);

            Thread.Sleep(200);
            foreach (var key in keys)
            {
                switch (key.Value)
                {
                    case 0: // press
                        Keyboard.Press(key.Key);
                        Thread.Sleep(200);
                        Keyboard.Release(key.Key);
                        break;
                    case 1: // hold
                        Keyboard.Press(key.Key);
                        break;
                    case 2: // release
                        Keyboard.Release(key.Key);
                        break;
                }

                Thread.Sleep(200);
            }
        }


        private List<KeyValuePair<Key, byte>> MapCommandToKey(string modeAndCommand)
        {
            modeAndCommand = modeAndCommand.Trim().ToUpper();
            var keys = new List<KeyValuePair<Key, byte>>();
            var commands = modeAndCommand.Split('+');

            foreach (var c in commands)
            {
                var command = string.Empty;
                var modeCommand = string.Empty;
                var words = c.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

                if (words.Length == 1)
                {
                    modeCommand = "PRESS";
                    command = words[0];
                }
                else if (words.Length == 2)
                {
                    modeCommand = words[0];
                    command = words[1];
                }
                else
                {
                    break;
                }

                var mode = ParseMode(modeCommand);

                var key = ParseKey(command);
                keys.Add(new KeyValuePair<Key, byte>(key, mode));
            }

            return keys;
        }

        private Key ParseKey(string command)
        {
            var result = Key.Space;

            if (command == "ACTION")
                result = Key.Space;
            else if (command == "RESTART")
                result = Key.F2;
            else if (command == "LEFT")
                result = Key.Left;
            else if (command == "RIGHT")
                result = Key.Right;
            else if (command == "UP")
                result = Key.Up;
            else if (command == "DOWN") result = Key.Down;


            return result;
        }

        private byte ParseMode(string modeCommand)
        {
            byte result = 0;


            if (modeCommand == "HOLD")
                result = 1;
            else if (modeCommand == "RELEASE") result = 2;


            return result;
        }


        /// <summary>
        ///     Refresh the combobox list with all the top level windows running on desktop.
        /// </summary>
        private void RefreshWindows()
        {
            cboWindows.Items.Clear();
            GetTaskWindows();
        }

        /// <summary>
        ///     Allows combobox and textbox switching on selection of Auto and Manual.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionSelection(object sender, EventArgs e)
        {
            if (rbAuto.Checked)
            {
                cboWindows.Visible = true;
                txtTitle.Text = cboWindows.Text;
                txtTitle.Visible = false;
            }
            else
            {
                cboWindows.Visible = false;
                txtTitle.Text = cboWindows.Text;
                txtTitle.Visible = true;
            }
        }


        /// <summary>
        ///     Refill the combobox with the currently running top level windows applications.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RefreshWindows();
        }

        /// <summary>
        ///     Get all the top level visible windows
        /// </summary>
        private void GetTaskWindows()
        {
            // Get the desktopwindow handle
            var nDeshWndHandle = NativeWin32.GetDesktopWindow();
            // Get the first child window
            var nChildHandle = NativeWin32.GetWindow(nDeshWndHandle, NativeWin32.GW_CHILD);

            while (nChildHandle != 0)
            {
                //If the child window is this (SendKeys) application then ignore it.
                if (nChildHandle == Handle.ToInt32())
                    nChildHandle = NativeWin32.GetWindow(nChildHandle, NativeWin32.GW_HWNDNEXT);

                // Get only visible windows
                if (NativeWin32.IsWindowVisible(nChildHandle) != 0)
                {
                    var sbTitle = new StringBuilder(1024);
                    // Read the Title bar text on the windows to put in combobox
                    NativeWin32.GetWindowText(nChildHandle, sbTitle, sbTitle.Capacity);
                    var sWinTitle = sbTitle.ToString();
                    {
                        if (sWinTitle.Length > 0) cboWindows.Items.Add(sWinTitle);
                    }
                }

                // Look for the next child.
                nChildHandle = NativeWin32.GetWindow(nChildHandle, NativeWin32.GW_HWNDNEXT);
            }
        }
    }
}