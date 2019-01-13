namespace ToolMaker
{
	partial class ToolMaker
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
			this.btnComponent = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnComponent
			// 
			this.btnComponent.Location = new System.Drawing.Point(12, 12);
			this.btnComponent.Name = "btnComponent";
			this.btnComponent.Size = new System.Drawing.Size(135, 23);
			this.btnComponent.TabIndex = 0;
			this.btnComponent.Text = "Component Classes";
			this.btnComponent.UseVisualStyleBackColor = true;
			this.btnComponent.Click += new System.EventHandler(this.btnComponent_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(12, 41);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(135, 23);
			this.btnLoad.TabIndex = 1;
			this.btnLoad.Text = "Component Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// ToolMaker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.btnComponent);
			this.Name = "ToolMaker";
			this.Text = "Tool Maker";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnComponent;
		private System.Windows.Forms.Button btnLoad;
	}
}

