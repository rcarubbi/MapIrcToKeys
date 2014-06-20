using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace SendKeys
{
    public partial class SendKeysToApps : Form 
    {


        /// <summary>
        /// Default constructor
        /// </summary>
        public SendKeysToApps()
        {
            InitializeComponent();
          
        }

        private bool iddle = true;
        private int timerInterval = 5;
        /// <summary>
        /// Send keystrokes to application after finding it with its windows title and activating it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendKeys_Click(object sender, EventArgs e)
        {
         
            c.ActiveChannelSyncing = true;
            c.OnConnected += c_OnConnected;
            c.Connect("irc.twitch.tv", 6667);
 
        }

        void c_OnConnected(object sender, EventArgs e)
        {
 
            c.Login(new string[] { "rcarubbi" }, "rcarubbi", 0, "rcarubbi", "oauth:jj47a9vg0cwb5s70lgo5roxxrqj53so");

            c.RfcJoin("#rcarubbi");


            c.OnChannelMessage += c_OnChannelMessage;
            Thread t = new Thread(new ThreadStart(Listen));

            t.Start();
 
        }


        void Listen()
        {
            c.Listen(true);
        }

        void c_OnChannelMessage(object sender, Meebey.SmartIrc4net.IrcEventArgs e)
        {
            SendKey(e.Data.Message);
        }

        Meebey.SmartIrc4net.IrcClient c = new Meebey.SmartIrc4net.IrcClient();



        private void SendKey(string text)
        {
            int iHandle = NativeWin32.FindWindow(null, txtTitle.Text);
            NativeWin32.SetForegroundWindow(iHandle);
            List<KeyValuePair<Microsoft.Test.Input.Key, byte>> keys = new List<KeyValuePair<Microsoft.Test.Input.Key, byte>>();

            keys = MapCommandToKey(text);

            Thread.Sleep(200);
            foreach (var key in keys)
            {
                switch (key.Value)
                {
                    case 0: // press
                        Microsoft.Test.Input.Keyboard.Press(key.Key);
                        Thread.Sleep(200);
                        Microsoft.Test.Input.Keyboard.Release(key.Key);
                        break;
                    case 1: // hold
                        Microsoft.Test.Input.Keyboard.Press(key.Key);
                        break;
                    case 2: // release
                        Microsoft.Test.Input.Keyboard.Release(key.Key);
                        break;

                }
                Thread.Sleep(200);

            }
        }

 

        private List<KeyValuePair<Microsoft.Test.Input.Key, byte>> MapCommandToKey(string modeAndCommand)
        {
            modeAndCommand = modeAndCommand.Trim().ToUpper();
            List<KeyValuePair<Microsoft.Test.Input.Key, byte>> keys = new List<KeyValuePair<Microsoft.Test.Input.Key, byte>>();
            var commands = modeAndCommand.Split('+');

            foreach (var c in commands)
            {
                var command = string.Empty;
                var modeCommand = string.Empty;
                var words = c.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

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
                    break;

                byte mode = ParseMode(modeCommand);

                Microsoft.Test.Input.Key key = ParseKey(command);
                keys.Add(new KeyValuePair<Microsoft.Test.Input.Key, byte>(key, mode));
            }

            return keys;

        }

        private Microsoft.Test.Input.Key ParseKey(string command)
        {
            Microsoft.Test.Input.Key result = Microsoft.Test.Input.Key.Space;

            if (command == "ACTION")
            {
                result = Microsoft.Test.Input.Key.Space;
            }
            else if (command == "RESTART")
            {
                result = Microsoft.Test.Input.Key.F2;
            }
            else if (command == "LEFT")
            {
                result = Microsoft.Test.Input.Key.Left;
            }
            else if (command == "RIGHT")
            {
                result = Microsoft.Test.Input.Key.Right;
            }
            else if (command == "UP")
            {
                result = Microsoft.Test.Input.Key.Up;
            }
            else if (command == "DOWN")
            {
                result = Microsoft.Test.Input.Key.Down;
            }


            return result;
        }

        private byte ParseMode(string modeCommand)
        {
            byte result = 0;


            if (modeCommand == "HOLD")
            {
                result = 1;
            }
            else if (modeCommand == "RELEASE")
            {
                result = 2;

            }


            return result;
        }




        /// <summary>
        /// Refresh the combobox list with all the top level windows running on desktop.
        /// </summary>
        private void RefreshWindows()
        {
            cboWindows.Items.Clear();
            GetTaskWindows();
        }

        /// <summary>
        /// Allows combobox and textbox switching on selection of Auto and Manual.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionSelection(object sender, EventArgs e)
        {
            if (rbAuto.Checked == true)
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
        /// Refill the combobox with the currently running top level windows applications.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RefreshWindows();
        }
        /// <summary>
        /// Get all the top level visible windows
        /// </summary>
        private void GetTaskWindows()
        {
            // Get the desktopwindow handle
            int nDeshWndHandle = NativeWin32.GetDesktopWindow();
            // Get the first child window
            int nChildHandle = NativeWin32.GetWindow(nDeshWndHandle, NativeWin32.GW_CHILD);

            while (nChildHandle != 0)
            {
                //If the child window is this (SendKeys) application then ignore it.
                if (nChildHandle == this.Handle.ToInt32())
                {
                    nChildHandle = NativeWin32.GetWindow(nChildHandle, NativeWin32.GW_HWNDNEXT);
                }

                // Get only visible windows
                if (NativeWin32.IsWindowVisible(nChildHandle) != 0)
                {
                    StringBuilder sbTitle = new StringBuilder(1024);
                    // Read the Title bar text on the windows to put in combobox
                    NativeWin32.GetWindowText(nChildHandle, sbTitle, sbTitle.Capacity);
                    String sWinTitle = sbTitle.ToString();
                    {
                        if (sWinTitle.Length > 0)
                        {
                            cboWindows.Items.Add(sWinTitle);
                        }
                    }
                }
                // Look for the next child.
                nChildHandle = NativeWin32.GetWindow(nChildHandle, NativeWin32.GW_HWNDNEXT);
            }
        }


       
    }
}