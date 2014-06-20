namespace SendKeys
{
    partial class SendKeysToApps
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.btnSendKeys = new System.Windows.Forms.Button();
            this.cboWindows = new System.Windows.Forms.ComboBox();
            this.rbManual = new System.Windows.Forms.RadioButton();
            this.rbAuto = new System.Windows.Forms.RadioButton();
            this.lnkRefresh = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(32, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Windows Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(35, 41);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(502, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // btnSendKeys
            // 
            this.btnSendKeys.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnSendKeys.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSendKeys.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnSendKeys.ForeColor = System.Drawing.Color.Blue;
            this.btnSendKeys.Location = new System.Drawing.Point(543, 41);
            this.btnSendKeys.Name = "btnSendKeys";
            this.btnSendKeys.Size = new System.Drawing.Size(99, 20);
            this.btnSendKeys.TabIndex = 2;
            this.btnSendKeys.Text = "Send Keys";
            this.btnSendKeys.UseVisualStyleBackColor = false;
            this.btnSendKeys.Click += new System.EventHandler(this.btnSendKeys_Click);
            // 
            // cboWindows
            // 
            this.cboWindows.FormattingEnabled = true;
            this.cboWindows.Location = new System.Drawing.Point(35, 41);
            this.cboWindows.Name = "cboWindows";
            this.cboWindows.Size = new System.Drawing.Size(502, 21);
            this.cboWindows.TabIndex = 13;
            this.cboWindows.SelectedIndexChanged += new System.EventHandler(this.OptionSelection);
            // 
            // rbManual
            // 
            this.rbManual.AutoSize = true;
            this.rbManual.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.rbManual.Location = new System.Drawing.Point(312, 18);
            this.rbManual.Name = "rbManual";
            this.rbManual.Size = new System.Drawing.Size(72, 18);
            this.rbManual.TabIndex = 14;
            this.rbManual.Text = "Manual";
            this.rbManual.UseVisualStyleBackColor = true;
            this.rbManual.Click += new System.EventHandler(this.OptionSelection);
            // 
            // rbAuto
            // 
            this.rbAuto.AutoSize = true;
            this.rbAuto.Checked = true;
            this.rbAuto.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.rbAuto.Location = new System.Drawing.Point(209, 18);
            this.rbAuto.Name = "rbAuto";
            this.rbAuto.Size = new System.Drawing.Size(55, 18);
            this.rbAuto.TabIndex = 15;
            this.rbAuto.TabStop = true;
            this.rbAuto.Text = "Auto";
            this.rbAuto.UseVisualStyleBackColor = true;
            this.rbAuto.Click += new System.EventHandler(this.OptionSelection);
            // 
            // lnkRefresh
            // 
            this.lnkRefresh.AutoSize = true;
            this.lnkRefresh.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lnkRefresh.Location = new System.Drawing.Point(479, 22);
            this.lnkRefresh.Name = "lnkRefresh";
            this.lnkRefresh.Size = new System.Drawing.Size(58, 14);
            this.lnkRefresh.TabIndex = 22;
            this.lnkRefresh.TabStop = true;
            this.lnkRefresh.Text = "Refresh";
            this.lnkRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRefresh_LinkClicked);
            // 
            // SendKeysToApps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 94);
            this.Controls.Add(this.lnkRefresh);
            this.Controls.Add(this.rbAuto);
            this.Controls.Add(this.rbManual);
            this.Controls.Add(this.cboWindows);
            this.Controls.Add(this.btnSendKeys);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SendKeysToApps";
            this.Text = "Send Keys to Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Button btnSendKeys;
        private System.Windows.Forms.ComboBox cboWindows;
        private System.Windows.Forms.RadioButton rbManual;
        private System.Windows.Forms.RadioButton rbAuto;
        private System.Windows.Forms.LinkLabel lnkRefresh;
    }
}

