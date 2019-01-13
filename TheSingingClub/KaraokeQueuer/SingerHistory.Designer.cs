namespace KaraokeQueuer
{
	partial class SingerHistory
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.dataGridViewHistory = new System.Windows.Forms.DataGridView();
			this.TSCEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Singer = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Song = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Artist = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Link = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStripCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripSave = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.exportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
			this.contextMenuStripCopy.SuspendLayout();
			this.contextMenuStripSave.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.dataGridViewHistory);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1056, 310);
			this.panel1.TabIndex = 0;
			// 
			// dataGridViewHistory
			// 
			this.dataGridViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TSCEvent,
            this.Singer,
            this.Song,
            this.Artist,
            this.Note,
            this.Link});
			this.dataGridViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewHistory.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewHistory.Name = "dataGridViewHistory";
			this.dataGridViewHistory.Size = new System.Drawing.Size(1056, 310);
			this.dataGridViewHistory.TabIndex = 0;
			this.dataGridViewHistory.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewHistory_CellMouseDown);
			this.dataGridViewHistory.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewHistory_RowHeaderMouseClick);
			// 
			// TSCEvent
			// 
			this.TSCEvent.HeaderText = "Event";
			this.TSCEvent.Name = "TSCEvent";
			this.TSCEvent.Width = 150;
			// 
			// Singer
			// 
			this.Singer.HeaderText = "Singer";
			this.Singer.Name = "Singer";
			this.Singer.Width = 150;
			// 
			// Song
			// 
			this.Song.HeaderText = "Song";
			this.Song.Name = "Song";
			this.Song.Width = 175;
			// 
			// Artist
			// 
			this.Artist.HeaderText = "Artist";
			this.Artist.Name = "Artist";
			this.Artist.Width = 150;
			// 
			// Note
			// 
			this.Note.HeaderText = "Note";
			this.Note.Name = "Note";
			this.Note.Width = 175;
			// 
			// Link
			// 
			this.Link.HeaderText = "Link";
			this.Link.Name = "Link";
			this.Link.Width = 175;
			// 
			// contextMenuStripCopy
			// 
			this.contextMenuStripCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
			this.contextMenuStripCopy.Name = "contextMenuStripCopy";
			this.contextMenuStripCopy.Size = new System.Drawing.Size(103, 26);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// contextMenuStripSave
			// 
			this.contextMenuStripSave.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcelToolStripMenuItem,
            this.exportToHTMLToolStripMenuItem});
			this.contextMenuStripSave.Name = "contextMenuStripSave";
			this.contextMenuStripSave.Size = new System.Drawing.Size(158, 48);
			// 
			// exportToExcelToolStripMenuItem
			// 
			this.exportToExcelToolStripMenuItem.Name = "exportToExcelToolStripMenuItem";
			this.exportToExcelToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.exportToExcelToolStripMenuItem.Text = "Export to Excel";
			this.exportToExcelToolStripMenuItem.Click += new System.EventHandler(this.exportToExcelToolStripMenuItem_Click);
			// 
			// exportToHTMLToolStripMenuItem
			// 
			this.exportToHTMLToolStripMenuItem.Name = "exportToHTMLToolStripMenuItem";
			this.exportToHTMLToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.exportToHTMLToolStripMenuItem.Text = "Export to HTML";
			this.exportToHTMLToolStripMenuItem.Click += new System.EventHandler(this.exportToHTMLToolStripMenuItem_Click);
			// 
			// SingerHistory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1056, 310);
			this.Controls.Add(this.panel1);
			this.MinimumSize = new System.Drawing.Size(1072, 348);
			this.Name = "SingerHistory";
			this.Text = "SingerHistory";
			this.Load += new System.EventHandler(this.SingerHistory_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
			this.contextMenuStripCopy.ResumeLayout(false);
			this.contextMenuStripSave.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridView dataGridViewHistory;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripCopy;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn TSCEvent;
		private System.Windows.Forms.DataGridViewTextBoxColumn Singer;
		private System.Windows.Forms.DataGridViewTextBoxColumn Song;
		private System.Windows.Forms.DataGridViewTextBoxColumn Artist;
		private System.Windows.Forms.DataGridViewTextBoxColumn Note;
		private System.Windows.Forms.DataGridViewTextBoxColumn Link;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripSave;
		private System.Windows.Forms.ToolStripMenuItem exportToExcelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToHTMLToolStripMenuItem;
	}
}