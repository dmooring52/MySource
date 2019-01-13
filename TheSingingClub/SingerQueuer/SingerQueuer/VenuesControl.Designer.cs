namespace SingerQueuer
{
	partial class VenuesControl
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
			this.dataGridViewVenues = new System.Windows.Forms.DataGridView();
			this.contextMenuStripSave = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
			this.VenueKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.VenueName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.VenueEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.VenueAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.VenueContact = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.VenuePhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewVenues)).BeginInit();
			this.contextMenuStripSave.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridViewVenues
			// 
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dataGridViewVenues.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewVenues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewVenues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewVenues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VenueKey,
            this.VenueName,
            this.VenueEmail,
            this.VenueAddress,
            this.VenueContact,
            this.VenuePhone});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewVenues.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewVenues.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewVenues.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewVenues.Name = "dataGridViewVenues";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewVenues.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewVenues.Size = new System.Drawing.Size(1034, 381);
			this.dataGridViewVenues.TabIndex = 1;
			this.dataGridViewVenues.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dataGridViewVenues_CellContextMenuStripNeeded);
			this.dataGridViewVenues.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewVenues_RowValidated);
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
			// VenueKey
			// 
			this.VenueKey.HeaderText = "Key";
			this.VenueKey.Name = "VenueKey";
			// 
			// VenueName
			// 
			this.VenueName.HeaderText = "Name";
			this.VenueName.Name = "VenueName";
			this.VenueName.Width = 150;
			// 
			// VenueEmail
			// 
			this.VenueEmail.HeaderText = "Email";
			this.VenueEmail.Name = "VenueEmail";
			this.VenueEmail.Width = 200;
			// 
			// VenueAddress
			// 
			this.VenueAddress.HeaderText = "Address";
			this.VenueAddress.Name = "VenueAddress";
			this.VenueAddress.Width = 220;
			// 
			// VenueContact
			// 
			this.VenueContact.HeaderText = "Contact";
			this.VenueContact.MinimumWidth = 200;
			this.VenueContact.Name = "VenueContact";
			this.VenueContact.Width = 200;
			// 
			// VenuePhone
			// 
			this.VenuePhone.HeaderText = "Phone";
			this.VenuePhone.Name = "VenuePhone";
			// 
			// VenuesControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridViewVenues);
			this.Name = "VenuesControl";
			this.Size = new System.Drawing.Size(1034, 381);
			this.Load += new System.EventHandler(this.VenuesControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewVenues)).EndInit();
			this.contextMenuStripSave.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewVenues;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripSave;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
		private System.Windows.Forms.DataGridViewTextBoxColumn VenueKey;
		private System.Windows.Forms.DataGridViewTextBoxColumn VenueName;
		private System.Windows.Forms.DataGridViewTextBoxColumn VenueEmail;
		private System.Windows.Forms.DataGridViewTextBoxColumn VenueAddress;
		private System.Windows.Forms.DataGridViewTextBoxColumn VenueContact;
		private System.Windows.Forms.DataGridViewTextBoxColumn VenuePhone;
	}
}
