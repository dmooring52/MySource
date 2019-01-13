namespace KaraokeQueuer
{
	partial class EventsControl
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
			this.dataGridViewEvents = new System.Windows.Forms.DataGridView();
			this.contextMenuStripSave = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
			this.EventKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.VenueKey = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.EventDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EventEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EventAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvents)).BeginInit();
			this.contextMenuStripSave.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridViewEvents
			// 
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dataGridViewEvents.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewEvents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EventKey,
            this.EventName,
            this.VenueKey,
            this.EventDate,
            this.EventEmail,
            this.EventAddress});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewEvents.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewEvents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewEvents.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewEvents.Name = "dataGridViewEvents";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewEvents.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewEvents.Size = new System.Drawing.Size(1034, 381);
			this.dataGridViewEvents.TabIndex = 1;
			this.dataGridViewEvents.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dataGridViewEvents_CellContextMenuStripNeeded);
			this.dataGridViewEvents.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEvents_RowValidated);
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
			// EventKey
			// 
			this.EventKey.HeaderText = "Key";
			this.EventKey.Name = "EventKey";
			// 
			// EventName
			// 
			this.EventName.HeaderText = "Name";
			this.EventName.Name = "EventName";
			this.EventName.Width = 145;
			// 
			// VenueKey
			// 
			this.VenueKey.HeaderText = "Venue";
			this.VenueKey.Name = "VenueKey";
			this.VenueKey.Width = 125;
			// 
			// EventDate
			// 
			this.EventDate.HeaderText = "Date";
			this.EventDate.Name = "EventDate";
			// 
			// EventEmail
			// 
			this.EventEmail.HeaderText = "Email";
			this.EventEmail.Name = "EventEmail";
			this.EventEmail.Width = 250;
			// 
			// EventAddress
			// 
			this.EventAddress.HeaderText = "Address";
			this.EventAddress.Name = "EventAddress";
			this.EventAddress.Width = 250;
			// 
			// EventsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridViewEvents);
			this.Name = "EventsControl";
			this.Size = new System.Drawing.Size(1034, 381);
			this.Load += new System.EventHandler(this.EventsControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvents)).EndInit();
			this.contextMenuStripSave.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewEvents;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripSave;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
		private System.Windows.Forms.DataGridViewTextBoxColumn EventKey;
		private System.Windows.Forms.DataGridViewTextBoxColumn EventName;
		private System.Windows.Forms.DataGridViewComboBoxColumn VenueKey;
		private System.Windows.Forms.DataGridViewTextBoxColumn EventDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn EventEmail;
		private System.Windows.Forms.DataGridViewTextBoxColumn EventAddress;
	}
}
