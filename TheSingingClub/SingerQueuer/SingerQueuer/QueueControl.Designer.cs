namespace SingerQueuer
{
	partial class QueueControl
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
			this.dataGridViewQueue = new System.Windows.Forms.DataGridView();
			this.EventKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SingerKey = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.QueueRound = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.QueueOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.QueueSong = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.QueueArtist = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.QueueNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.QueueLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.QueueState = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStripSave = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripOrder = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.setStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.finishedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.goneHomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.notHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemOrder = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemUp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDown = new System.Windows.Forms.ToolStripMenuItem();
			this.markToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.singerHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewQueue)).BeginInit();
			this.contextMenuStripSave.SuspendLayout();
			this.contextMenuStripOrder.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridViewQueue
			// 
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dataGridViewQueue.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewQueue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EventKey,
            this.SingerKey,
            this.QueueRound,
            this.QueueOrder,
            this.QueueSong,
            this.QueueArtist,
            this.QueueNote,
            this.QueueLink,
            this.QueueState});
			this.dataGridViewQueue.ContextMenuStrip = this.contextMenuStripSave;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewQueue.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewQueue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewQueue.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewQueue.Name = "dataGridViewQueue";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewQueue.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewQueue.Size = new System.Drawing.Size(1034, 381);
			this.dataGridViewQueue.TabIndex = 2;
			this.dataGridViewQueue.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewQueue_CellContentDoubleClick);
			this.dataGridViewQueue.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dataGridViewQueue_CellContextMenuStripNeeded);
			this.dataGridViewQueue.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewQueue_CellPainting);
			this.dataGridViewQueue.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewQueue_RowStateChanged);
			this.dataGridViewQueue.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewQueue_RowValidated);
			// 
			// EventKey
			// 
			this.EventKey.HeaderText = "Event";
			this.EventKey.Name = "EventKey";
			this.EventKey.ReadOnly = true;
			this.EventKey.Visible = false;
			// 
			// SingerKey
			// 
			this.SingerKey.HeaderText = "Singer";
			this.SingerKey.Name = "SingerKey";
			this.SingerKey.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.SingerKey.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.SingerKey.Width = 150;
			// 
			// QueueRound
			// 
			this.QueueRound.HeaderText = "Round";
			this.QueueRound.Name = "QueueRound";
			this.QueueRound.Visible = false;
			// 
			// QueueOrder
			// 
			this.QueueOrder.HeaderText = "Order";
			this.QueueOrder.Name = "QueueOrder";
			this.QueueOrder.ReadOnly = true;
			this.QueueOrder.Width = 50;
			// 
			// QueueSong
			// 
			this.QueueSong.HeaderText = "Song";
			this.QueueSong.Name = "QueueSong";
			this.QueueSong.Width = 190;
			// 
			// QueueArtist
			// 
			this.QueueArtist.HeaderText = "Artist";
			this.QueueArtist.Name = "QueueArtist";
			// 
			// QueueNote
			// 
			this.QueueNote.HeaderText = "Note";
			this.QueueNote.Name = "QueueNote";
			this.QueueNote.Width = 200;
			// 
			// QueueLink
			// 
			this.QueueLink.HeaderText = "Link";
			this.QueueLink.Name = "QueueLink";
			this.QueueLink.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.QueueLink.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.QueueLink.Width = 280;
			// 
			// QueueState
			// 
			this.QueueState.HeaderText = "State";
			this.QueueState.Name = "QueueState";
			this.QueueState.Visible = false;
			// 
			// contextMenuStripSave
			// 
			this.contextMenuStripSave.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSave});
			this.contextMenuStripSave.Name = "contextMenuStripSave";
			this.contextMenuStripSave.Size = new System.Drawing.Size(164, 26);
			this.contextMenuStripSave.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripSave_Opening);
			this.contextMenuStripSave.Click += new System.EventHandler(this.contextMenuStripSave_Click);
			// 
			// toolStripMenuItemSave
			// 
			this.toolStripMenuItemSave.Name = "toolStripMenuItemSave";
			this.toolStripMenuItemSave.Size = new System.Drawing.Size(163, 22);
			this.toolStripMenuItemSave.Text = "Save to Database";
			// 
			// contextMenuStripOrder
			// 
			this.contextMenuStripOrder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setStateToolStripMenuItem,
            this.toolStripMenuItemOrder,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.launchToolStripMenuItem,
            this.singerHistoryToolStripMenuItem});
			this.contextMenuStripOrder.Name = "contextMenuStripOrder";
			this.contextMenuStripOrder.Size = new System.Drawing.Size(149, 136);
			this.contextMenuStripOrder.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripOrder_Opening);
			// 
			// setStateToolStripMenuItem
			// 
			this.setStateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finishedToolStripMenuItem,
            this.goneHomeToolStripMenuItem,
            this.pendingToolStripMenuItem,
            this.notHereToolStripMenuItem});
			this.setStateToolStripMenuItem.Name = "setStateToolStripMenuItem";
			this.setStateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.setStateToolStripMenuItem.Text = "Set State";
			// 
			// finishedToolStripMenuItem
			// 
			this.finishedToolStripMenuItem.Name = "finishedToolStripMenuItem";
			this.finishedToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.finishedToolStripMenuItem.Text = "Finished";
			this.finishedToolStripMenuItem.Click += new System.EventHandler(this.finishedToolStripMenuItem_Click);
			// 
			// goneHomeToolStripMenuItem
			// 
			this.goneHomeToolStripMenuItem.Name = "goneHomeToolStripMenuItem";
			this.goneHomeToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.goneHomeToolStripMenuItem.Text = "Gone Home";
			this.goneHomeToolStripMenuItem.Click += new System.EventHandler(this.goneHomeToolStripMenuItem_Click);
			// 
			// pendingToolStripMenuItem
			// 
			this.pendingToolStripMenuItem.Name = "pendingToolStripMenuItem";
			this.pendingToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.pendingToolStripMenuItem.Text = "Pending";
			this.pendingToolStripMenuItem.Click += new System.EventHandler(this.pendingToolStripMenuItem_Click);
			// 
			// notHereToolStripMenuItem
			// 
			this.notHereToolStripMenuItem.Name = "notHereToolStripMenuItem";
			this.notHereToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.notHereToolStripMenuItem.Text = "Not Here";
			this.notHereToolStripMenuItem.Click += new System.EventHandler(this.notHereToolStripMenuItem_Click);
			// 
			// toolStripMenuItemOrder
			// 
			this.toolStripMenuItemOrder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemUp,
            this.toolStripMenuItemDown,
            this.markToolStripMenuItem,
            this.insertToolStripMenuItem});
			this.toolStripMenuItemOrder.Name = "toolStripMenuItemOrder";
			this.toolStripMenuItemOrder.Size = new System.Drawing.Size(148, 22);
			this.toolStripMenuItemOrder.Text = "Move Row";
			// 
			// toolStripMenuItemUp
			// 
			this.toolStripMenuItemUp.Name = "toolStripMenuItemUp";
			this.toolStripMenuItemUp.Size = new System.Drawing.Size(105, 22);
			this.toolStripMenuItemUp.Text = "Up";
			this.toolStripMenuItemUp.Click += new System.EventHandler(this.toolStripMenuItemUp_Click);
			// 
			// toolStripMenuItemDown
			// 
			this.toolStripMenuItemDown.Name = "toolStripMenuItemDown";
			this.toolStripMenuItemDown.Size = new System.Drawing.Size(105, 22);
			this.toolStripMenuItemDown.Text = "Down";
			this.toolStripMenuItemDown.Click += new System.EventHandler(this.toolStripMenuItemDown_Click);
			// 
			// markToolStripMenuItem
			// 
			this.markToolStripMenuItem.Name = "markToolStripMenuItem";
			this.markToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
			this.markToolStripMenuItem.Text = "Mark";
			this.markToolStripMenuItem.Click += new System.EventHandler(this.markToolStripMenuItem_Click);
			// 
			// insertToolStripMenuItem
			// 
			this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
			this.insertToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
			this.insertToolStripMenuItem.Text = "Insert";
			this.insertToolStripMenuItem.Click += new System.EventHandler(this.insertToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// launchToolStripMenuItem
			// 
			this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
			this.launchToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.launchToolStripMenuItem.Text = "Launch";
			this.launchToolStripMenuItem.Click += new System.EventHandler(this.launchToolStripMenuItem_Click);
			// 
			// singerHistoryToolStripMenuItem
			// 
			this.singerHistoryToolStripMenuItem.Name = "singerHistoryToolStripMenuItem";
			this.singerHistoryToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.singerHistoryToolStripMenuItem.Text = "Singer History";
			this.singerHistoryToolStripMenuItem.Click += new System.EventHandler(this.singerHistoryToolStripMenuItem_Click);
			// 
			// QueueControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridViewQueue);
			this.Name = "QueueControl";
			this.Size = new System.Drawing.Size(1034, 381);
			this.Load += new System.EventHandler(this.QueueControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewQueue)).EndInit();
			this.contextMenuStripSave.ResumeLayout(false);
			this.contextMenuStripOrder.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewQueue;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripSave;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripOrder;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOrder;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUp;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDown;
		private System.Windows.Forms.ToolStripMenuItem setStateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem finishedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem goneHomeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pendingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem notHereToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem singerHistoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem markToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn EventKey;
		private System.Windows.Forms.DataGridViewComboBoxColumn SingerKey;
		private System.Windows.Forms.DataGridViewTextBoxColumn QueueRound;
		private System.Windows.Forms.DataGridViewTextBoxColumn QueueOrder;
		private System.Windows.Forms.DataGridViewTextBoxColumn QueueSong;
		private System.Windows.Forms.DataGridViewTextBoxColumn QueueArtist;
		private System.Windows.Forms.DataGridViewTextBoxColumn QueueNote;
		private System.Windows.Forms.DataGridViewTextBoxColumn QueueLink;
		private System.Windows.Forms.DataGridViewTextBoxColumn QueueState;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem launchToolStripMenuItem;
	}
}
