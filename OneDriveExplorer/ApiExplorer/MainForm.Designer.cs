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

namespace Microsoft.Live.Desktop.Samples.ApiExplorer
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
            this.signinButton = new System.Windows.Forms.Button();
            this.signOutWebBrowser = new System.Windows.Forms.WebBrowser();
            this.mePictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.currentScopeTextBox = new System.Windows.Forms.TextBox();
            this.meNameLabel = new System.Windows.Forms.Label();
            this.signOutButton = new System.Windows.Forms.Button();
            this.connectGroupBox = new System.Windows.Forms.GroupBox();
            this.btnCreateMap = new System.Windows.Forms.Button();
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.richTextStatus = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mePictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.connectGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // signinButton
            // 
            this.signinButton.Location = new System.Drawing.Point(247, 22);
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
            this.groupBox1.Controls.Add(this.currentScopeTextBox);
            this.groupBox1.Controls.Add(this.meNameLabel);
            this.groupBox1.Controls.Add(this.mePictureBox);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 131);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Session";
            // 
            // currentScopeTextBox
            // 
            this.currentScopeTextBox.Location = new System.Drawing.Point(88, 43);
            this.currentScopeTextBox.Multiline = true;
            this.currentScopeTextBox.Name = "currentScopeTextBox";
            this.currentScopeTextBox.ReadOnly = true;
            this.currentScopeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.currentScopeTextBox.Size = new System.Drawing.Size(131, 72);
            this.currentScopeTextBox.TabIndex = 5;
            // 
            // meNameLabel
            // 
            this.meNameLabel.Location = new System.Drawing.Point(11, 21);
            this.meNameLabel.Name = "meNameLabel";
            this.meNameLabel.Size = new System.Drawing.Size(209, 19);
            this.meNameLabel.TabIndex = 4;
            // 
            // signOutButton
            // 
            this.signOutButton.Enabled = false;
            this.signOutButton.Location = new System.Drawing.Point(361, 22);
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
            this.connectGroupBox.Location = new System.Drawing.Point(15, 149);
            this.connectGroupBox.Name = "connectGroupBox";
            this.connectGroupBox.Size = new System.Drawing.Size(658, 394);
            this.connectGroupBox.TabIndex = 18;
            this.connectGroupBox.TabStop = false;
            // 
            // btnCreateMap
            // 
            this.btnCreateMap.Location = new System.Drawing.Point(247, 93);
            this.btnCreateMap.Name = "btnCreateMap";
            this.btnCreateMap.Size = new System.Drawing.Size(75, 23);
            this.btnCreateMap.TabIndex = 19;
            this.btnCreateMap.Text = "Create Map";
            this.btnCreateMap.UseVisualStyleBackColor = true;
            this.btnCreateMap.Click += new System.EventHandler(this.btnCreateMap_Click);
            // 
            // txtRoot
            // 
            this.txtRoot.Location = new System.Drawing.Point(247, 122);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.Size = new System.Drawing.Size(222, 20);
            this.txtRoot.TabIndex = 20;
            // 
            // richTextStatus
            // 
            this.richTextStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextStatus.Location = new System.Drawing.Point(6, 19);
            this.richTextStatus.Name = "richTextStatus";
            this.richTextStatus.Size = new System.Drawing.Size(646, 369);
            this.richTextStatus.TabIndex = 0;
            this.richTextStatus.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 555);
            this.Controls.Add(this.txtRoot);
            this.Controls.Add(this.btnCreateMap);
            this.Controls.Add(this.connectGroupBox);
            this.Controls.Add(this.signOutButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.signinButton);
            this.Controls.Add(this.signOutWebBrowser);
            this.Name = "MainForm";
            this.Text = "Live Connect API Explorer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ClientSizeChanged += new System.EventHandler(this.MainForm_ClientSizeChange);
            ((System.ComponentModel.ISupportInitialize)(this.mePictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox currentScopeTextBox;
        private System.Windows.Forms.Button btnCreateMap;
        private System.Windows.Forms.TextBox txtRoot;
        private System.Windows.Forms.RichTextBox richTextStatus;
    }
}

