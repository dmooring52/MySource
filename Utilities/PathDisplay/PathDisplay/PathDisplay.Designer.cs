namespace PathDisplay
{
	partial class PathDisplay
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
			this.lblPath = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.txtDirectory = new System.Windows.Forms.TextBox();
			this.lblDirectory = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblPath
			// 
			this.lblPath.AutoSize = true;
			this.lblPath.Location = new System.Drawing.Point(20, 12);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(52, 16);
			this.lblPath.TabIndex = 0;
			this.lblPath.Text = "Full Path:";
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(80, 12);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(276, 22);
			this.txtPath.TabIndex = 1;
			this.txtPath.Enter += new System.EventHandler(this.txtPath_Enter);
			// 
			// txtDirectory
			// 
			this.txtDirectory.Location = new System.Drawing.Point(80, 40);
			this.txtDirectory.Name = "txtDirectory";
			this.txtDirectory.Size = new System.Drawing.Size(276, 22);
			this.txtDirectory.TabIndex = 3;
			this.txtDirectory.Enter += new System.EventHandler(this.txtDirectory_Enter);
			// 
			// lblDirectory
			// 
			this.lblDirectory.AutoSize = true;
			this.lblDirectory.Location = new System.Drawing.Point(20, 43);
			this.lblDirectory.Name = "lblDirectory";
			this.lblDirectory.Size = new System.Drawing.Size(54, 16);
			this.lblDirectory.TabIndex = 2;
			this.lblDirectory.Text = "Directory:";
			// 
			// PathDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(368, 77);
			this.Controls.Add(this.txtDirectory);
			this.Controls.Add(this.lblDirectory);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.lblPath);
			this.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "PathDisplay";
			this.Text = "Path Display";
			this.Load += new System.EventHandler(this.PathDisplay_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblPath;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.TextBox txtDirectory;
		private System.Windows.Forms.Label lblDirectory;
	}
}

