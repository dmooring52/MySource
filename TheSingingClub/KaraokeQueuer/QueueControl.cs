using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

using XmlUtility;

namespace KaraokeQueuer
{
	public partial class QueueControl : UserControl
	{
		#region member data
		public DataGridView queueDataGrid;
		public BindingList<TSCQueue> gd_tscqueue = null;
		public List<TSCQueue> db_tscqueue = null;

		private SingerDatabase _scc = null;
		private DataGridViewCellStyle defaultstyle;
		private DataGridViewCellStyle addstyle;
		private DataGridViewCellStyle changestyle;

		private DataGridViewCellStyle stateStyleFinished;
		private DataGridViewCellStyle stateStyleGoneHome;
		private DataGridViewCellStyle stateDefault;

		private int _rowIndex = 0;
		private int _markIndex = 0;
		private ContextMenuStrip _contextMenuStrip = null;

		private QueuerForm _queuerForm = null;
		private int _round = 0;

		#endregion
		#region constructors
		public QueueControl()
		{
			InitializeComponent();
			queueDataGrid = dataGridViewQueue;
		}
		#endregion
		#region Load
		private void QueueControl_Load(object sender, EventArgs e)
		{
			dataGridViewQueue.EnableHeadersVisualStyles = true;

			defaultstyle = new DataGridViewCellStyle();
			defaultstyle.ForeColor = Color.Black;
			addstyle = new DataGridViewCellStyle();
			addstyle.ForeColor = Color.DarkGreen;
			changestyle = new DataGridViewCellStyle();
			changestyle.ForeColor = Color.DarkBlue;

			stateDefault = new DataGridViewCellStyle();
			stateDefault.BackColor = Color.Green;
			stateStyleFinished = new DataGridViewCellStyle();
			stateStyleFinished.BackColor = Color.DarkBlue;
			stateStyleGoneHome = new DataGridViewCellStyle();
			stateStyleGoneHome.BackColor = Color.Black;

			_queuerForm = Tag as QueuerForm;
			dataGridViewQueue.AutoGenerateColumns = false;
			SetSingers();
			if (_queuerForm != null)
			{
				_scc = _queuerForm.scc;
				string ix = Name.Substring(Name.Length - 1, 1);
				switch (ix)
				{
					case "1":
						(Tag as QueuerForm).controlTag.queueControl1 = this;
						_round = 1;
						break;
					case "2":
						(Tag as QueuerForm).controlTag.queueControl2 = this;
						_round = 2;
						break;
					case "3":
						(Tag as QueuerForm).controlTag.queueControl3 = this;
						_round = 3;
						break;
					case "4":
						(Tag as QueuerForm).controlTag.queueControl4 = this;
						_round = 4;
						break;
					case "5":
						(Tag as QueuerForm).controlTag.queueControl5 = this;
						_round = 5;
						break;
					case "6":
						(Tag as QueuerForm).controlTag.queueControl6 = this;
						_round = 6;
						break;
				}
				//xml = GetQueue(_round);
				if (_round > 0 && _queuerForm != null)
				{
					LoadTSCQueue(_queuerForm.controlTag.queueXml, _round);
					_queuerForm.controlTag.queueControl1.MasterRefresh();
					_queuerForm.SetNext();
				}
			}
			else
				_scc = new SingerDatabase();

		}

		public void QueueControl_Reload()
		{
			if (_queuerForm == null || _queuerForm.controlTag == null || _queuerForm.controlTag.singersXml == null || _queuerForm.controlTag.singersXml == null)
				return;
			SetSingers();
			//xml = GetQueue(_round);
			LoadTSCQueue(_queuerForm.controlTag.queueXml, _round);
		}

		public void SetSingers()
		{
			if (_queuerForm == null || _queuerForm.controlTag == null || _queuerForm.controlTag.singersXml == null || _queuerForm.controlTag.singersXml == null)
				return;
			DataGridViewComboBoxColumn cbc = dataGridViewQueue.Columns["SingerKey"] as DataGridViewComboBoxColumn;
			cbc.Items.Clear();
			try
			{
				if (_queuerForm.controlTag.singersXml != null)
				{
					XmlNodeList nodelist = _queuerForm.controlTag.singersXml.SelectNodes("/Root/Data");
					foreach (XmlNode node in nodelist)
						cbc.Items.Add(Utility.GetXmlString(node, "SingerKey"));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private XmlDocument GetQueue(int Round)
		{
			if (_queuerForm != null && _queuerForm.cmb_events.Text != null && _queuerForm.cmb_events.Text.Trim().Length > 0)
			{
				TSCQueue q = new TSCQueue();
				q.EventKey = _queuerForm.cmb_events.Text;
				q.QueueRound = Round;
				return (_scc.GetTable("TSCQueue", q.EventKey));
			}
			return null;
		}

		private void LoadTSCQueue(XmlDocument doc, int iround)
		{
			gd_tscqueue = new BindingList<TSCQueue>();
			List<TSCQueue> gd_sort = new List<TSCQueue>();
			db_tscqueue = new List<TSCQueue>();
			if (doc != null)
			{
				XmlNode nodedata = doc.SelectSingleNode("/Root/Data");
				if (nodedata != null)
				{
					XmlNode noderound = nodedata.SelectSingleNode("QueueRound[@round='" + iround.ToString() + "']");
					XmlNodeList nodelist = noderound.SelectNodes("SingerKey");
					foreach (XmlNode node in nodelist)
					{
						TSCQueue tscqueue = new TSCQueue();
						tscqueue.EventKey = Utility.GetXmlString(nodedata, "EventKey");
						tscqueue.SingerKey = Utility.GetXmlString(node, "key", true);
						tscqueue.QueueRound = iround;
						tscqueue.QueueOrder = Utility.GetXmlInteger(node, "QueueOrder");
						tscqueue.QueueSong = Utility.GetXmlString(node, "QueueSong");
						tscqueue.QueueArtist = Utility.GetXmlString(node, "QueueArtist");
						tscqueue.QueueNote = Utility.GetXmlString(node, "QueueNote");
						tscqueue.QueueLink = Utility.GetXmlString(node, "QueueLink");
						tscqueue.QueueState = Utility.GetXmlString(node, "QueueState");
						gd_sort.Add(tscqueue);
					}
					foreach (TSCQueue tscqueue in gd_sort)
					{
						db_tscqueue.Add(new TSCQueue(tscqueue));
					}
				}
			}
			else
			{
			}
			gd_sort.Sort();
			foreach (TSCQueue t in gd_sort)
				gd_tscqueue.Add(t);
			dataGridViewQueue.DataSource = gd_tscqueue;
			dataGridViewQueue.Columns["EventKey"].DataPropertyName = "EventKey";
			dataGridViewQueue.Columns["SingerKey"].DataPropertyName = "SingerKey";
			dataGridViewQueue.Columns["QueueRound"].DataPropertyName = "QueueRound";
			dataGridViewQueue.Columns["QueueOrder"].DataPropertyName = "QueueOrder";
			dataGridViewQueue.Columns["QueueSong"].DataPropertyName = "QueueSong";
			dataGridViewQueue.Columns["QueueArtist"].DataPropertyName = "QueueArtist";
			dataGridViewQueue.Columns["QueueNote"].DataPropertyName = "QueueNote";
			dataGridViewQueue.Columns["QueueLink"].DataPropertyName = "QueueLink";
			dataGridViewQueue.Columns["QueueState"].DataPropertyName = "QueueState";
			SetHeaders();
		}

		private void SetHeaders()
		{
			dataGridViewQueue.Columns["EventKey"].HeaderText = "Event";
			dataGridViewQueue.Columns["SingerKey"].HeaderText = "Singer";
		}
		#endregion
		#region Refresh
		public void MasterRefresh()
		{
			if (_round == 1 && _queuerForm != null)
				_queuerForm.controlTag.RefreshQueue(gd_tscqueue);
		}

		public void RefreshQueue(BindingList<TSCQueue> list)
		{
			BindingList<TSCQueue> sync_tscqueue = new BindingList<TSCQueue>();
			if (list != null && list.Count > 0)
			{
				if (list[0].QueueRound == _round)
					return;
				foreach (TSCQueue tscqueue in list)
					AddQueue(sync_tscqueue, tscqueue);
				gd_tscqueue = sync_tscqueue;
				dataGridViewQueue.DataSource = gd_tscqueue;
				Save();
			}
			//else
			//{
			//	dataGridViewQueue.DataSource = sync_tscqueue;
			//}
		}

		private void AddQueue(BindingList<TSCQueue> sync_tscqueue, TSCQueue tscqueue)
		{
			foreach (TSCQueue q in gd_tscqueue)
			{
				if (q.SingerKey == tscqueue.SingerKey)
				{
					TSCQueue nq = new TSCQueue();
					nq.EventKey = q.EventKey;
					nq.SingerKey = q.SingerKey;
					nq.QueueRound = _round;
					nq.QueueOrder = tscqueue.QueueOrder;
					nq.QueueArtist = q.QueueArtist;
					nq.QueueLink = q.QueueLink;
					nq.QueueState = q.QueueState;
					nq.QueueNote = q.QueueNote;
					nq.QueueSong = q.QueueSong;
					sync_tscqueue.Add(nq);
					return;
				}
			}
			TSCQueue n = new TSCQueue();
			n.EventKey = tscqueue.EventKey;
			n.SingerKey = tscqueue.SingerKey;
			n.QueueRound = _round;
			n.QueueOrder = tscqueue.QueueOrder;
			n.QueueArtist = "";
			n.QueueLink = "";
			n.QueueState = "";
			n.QueueNote = "";
			n.QueueSong = "";
			sync_tscqueue.Add(n);
		}

		private string QueueKey(TSCQueue queue)
		{
			return string.Format("[{0}]-[{1}]-{2}", queue.EventKey, queue.SingerKey, queue.QueueRound.ToString());
		}

		private bool ValidQueueKey(TSCQueue queue)
		{
			if (_queuerForm != null && queue.SingerKey != null && queue.SingerKey.Trim().Length > 0)
			{
				queue.EventKey = _queuerForm.cmb_events.Text;
				queue.QueueRound = _round;
				return (queue.EventKey != null && queue.SingerKey != null && queue.QueueRound >= 0 && queue.EventKey.Trim().Length > 0 && queue.SingerKey.Trim().Length > 0);
			}
			return false;
		}
		#endregion
		#region events
		private void dataGridViewQueue_RowValidated(object sender, DataGridViewCellEventArgs e)
		{
			foreach (TSCQueue queue in gd_tscqueue)
				SetState(queue, db_tscqueue);
			if (e.RowIndex >= 0)
				if (e.RowIndex < gd_tscqueue.Count)
					SetRow(e.RowIndex, gd_tscqueue[e.RowIndex].state);
		}

		public void Save()
		{
			int recordsadded = 0;
			int recordschanged = 0;
			int recordsremoved = 0;

			if (IsValid())
			{
				foreach (TSCQueue queue in gd_tscqueue)
					SetState(queue, db_tscqueue);
				foreach (TSCQueue queue in db_tscqueue)
					SetState(queue, gd_tscqueue);
				foreach (TSCQueue queue in gd_tscqueue)
				{
					if (queue.state == 'I')
					{
						string xml = queue.GetDataXml();
						string result = _scc.GeneralStore("TSCQueue", _queuerForm.controlTag.queueXml, queue.EventKey, "INSERT", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Save error: " + QueueKey(queue) + " - " + result);
						}
						else
							recordsadded++;
					}
					else if (queue.state == 'U')
					{
						string xml = queue.GetDataXml();
						string result = _scc.GeneralStore("TSCQueue", _queuerForm.controlTag.queueXml, queue.EventKey, "UPDATE", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Save error: " + QueueKey(queue) + " - " + result);
						}
						else
							recordschanged++;
					}
				}
				foreach (TSCQueue queue in db_tscqueue)
				{
					if (queue.state == 'D')
					{
						string xml = queue.GetDataXml();
						string result = _scc.GeneralStore("TSCQueue", _queuerForm.controlTag.queueXml, queue.EventKey, "DELETE", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Delete error: " + QueueKey(queue) + " - " + result);
						}
						else
							recordsremoved++;
					}
				}
				if (_queuerForm != null && (recordsadded > 0 || recordschanged > 0 || recordsremoved > 0))
				{
					db_tscqueue.Clear();
					gd_tscqueue.Clear();
					TSCQueue templateQueue = new TSCQueue();
					templateQueue.EventKey = _queuerForm.cmb_events.Text;
					templateQueue.QueueRound = _round;
					_queuerForm.controlTag.queueXml = _scc.GetTable("TSCQueue", templateQueue.EventKey);
					LoadTSCQueue(_queuerForm.controlTag.queueXml, 1);

					RefreshGrid();
					if (_queuerForm != null)
					{
						_queuerForm.controlTag.RefreshQueue(gd_tscqueue);
						if (_queuerForm.controlTag != null)
							_queuerForm.controlTag.SetNext();
						_queuerForm.SetNext();
					}
				}
			}
		}

		private void contextMenuStripSave_Click(object sender, EventArgs e)
		{
			if (_queuerForm != null)
				_queuerForm.Save();
		}

		private void contextMenuStripSave_Opening(object sender, CancelEventArgs e)
		{
			if (_contextMenuStrip == null)
				e.Cancel = true;
		}

		private void dataGridViewQueue_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
		{
			if (e.ColumnIndex == -1 && e.RowIndex == -1)
			{
				_contextMenuStrip = contextMenuStripSave;
				e.ContextMenuStrip = contextMenuStripSave;
			}
			else if (e.ColumnIndex == -1 && e.RowIndex >= 0 && dataGridViewQueue.Rows[e.RowIndex].IsNewRow == false)
			{
				_rowIndex = e.RowIndex;
				_contextMenuStrip = contextMenuStripOrder;
				e.ContextMenuStrip = contextMenuStripOrder;
			}
			else
			{
				_contextMenuStrip = null;
				e.ContextMenuStrip = null;
			}
		}

		private void dataGridViewQueue_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
		{
			if (e.Row.Index > 0 && e.Row.Index == dataGridViewQueue.Rows.Count - 1)
				if (dataGridViewQueue.Rows[e.Row.Index - 1].Cells["QueueOrder"].Value == null || dataGridViewQueue.Rows[e.Row.Index - 1].Cells["QueueOrder"].Value.ToString() == "0")
					dataGridViewQueue.Rows[e.Row.Index - 1].Cells["QueueOrder"].Value = e.Row.Index;
		}

		private void singerHistoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_queuerForm != null && _rowIndex >= 0 && _rowIndex < dataGridViewQueue.Rows.Count)
			{
				string singerkey = dataGridViewQueue.Rows[_rowIndex].Cells["SingerKey"].Value.ToString();
				if (singerkey != null && singerkey.Trim().Length > 0)
				{
					SingerHistory sh = new SingerHistory();
					sh.SetUp(_queuerForm.scc, singerkey);
					sh.ShowDialog();
				}
			}
		}

		private void markToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_rowIndex > 0 && _rowIndex < gd_tscqueue.Count)
			{
				_markIndex = _rowIndex;
			}
		}

		private void insertToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_rowIndex >= 0 && _rowIndex < gd_tscqueue.Count && _markIndex >= 0 && _markIndex < gd_tscqueue.Count && _rowIndex != _markIndex)
			{
				int iremember = 0;
				int ix = 1;
				if (_rowIndex < _markIndex)
				{
					for (int i = 0; i < gd_tscqueue.Count; i++)
					{
						if (i == _rowIndex)
						{
							iremember = ix;
							dataGridViewQueue.Rows[i].Cells["QueueOrder"].Value = ix + 1;
							ix += 2;
						}
						else if (i == _markIndex)
						{
							dataGridViewQueue.Rows[i].Cells["QueueOrder"].Value = iremember;
							//ix += 1;
						}
						else
						{
							dataGridViewQueue.Rows[i].Cells["QueueOrder"].Value = ix;
							ix++;
						}
					}
					Save();
				}
				else
				{
					for (int i = 0; i < gd_tscqueue.Count; i++)
					{
						if (i == _rowIndex)
						{
							dataGridViewQueue.Rows[iremember].Cells["QueueOrder"].Value = ix + 1;
							dataGridViewQueue.Rows[i].Cells["QueueOrder"].Value = ix;
							ix += 2;
						}
						else if (i == _markIndex)
						{
							iremember = i;
						}
						else
						{
							dataGridViewQueue.Rows[i].Cells["QueueOrder"].Value = ix;
							ix++;
						}
					}
					Save();
				}
			}
		}

		private void toolStripMenuItemUp_Click(object sender, EventArgs e)
		{
			if (_rowIndex > 0 && _rowIndex < gd_tscqueue.Count)
			{
				int ix = 1;
				for (int i = 0; i < gd_tscqueue.Count; i++)
				{
					if (i == _rowIndex - 1)
					{
						dataGridViewQueue.Rows[i].Cells["QueueOrder"].Value = ix + 1;
						ix += 2;
					}
					else if (i == _rowIndex)
					{
						dataGridViewQueue.Rows[i].Cells["QueueOrder"].Value = ix - 2;
					}
					else
					{
						dataGridViewQueue.Rows[i].Cells["QueueOrder"].Value = ix;
						ix++;
					}
				}
				Save();
			}
		}

		private void toolStripMenuItemDown_Click(object sender, EventArgs e)
		{
			if (_rowIndex >= 0 && _rowIndex < gd_tscqueue.Count - 1)
			{
				int ix = 1;
				for (int i = 0; i < gd_tscqueue.Count; i++)
				{
					if (i == _rowIndex)
					{
						dataGridViewQueue.Rows[i].Cells["QueueOrder"].Value = ix + 1;
						ix++;
					}
					else if (i == _rowIndex + 1)
					{
						dataGridViewQueue.Rows[i].Cells["QueueOrder"].Value = ix - 1;
						ix++;
					}
					else
					{
						dataGridViewQueue.Rows[i].Cells["QueueOrder"].Value = ix;
						ix++;
					}
				}
				Save();
			}
		}
		#endregion
		#region state
		private void RefreshGrid()
		{
			for (int i = 0; i < gd_tscqueue.Count; i++)
			{
				SetRow(i, gd_tscqueue[i].state);
			}
		}

		private void SetRow(int row, char state)
		{
			if (state == 'I')
				dataGridViewQueue.Rows[row].DefaultCellStyle = addstyle;
			else if (state == 'U')
				dataGridViewQueue.Rows[row].DefaultCellStyle = changestyle;
			else
				dataGridViewQueue.Rows[row].DefaultCellStyle = defaultstyle;
		}

		private bool IsNumber(string number)
		{
			if (number == null || number.Trim().Length == 0)
				return true;
			foreach (char c in number)
				if (!char.IsNumber(c))
					return false;
			return true;
		}

		private bool IsValid()
		{
			if (gd_tscqueue.Count < 2)
				return true;
			for (int i = 0; i < gd_tscqueue.Count - 1; i++)
			{
				if (ValidQueueKey(gd_tscqueue[i]) == false)
				{
					MessageBox.Show("Key field cannot be empty");
					return false;
				}
			}
			List<string> keys = new List<string>();
			foreach (TSCQueue queue in gd_tscqueue)
			{
				if (ValidQueueKey(queue) == true && keys.Contains(QueueKey(queue).ToLower()))
				{
					MessageBox.Show("Duplicate key found: " + QueueKey(queue));
					return false;
				}
				else
					if (ValidQueueKey(queue) == true)
						keys.Add(QueueKey(queue).ToLower());
			}
			return true;
		}

		private void SetState(TSCQueue queue, BindingList<TSCQueue> list)
		{
			bool bFound = false;
			if (ValidQueueKey(queue) == true)
			{
				foreach (TSCQueue s in list)
				{
					if (ValidQueueKey(s) == true && s.KeyEquals(queue))
					{
						bFound = true;
						if (s == queue)
							queue.state = '=';
						else
							queue.state = 'U';
					}
				}
			}
			else
			{
				queue.state = '?';
				return;
			}
			if (bFound == false)
				queue.state = 'D';
		}

		private void SetState(TSCQueue queue, List<TSCQueue> list)
		{
			if (ValidQueueKey(queue) == false)
				return;
			bool bFound = false;
			foreach (TSCQueue s in list)
			{
				if (s.KeyEquals(queue))
				{
					bFound = true;
					if (s == queue)
						queue.state = '=';
					else
						queue.state = 'U';
				}
			}
			if (bFound == false)
				queue.state = 'I';
		}

		private void dataGridViewQueue_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex == -1 && e.RowIndex >= 0)
			{
				Brush brush = Brushes.Gray;
				DataGridViewRow row = dataGridViewQueue.Rows[e.RowIndex];
				if (row.Cells["QueueState"].Value != null)
				{
					string state = row.Cells["QueueState"].Value.ToString();
					if (state.Trim().ToLower() == "finished")
						brush = Brushes.Green;
					else if (state.Trim().ToLower() == "gone home")
						brush = Brushes.Black;
					else if (state.Trim().ToLower() == "pending")
						brush = Brushes.Gray;
					else if (state.Trim().ToLower() == "not here")
						brush = Brushes.LightGreen;
					else
						brush = Brushes.Gray;
				}

				//StringFormat sf = new StringFormat();
				//sf.Alignment = StringAlignment.Center;
				//sf.LineAlignment = StringAlignment.Center;
				e.Graphics.FillRectangle(brush, e.CellBounds);
				e.Graphics.DrawRectangle(Pens.Black, e.CellBounds);
				//SolidBrush br = new SolidBrush(e.CellStyle.ForeColor);
				//if (dataGridViewQueue.Rows[e.RowIndex].Selected)
				//{
				//	br = new SolidBrush(Color.White);
				//}
				//e.Graphics.DrawString(dt1.Columns[e.RowIndex].ColumnName.ToString(), this.Font, br, e.CellBounds, sf );
				e.Handled = true;
				//br.Dispose();
			}
		}

		private void finishedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_rowIndex >= 0 && _rowIndex < gd_tscqueue.Count)
			{
				if (dataGridViewQueue.SelectedRows.Count > 1)
				{
					List<int> rows = new List<int>();
					foreach (DataGridViewRow row in (dataGridViewQueue.SelectedRows))
						rows.Add(row.Index);
					foreach (int irow in rows)
						SetFinished(irow);
				}
				else
					SetFinished(_rowIndex);
			}
		}

		public void SetFinished(TSCQueue q)
		{
			if (gd_tscqueue != null && gd_tscqueue.Count > 0)
			{
				for (int ix = 0; ix < gd_tscqueue.Count; ix++)
				{
					TSCQueue tq = gd_tscqueue[ix];
					if (tq.KeyEquals(q))
					{
						SetFinished(ix);
						break;
					}
				}
			}
		}

		private void SetFinished(int rowIndex)
		{
			if (rowIndex >= 0 && rowIndex < gd_tscqueue.Count && gd_tscqueue[rowIndex].EventKey != null && gd_tscqueue[rowIndex].EventKey.Trim().Length > 0)
			{
				if (gd_tscqueue[rowIndex].QueueState.Trim().ToLower() != "finished")
				{
					string singerkey = gd_tscqueue[rowIndex].SingerKey;
					gd_tscqueue[rowIndex].QueueState = "Finished";
					dataGridViewQueue.Refresh();
					Save();
					if (_queuerForm != null)
					{
						_queuerForm.controlTag.SetNext();
						_queuerForm.SetNext();
						_queuerForm.UpdateSingerActivity(singerkey);
					}
				}
			}
		}

		public void SetNotHere(TSCQueue q)
		{
			if (gd_tscqueue != null && gd_tscqueue.Count > 0)
			{
				for (int ix = 0; ix < gd_tscqueue.Count; ix++)
				{
					TSCQueue tq = gd_tscqueue[ix];
					if (tq.KeyEquals(q))
					{
						SetNotHere(ix);
						break;
					}
				}
			}
		}

		private void SetNotHere(int rowIndex)
		{
			if (rowIndex >= 0 && rowIndex < gd_tscqueue.Count && gd_tscqueue[rowIndex].EventKey != null && gd_tscqueue[rowIndex].EventKey.Trim().Length > 0)
			{
				if (gd_tscqueue[rowIndex].QueueState.Trim().ToLower() != "not here")
				{
					gd_tscqueue[rowIndex].QueueState = "not here";
					dataGridViewQueue.Refresh();
					Save();
					if (_queuerForm != null)
					{
						_queuerForm.controlTag.SetNext();
						_queuerForm.SetNext();
					}
				}
			}
		}

		private void goneHomeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_rowIndex >= 0 && _rowIndex < gd_tscqueue.Count)
			{
				if (gd_tscqueue[_rowIndex].EventKey != null && gd_tscqueue[_rowIndex].EventKey.Trim().Length > 0 && gd_tscqueue[_rowIndex].SingerKey.Trim().Length > 0)
				{
					if (_queuerForm != null)
						_queuerForm.SetGoneHome(gd_tscqueue[_rowIndex].SingerKey);
				}
			}
		}

		public void SetGoneHome(string SingerKey)
		{
			foreach (TSCQueue queue in gd_tscqueue)
			{
				if (queue.SingerKey.Trim().ToLower() == SingerKey.Trim().ToLower())
				{
					if (queue.EventKey != null && queue.EventKey.Trim().Length > 0)
					{
						if (queue.QueueState.Trim().Length == 0 || queue.QueueState.Trim().ToLower() == "pending")
						{
							queue.QueueState = "Gone Home";
							dataGridViewQueue.Refresh();
							Save();
							if (_queuerForm != null)
							{
								_queuerForm.controlTag.SetNext();
								_queuerForm.SetNext();
							}
							break;
						}
					}
				}
			}
		}

		private void pendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_rowIndex >= 0 && _rowIndex < gd_tscqueue.Count)
			{
				if (dataGridViewQueue.SelectedRows.Count > 1)
				{
					List<int> rows = new List<int>();
					foreach (DataGridViewRow row in (dataGridViewQueue.SelectedRows))
						rows.Add(row.Index);
					foreach (int irow in rows)
						SetPending(irow);
				}
				else
					SetPending(_rowIndex);
			}
		}

		private void SetPending(int rowIndex)
		{
			if (rowIndex >= 0 && rowIndex < gd_tscqueue.Count)
			{
				if (gd_tscqueue[rowIndex].EventKey != null && gd_tscqueue[rowIndex].EventKey.Trim().Length > 0)
				{
					if (gd_tscqueue[rowIndex].QueueState.Trim().Length == 0 || gd_tscqueue[rowIndex].QueueState.Trim().ToLower() != "pending")
					{
						gd_tscqueue[rowIndex].QueueState = "Pending";
						dataGridViewQueue.Refresh();
						Save();
						if (_queuerForm != null)
						{
							_queuerForm.controlTag.SetNext();
							_queuerForm.SetNext();
						}
					}
				}
			}
		}

		private void notHereToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_rowIndex >= 0 && _rowIndex < gd_tscqueue.Count)
			{
				if (gd_tscqueue[_rowIndex].EventKey != null && gd_tscqueue[_rowIndex].EventKey.Trim().Length > 0)
				{
					if (gd_tscqueue[_rowIndex].QueueState.Trim().ToLower() != "not here")
					{
						gd_tscqueue[_rowIndex].QueueState = "Not Here";
						dataGridViewQueue.Refresh();
						Save();
						if (_queuerForm != null)
						{
							_queuerForm.controlTag.SetNext();
							_queuerForm.SetNext();
						}
					}
				}
			}
		}
		#endregion
		#region paste and launch
		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_queuerForm != null && Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true)
			{
				_queuerForm.txt_invisible.Text = "";
				_queuerForm.txt_invisible.Paste();
				string[] clip = _queuerForm.txt_invisible.Text.Split('	');
				if (clip.Length > 2)
				{
					if (clip.Length == 4)
					{
						dataGridViewQueue.Rows[_rowIndex].Cells["QueueSong"].Value = clip[0];
						dataGridViewQueue.Rows[_rowIndex].Cells["QueueArtist"].Value = clip[1];
						dataGridViewQueue.Rows[_rowIndex].Cells["QueueNote"].Value = clip[2];
						dataGridViewQueue.Rows[_rowIndex].Cells["QueueLink"].Value = clip[3];
					}
					else if (IsLink(clip[clip.Length - 1]))
					{
						dataGridViewQueue.Rows[_rowIndex].Cells["QueueSong"].Value = clip[0];
						dataGridViewQueue.Rows[_rowIndex].Cells["QueueArtist"].Value = clip[1];
						dataGridViewQueue.Rows[_rowIndex].Cells["QueueLink"].Value = clip[clip.Length - 1];
					}
				}
				else
				{
					if (IsLink(clip[0]))
					{
						dataGridViewQueue.Rows[_rowIndex].Cells["QueueLink"].Value = clip[0];
					}
				}
			}
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_queuerForm != null)
			{
				string song = "";
				string artist = "";
				string note = "";
				string link = "";
				if (dataGridViewQueue.Rows[_rowIndex].Cells["QueueSong"].Value != null)
					song = dataGridViewQueue.Rows[_rowIndex].Cells["QueueSong"].Value.ToString().Trim();
				if (dataGridViewQueue.Rows[_rowIndex].Cells["QueueArtist"].Value != null)
					artist = dataGridViewQueue.Rows[_rowIndex].Cells["QueueArtist"].Value.ToString().Trim();
				if (dataGridViewQueue.Rows[_rowIndex].Cells["QueueNote"].Value != null)
					note = dataGridViewQueue.Rows[_rowIndex].Cells["QueueNote"].Value.ToString().Trim();
				if (dataGridViewQueue.Rows[_rowIndex].Cells["QueueLink"].Value != null)
					link = dataGridViewQueue.Rows[_rowIndex].Cells["QueueLink"].Value.ToString().Trim();

				if (song.Length > 0 || artist.Length > 0 || note.Length > 0 || link.Length > 0)
				{
					Clipboard.Clear();
					string cells = string.Format("{0}	{1}	{2}	{3}", song, artist, note, link);
					Clipboard.SetText(cells);
				}
			}
		}

		private void launchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_queuerForm != null && _rowIndex >= 0 && _rowIndex < dataGridViewQueue.Rows.Count)
			{
				string link = dataGridViewQueue.Rows[_rowIndex].Cells["QueueLink"].Value.ToString();
				if (link != null && link.Trim().Length > 0)
				{
					Launch(link);
				}
			}
		}

		private bool IsLink(string link)
		{
			return (link.Contains('/') || link.Contains('\\'));
		}

		private void contextMenuStripOrder_Opening(object sender, CancelEventArgs e)
		{
			pasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Text);
		}

		private void dataGridViewQueue_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.ColumnIndex == dataGridViewQueue.Columns["QueueLink"].Index)
			{
				Launch(dataGridViewQueue.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
			}
		}

		public void Launch(string path)
		{
			if (path.Trim().Length > 0 && IsLink(path) == true)
			{
				LaunchForm lf = new LaunchForm(path);
				lf.Show();
			}
		}
		#endregion
	}
}
