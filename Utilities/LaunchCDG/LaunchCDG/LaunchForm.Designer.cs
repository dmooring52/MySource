namespace LaunchCDG
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.btn_Select = new System.Windows.Forms.Button();
			this.radio_KBPlayer = new System.Windows.Forms.RadioButton();
			this.radio_SunFly = new System.Windows.Forms.RadioButton();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btn_Select);
			this.panel1.Controls.Add(this.radio_KBPlayer);
			this.panel1.Controls.Add(this.radio_SunFly);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(260, 238);
			this.panel1.TabIndex = 0;
			// 
			// btn_Select
			// 
			this.btn_Select.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Select.Location = new System.Drawing.Point(46, 121);
			this.btn_Select.Name = "btn_Select";
			this.btn_Select.Size = new System.Drawing.Size(75, 23);
			this.btn_Select.TabIndex = 2;
			this.btn_Select.Text = "OK";
			this.btn_Select.UseVisualStyleBackColor = true;
			this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
			// 
			// radio_KBPlayer
			// 
			this.radio_KBPlayer.AutoSize = true;
			this.radio_KBPlayer.Location = new System.Drawing.Point(46, 82);
			this.radio_KBPlayer.Name = "radio_KBPlayer";
			this.radio_KBPlayer.Size = new System.Drawing.Size(71, 17);
			this.radio_KBPlayer.TabIndex = 1;
			this.radio_KBPlayer.TabStop = true;
			this.radio_KBPlayer.Text = "KB Player";
			this.radio_KBPlayer.UseVisualStyleBackColor = true;
			// 
			// radio_SunFly
			// 
			this.radio_SunFly.AutoSize = true;
			this.radio_SunFly.Location = new System.Drawing.Point(46, 59);
			this.radio_SunFly.Name = "radio_SunFly";
			this.radio_SunFly.Size = new System.Drawing.Size(57, 17);
			this.radio_SunFly.TabIndex = 0;
			this.radio_SunFly.TabStop = true;
			this.radio_SunFly.Text = "SunFly";
			this.radio_SunFly.UseVisualStyleBackColor = true;
			// 
			// LaunchForm
			// 
			this.AcceptButton = this.btn_Select;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_Select;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.panel1);
			this.Name = "LaunchForm";
			this.Text = "LaunchCDG";
			this.Load += new System.EventHandler(this.LaunchForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.RadioButton radio_KBPlayer;
        private System.Windows.Forms.RadioButton radio_SunFly;
    }
}

