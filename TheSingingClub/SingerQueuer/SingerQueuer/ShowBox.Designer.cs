﻿namespace SingerQueuer
{
	partial class ShowBox
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
			this.richShowBox = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// richShowBox
			// 
			this.richShowBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richShowBox.Location = new System.Drawing.Point(12, 12);
			this.richShowBox.Name = "richShowBox";
			this.richShowBox.Size = new System.Drawing.Size(846, 317);
			this.richShowBox.TabIndex = 0;
			this.richShowBox.Text = "";
			// 
			// ShowBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(870, 341);
			this.Controls.Add(this.richShowBox);
			this.Name = "ShowBox";
			this.Text = "ShowBox";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox richShowBox;
	}
}