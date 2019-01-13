namespace SingerQueuer
{
	partial class SingersControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridViewSingers = new System.Windows.Forms.DataGridView();
			this.contextMenuStripSave = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
			this.SingerKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SingerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SingerEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SingerActivity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSingers)).BeginInit();
			this.contextMenuStripSave.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridViewSingers
			// 
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dataGridViewSingers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewSingers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewSingers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewSingers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SingerKey,
            this.SingerName,
            this.SingerEmail,
            this.SingerActivity});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewSingers.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewSingers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewSingers.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewSingers.Name = "dataGridViewSingers";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewSingers.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewSingers.Size = new System.Drawing.Size(1034, 381);
			this.dataGridViewSingers.TabIndex = 0;
			this.dataGridViewSingers.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dataGridViewSingers_CellContextMenuStripNeeded);
			this.dataGridViewSingers.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSingers_RowValidated);
			// 
			// contextMenuStripSave
			// 
			this.contextMenuStripSave.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSave});
			this.contextMenuStripSave.Name = "contextMenuStripSave";
			this.contextMenuStripSave.Size = new System.Drawing.Size(99, 26);
			this.contextMenuStripSave.Click += new System.EventHandler(this.contextMenuStripSave_Click);
			// 
			// toolStripMenuItemSave
			// 
			this.toolStripMenuItemSave.Name = "toolStripMenuItemSave";
			this.toolStripMenuItemSave.Size = new System.Drawing.Size(98, 22);
			this.toolStripMenuItemSave.Text = "Save";
			// 
			// SingerKey
			// 
			this.SingerKey.HeaderText = "Key";
			this.SingerKey.Name = "SingerKey";
			this.SingerKey.Width = 150;
			// 
			// SingerName
			// 
			this.SingerName.HeaderText = "Name";
			this.SingerName.Name = "SingerName";
			this.SingerName.Width = 350;
			// 
			// SingerEmail
			// 
			this.SingerEmail.HeaderText = "Email";
			this.SingerEmail.Name = "SingerEmail";
			this.SingerEmail.Width = 370;
			// 
			// SingerActivity
			// 
			this.SingerActivity.HeaderText = "Activity";
			this.SingerActivity.Name = "SingerActivity";
			// 
			// SingersControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.dataGridViewSingers);
			this.Name = "SingersControl";
			this.Size = new System.Drawing.Size(1034, 381);
			this.Load += new System.EventHandler(this.SingersControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSingers)).EndInit();
			this.contextMenuStripSave.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewSingers;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripSave;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
		private System.Windows.Forms.DataGridViewTextBoxColumn SingerKey;
		private System.Windows.Forms.DataGridViewTextBoxColumn SingerName;
		private System.Windows.Forms.DataGridViewTextBoxColumn SingerEmail;
		private System.Windows.Forms.DataGridViewTextBoxColumn SingerActivity;

	}
}
