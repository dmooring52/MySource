// ------------------------------------------------------------------------------
// Copyright (c) 2014 Microsoft Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
// ------------------------------------------------------------------------------

namespace Microsoft.Live.Validator
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.signinButton = new System.Windows.Forms.Button();
            this.signOutWebBrowser = new System.Windows.Forms.WebBrowser();
            this.mePictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.meNameLabel = new System.Windows.Forms.Label();
            this.signOutButton = new System.Windows.Forms.Button();
            this.connectGroupBox = new System.Windows.Forms.GroupBox();
            this.richTextStatus = new System.Windows.Forms.RichTextBox();
            this.btnValidateMP3 = new System.Windows.Forms.Button();
            this.txtExcel = new System.Windows.Forms.TextBox();
            this.lblOneDrive = new System.Windows.Forms.Label();
            this.txtOneDriveRoot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnValidateMP3Files = new System.Windows.Forms.Button();
            this.btnValidateZipFiles = new System.Windows.Forms.Button();
            this.btnZIPLink = new System.Windows.Forms.Button();
            this.SyncTimer = new System.Windows.Forms.Timer(this.components);
            this.btnStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mePictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.connectGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // signinButton
            // 
            this.signinButton.Location = new System.Drawing.Point(147, 22);
            this.signinButton.Name = "signinButton";
            this.signinButton.Size = new System.Drawing.Size(108, 23);
            this.signinButton.TabIndex = 0;
            this.signinButton.Text = "Sign in/Authorize";
            this.signinButton.UseVisualStyleBackColor = true;
            this.signinButton.Click += new System.EventHandler(this.SigninButton_Click);
            // 
            // signOutWebBrowser
            // 
            this.signOutWebBrowser.Location = new System.Drawing.Point(647, 22);
            this.signOutWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.signOutWebBrowser.Name = "signOutWebBrowser";
            this.signOutWebBrowser.Size = new System.Drawing.Size(26, 25);
            this.signOutWebBrowser.TabIndex = 1;
            this.signOutWebBrowser.Visible = false;
            // 
            // mePictureBox
            // 
            this.mePictureBox.Location = new System.Drawing.Point(10, 43);
            this.mePictureBox.Name = "mePictureBox";
            this.mePictureBox.Size = new System.Drawing.Size(72, 72);
            this.mePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mePictureBox.TabIndex = 3;
            this.mePictureBox.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.meNameLabel);
            this.groupBox1.Controls.Add(this.mePictureBox);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(126, 131);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Session";
            // 
            // meNameLabel
            // 
            this.meNameLabel.Location = new System.Drawing.Point(11, 21);
            this.meNameLabel.Name = "meNameLabel";
            this.meNameLabel.Size = new System.Drawing.Size(109, 19);
            this.meNameLabel.TabIndex = 4;
            // 
            // signOutButton
            // 
            this.signOutButton.Enabled = false;
            this.signOutButton.Location = new System.Drawing.Point(261, 22);
            this.signOutButton.Name = "signOutButton";
            this.signOutButton.Size = new System.Drawing.Size(108, 23);
            this.signOutButton.TabIndex = 7;
            this.signOutButton.Text = "Sign out";
            this.signOutButton.UseVisualStyleBackColor = true;
            this.signOutButton.Click += new System.EventHandler(this.SignOutButton_Click);
            // 
            // connectGroupBox
            // 
            this.connectGroupBox.Controls.Add(this.richTextStatus);
            this.connectGroupBox.Enabled = false;
            this.connectGroupBox.Location = new System.Drawing.Point(15, 197);
            this.connectGroupBox.Name = "connectGroupBox";
            this.connectGroupBox.Size = new System.Drawing.Size(658, 346);
            this.connectGroupBox.TabIndex = 18;
            this.connectGroupBox.TabStop = false;
            // 
            // richTextStatus
            // 
            this.richTextStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextStatus.Location = new System.Drawing.Point(6, 19);
            this.richTextStatus.Name = "richTextStatus";
            this.richTextStatus.Size = new System.Drawing.Size(646, 321);
            this.richTextStatus.TabIndex = 0;
            this.richTextStatus.Text = "";
            // 
            // btnValidateMP3
            // 
            this.btnValidateMP3.Location = new System.Drawing.Point(147, 55);
            this.btnValidateMP3.Name = "btnValidateMP3";
            this.btnValidateMP3.Size = new System.Drawing.Size(108, 23);
            this.btnValidateMP3.TabIndex = 19;
            this.btnValidateMP3.Text = "Validate Excel";
            this.btnValidateMP3.UseVisualStyleBackColor = true;
            this.btnValidateMP3.Click += new System.EventHandler(this.btnValidateMP3_Click);
            // 
            // txtExcel
            // 
            this.txtExcel.Location = new System.Drawing.Point(219, 123);
            this.txtExcel.Name = "txtExcel";
            this.txtExcel.Size = new System.Drawing.Size(248, 20);
            this.txtExcel.TabIndex = 20;
            // 
            // lblOneDrive
            // 
            this.lblOneDrive.AutoSize = true;
            this.lblOneDrive.Location = new System.Drawing.Point(18, 157);
            this.lblOneDrive.Name = "lblOneDrive";
            this.lblOneDrive.Size = new System.Drawing.Size(81, 13);
            this.lblOneDrive.TabIndex = 21;
            this.lblOneDrive.Text = "OneDrive Root:";
            // 
            // txtOneDriveRoot
            // 
            this.txtOneDriveRoot.Location = new System.Drawing.Point(105, 154);
            this.txtOneDriveRoot.Name = "txtOneDriveRoot";
            this.txtOneDriveRoot.Size = new System.Drawing.Size(364, 20);
            this.txtOneDriveRoot.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(147, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Excel target:";
            // 
            // btnValidateMP3Files
            // 
            this.btnValidateMP3Files.Location = new System.Drawing.Point(147, 84);
            this.btnValidateMP3Files.Name = "btnValidateMP3Files";
            this.btnValidateMP3Files.Size = new System.Drawing.Size(108, 23);
            this.btnValidateMP3Files.TabIndex = 25;
            this.btnValidateMP3Files.Text = "Validate MP3 Files";
            this.btnValidateMP3Files.UseVisualStyleBackColor = true;
            this.btnValidateMP3Files.Click += new System.EventHandler(this.btnValidateMP3Files_Click);
            // 
            // btnValidateZipFiles
            // 
            this.btnValidateZipFiles.Location = new System.Drawing.Point(261, 84);
            this.btnValidateZipFiles.Name = "btnValidateZipFiles";
            this.btnValidateZipFiles.Size = new System.Drawing.Size(108, 23);
            this.btnValidateZipFiles.TabIndex = 26;
            this.btnValidateZipFiles.Text = "Validate ZIP Files";
            this.btnValidateZipFiles.UseVisualStyleBackColor = true;
            this.btnValidateZipFiles.Click += new System.EventHandler(this.btnValidateZipFiles_Click);
            // 
            // btnZIPLink
            // 
            this.btnZIPLink.Location = new System.Drawing.Point(261, 55);
            this.btnZIPLink.Name = "btnZIPLink";
            this.btnZIPLink.Size = new System.Drawing.Size(108, 23);
            this.btnZIPLink.TabIndex = 27;
            this.btnZIPLink.Text = "ZIP Links";
            this.btnZIPLink.UseVisualStyleBackColor = true;
            this.btnZIPLink.Click += new System.EventHandler(this.btnZIPLink_Click);
            // 
            // SyncTimer
            // 
            this.SyncTimer.Tick += new System.EventHandler(this.SyncTimer_Tick);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(394, 22);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 28;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 555);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnZIPLink);
            this.Controls.Add(this.btnValidateZipFiles);
            this.Controls.Add(this.btnValidateMP3Files);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOneDriveRoot);
            this.Controls.Add(this.lblOneDrive);
            this.Controls.Add(this.txtExcel);
            this.Controls.Add(this.btnValidateMP3);
            this.Controls.Add(this.connectGroupBox);
            this.Controls.Add(this.signOutButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.signinButton);
            this.Controls.Add(this.signOutWebBrowser);
            this.Name = "MainForm";
            this.Text = "Live Connect API Explorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ClientSizeChanged += new System.EventHandler(this.MainForm_ClientSizeChange);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.mePictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.connectGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button signinButton;
        private System.Windows.Forms.WebBrowser signOutWebBrowser;
        private System.Windows.Forms.PictureBox mePictureBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label meNameLabel;
        private System.Windows.Forms.Button signOutButton;
        private System.Windows.Forms.GroupBox connectGroupBox;
        private System.Windows.Forms.Button btnValidateMP3;
        private System.Windows.Forms.TextBox txtExcel;
        private System.Windows.Forms.RichTextBox richTextStatus;
        private System.Windows.Forms.Label lblOneDrive;
        private System.Windows.Forms.TextBox txtOneDriveRoot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnValidateMP3Files;
        private System.Windows.Forms.Button btnValidateZipFiles;
        private System.Windows.Forms.Button btnZIPLink;
        private System.Windows.Forms.Timer SyncTimer;
        private System.Windows.Forms.Button btnStop;
    }
}

