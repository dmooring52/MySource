namespace FilesActor
{
    partial class ActorForm
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
            this.btnRun = new System.Windows.Forms.Button();
            this.lblFolderOne = new System.Windows.Forms.Label();
            this.lblFolderTwo = new System.Windows.Forms.Label();
            this.txtMaster = new System.Windows.Forms.TextBox();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.chkSuperset = new System.Windows.Forms.CheckBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(440, 144);
            this.btnRun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(87, 28);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblFolderOne
            // 
            this.lblFolderOne.AutoSize = true;
            this.lblFolderOne.Location = new System.Drawing.Point(8, 87);
            this.lblFolderOne.Name = "lblFolderOne";
            this.lblFolderOne.Size = new System.Drawing.Size(52, 16);
            this.lblFolderOne.TabIndex = 1;
            this.lblFolderOne.Text = "Master:";
            // 
            // lblFolderTwo
            // 
            this.lblFolderTwo.AutoSize = true;
            this.lblFolderTwo.Location = new System.Drawing.Point(8, 117);
            this.lblFolderTwo.Name = "lblFolderTwo";
            this.lblFolderTwo.Size = new System.Drawing.Size(47, 16);
            this.lblFolderTwo.TabIndex = 2;
            this.lblFolderTwo.Text = "Target:";
            // 
            // txtMaster
            // 
            this.txtMaster.Location = new System.Drawing.Point(66, 84);
            this.txtMaster.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaster.Name = "txtMaster";
            this.txtMaster.Size = new System.Drawing.Size(461, 22);
            this.txtMaster.TabIndex = 3;
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(66, 114);
            this.txtTarget.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(461, 22);
            this.txtTarget.TabIndex = 4;
            // 
            // chkSuperset
            // 
            this.chkSuperset.AutoSize = true;
            this.chkSuperset.Location = new System.Drawing.Point(66, 149);
            this.chkSuperset.Name = "chkSuperset";
            this.chkSuperset.Size = new System.Drawing.Size(79, 20);
            this.chkSuperset.TabIndex = 6;
            this.chkSuperset.Text = "Superset";
            this.chkSuperset.UseVisualStyleBackColor = true;
            this.chkSuperset.CheckedChanged += new System.EventHandler(this.chkSuperset_CheckedChanged);
            // 
            // MainPanel
            // 
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainPanel.Controls.Add(this.txtHeader);
            this.MainPanel.Location = new System.Drawing.Point(12, 12);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(515, 65);
            this.MainPanel.TabIndex = 7;
            // 
            // txtHeader
            // 
            this.txtHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeader.ForeColor = System.Drawing.Color.DarkRed;
            this.txtHeader.Location = new System.Drawing.Point(3, 3);
            this.txtHeader.Multiline = true;
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.ReadOnly = true;
            this.txtHeader.Size = new System.Drawing.Size(505, 55);
            this.txtHeader.TabIndex = 0;
            this.txtHeader.Text = "Select the common folder for Master and Target\r\nTarget will be made to match the " +
    "files in the Master\r\nFor Superset, both Master and Target will contain a superse" +
    "t of the content";
            this.txtHeader.WordWrap = false;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(66, 192);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(461, 22);
            this.txtOutput.TabIndex = 8;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(9, 195);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(51, 16);
            this.lblOutput.TabIndex = 9;
            this.lblOutput.Text = "Output:";
            // 
            // ActorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 226);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.chkSuperset);
            this.Controls.Add(this.txtTarget);
            this.Controls.Add(this.txtMaster);
            this.Controls.Add(this.lblFolderTwo);
            this.Controls.Add(this.lblFolderOne);
            this.Controls.Add(this.btnRun);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(6)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ActorForm";
            this.Text = "File Actor";
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblFolderOne;
        private System.Windows.Forms.Label lblFolderTwo;
        private System.Windows.Forms.TextBox txtMaster;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.CheckBox chkSuperset;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label lblOutput;
    }
}

