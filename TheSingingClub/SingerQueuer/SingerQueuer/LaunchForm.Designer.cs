namespace SingerQueuer
{
	partial class LaunchForm
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
			this.grpPlayers = new System.Windows.Forms.GroupBox();
			this.radio_SunFly = new System.Windows.Forms.RadioButton();
			this.radio_KBPlayer = new System.Windows.Forms.RadioButton();
			this.btnPlayer = new System.Windows.Forms.Button();
			this.grpPlayers.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpPlayers
			// 
			this.grpPlayers.Controls.Add(this.btnPlayer);
			this.grpPlayers.Controls.Add(this.radio_KBPlayer);
			this.grpPlayers.Controls.Add(this.radio_SunFly);
			this.grpPlayers.Location = new System.Drawing.Point(12, 12);
			this.grpPlayers.Name = "grpPlayers";
			this.grpPlayers.Size = new System.Drawing.Size(260, 238);
			this.grpPlayers.TabIndex = 0;
			this.grpPlayers.TabStop = false;
			// 
			// radio_SunFly
			// 
			this.radio_SunFly.AutoSize = true;
			this.radio_SunFly.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radio_SunFly.Location = new System.Drawing.Point(55, 56);
			this.radio_SunFly.Name = "radio_SunFly";
			this.radio_SunFly.Size = new System.Drawing.Size(67, 20);
			this.radio_SunFly.TabIndex = 0;
			this.radio_SunFly.Text = "SunFly";
			this.radio_SunFly.UseVisualStyleBackColor = true;
			// 
			// radio_KBPlayer
			// 
			this.radio_KBPlayer.AutoSize = true;
			this.radio_KBPlayer.Checked = true;
			this.radio_KBPlayer.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radio_KBPlayer.Location = new System.Drawing.Point(55, 82);
			this.radio_KBPlayer.Name = "radio_KBPlayer";
			this.radio_KBPlayer.Size = new System.Drawing.Size(85, 20);
			this.radio_KBPlayer.TabIndex = 1;
			this.radio_KBPlayer.TabStop = true;
			this.radio_KBPlayer.Text = "KB Player";
			this.radio_KBPlayer.UseVisualStyleBackColor = true;
			// 
			// btnPlayer
			// 
			this.btnPlayer.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPlayer.Location = new System.Drawing.Point(55, 121);
			this.btnPlayer.Name = "btnPlayer";
			this.btnPlayer.Size = new System.Drawing.Size(85, 29);
			this.btnPlayer.TabIndex = 2;
			this.btnPlayer.Text = "OK";
			this.btnPlayer.UseVisualStyleBackColor = true;
			this.btnPlayer.Click += new System.EventHandler(this.btnPlayer_Click);
			// 
			// LaunchForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.grpPlayers);
			this.Name = "LaunchForm";
			this.Text = "Launch";
			this.Load += new System.EventHandler(this.LaunchForm_Load);
			this.grpPlayers.ResumeLayout(false);
			this.grpPlayers.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpPlayers;
		private System.Windows.Forms.RadioButton radio_KBPlayer;
		private System.Windows.Forms.RadioButton radio_SunFly;
		private System.Windows.Forms.Button btnPlayer;
	}
}