namespace ToolMaker
{
	partial class ToolView
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
			this.richTextBoxView = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// richTextBoxView
			// 
			this.richTextBoxView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxView.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBoxView.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxView.Name = "richTextBoxView";
			this.richTextBoxView.Size = new System.Drawing.Size(885, 378);
			this.richTextBoxView.TabIndex = 0;
			this.richTextBoxView.Text = "";
			// 
			// ToolView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(885, 378);
			this.Controls.Add(this.richTextBoxView);
			this.Name = "ToolView";
			this.Text = "ToolView";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBoxView;
	}
}