namespace KaraokeSpreadSheetValidator
{
	partial class KaraokeSpreadsheetValidator
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
			this.btnValidate = new System.Windows.Forms.Button();
			this.statusStripKSV = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelKSV = new System.Windows.Forms.ToolStripStatusLabel();
			this.timerKSV = new System.Windows.Forms.Timer(this.components);
			this.btnZips = new System.Windows.Forms.Button();
			this.statusStripKSV.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnValidate
			// 
			this.btnValidate.Location = new System.Drawing.Point(240, 12);
			this.btnValidate.Name = "btnValidate";
			this.btnValidate.Size = new System.Drawing.Size(75, 23);
			this.btnValidate.TabIndex = 0;
			this.btnValidate.Text = "Validate";
			this.btnValidate.UseVisualStyleBackColor = true;
			this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
			// 
			// statusStripKSV
			// 
			this.statusStripKSV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelKSV});
			this.statusStripKSV.Location = new System.Drawing.Point(0, 240);
			this.statusStripKSV.Name = "statusStripKSV";
			this.statusStripKSV.Size = new System.Drawing.Size(327, 22);
			this.statusStripKSV.TabIndex = 1;
			this.statusStripKSV.Text = "statusStrip1";
			// 
			// toolStripStatusLabelKSV
			// 
			this.toolStripStatusLabelKSV.Name = "toolStripStatusLabelKSV";
			this.toolStripStatusLabelKSV.Size = new System.Drawing.Size(25, 17);
			this.toolStripStatusLabelKSV.Text = "File";
			// 
			// timerKSV
			// 
			this.timerKSV.Interval = 10;
			this.timerKSV.Tick += new System.EventHandler(this.timerKSV_Tick);
			// 
			// btnZips
			// 
			this.btnZips.Location = new System.Drawing.Point(240, 41);
			this.btnZips.Name = "btnZips";
			this.btnZips.Size = new System.Drawing.Size(75, 23);
			this.btnZips.TabIndex = 2;
			this.btnZips.Text = "Zips";
			this.btnZips.UseVisualStyleBackColor = true;
			this.btnZips.Click += new System.EventHandler(this.btnZips_Click);
			// 
			// KaraokeSpreadsheetValidator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(327, 262);
			this.Controls.Add(this.btnZips);
			this.Controls.Add(this.statusStripKSV);
			this.Controls.Add(this.btnValidate);
			this.Name = "KaraokeSpreadsheetValidator";
			this.Text = "Karaoke Spreadsheet Validator";
			this.statusStripKSV.ResumeLayout(false);
			this.statusStripKSV.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnValidate;
		private System.Windows.Forms.StatusStrip statusStripKSV;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelKSV;
		private System.Windows.Forms.Timer timerKSV;
		private System.Windows.Forms.Button btnZips;
	}
}

