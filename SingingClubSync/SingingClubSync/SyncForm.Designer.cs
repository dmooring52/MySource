namespace SingingClubSync
{
    partial class SyncForm
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
            this.btnSongList = new System.Windows.Forms.Button();
            this.btnArchive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(163, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(109, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Sync Queue Data";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnSongList
            // 
            this.btnSongList.Location = new System.Drawing.Point(163, 41);
            this.btnSongList.Name = "btnSongList";
            this.btnSongList.Size = new System.Drawing.Size(109, 23);
            this.btnSongList.TabIndex = 1;
            this.btnSongList.Text = "SongList";
            this.btnSongList.UseVisualStyleBackColor = true;
            this.btnSongList.Click += new System.EventHandler(this.btnSongList_Click);
            // 
            // btnArchive
            // 
            this.btnArchive.Location = new System.Drawing.Point(163, 70);
            this.btnArchive.Name = "btnArchive";
            this.btnArchive.Size = new System.Drawing.Size(109, 23);
            this.btnArchive.TabIndex = 2;
            this.btnArchive.Text = "250 Day Archive";
            this.btnArchive.UseVisualStyleBackColor = true;
            this.btnArchive.Click += new System.EventHandler(this.btnArchive_Click);
            // 
            // SyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnArchive);
            this.Controls.Add(this.btnSongList);
            this.Controls.Add(this.btnRun);
            this.Name = "SyncForm";
            this.Text = "Synchronization";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnSongList;
        private System.Windows.Forms.Button btnArchive;
    }
}

