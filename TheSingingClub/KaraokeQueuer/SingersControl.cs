using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using XmlUtility;

namespace KaraokeQueuer
{
	public partial class SingersControl : UserControl
	{
		#region member data
		public DataGridView singersDataGrid;
		public BindingList<TSCSingers> gd_tscsingers = null;
		public List<TSCSingers> db_tscsingers = null;

		private SingerDatabase _scc = null;
		private DataGridViewCellStyle defaultstyle;
		private DataGridViewCellStyle addstyle;
		private DataGridViewCellStyle changestyle;

		private QueuerForm _queuerForm = null;
		#endregion
		#region constructors
		public SingersControl()
		{
			InitializeComponent();
			singersDataGrid = dataGridViewSingers;
		}
		#endregion
		#region events
		private void SingersControl_Load(object sender, EventArgs e)
		{
			defaultstyle = new DataGridViewCellStyle();
			defaultstyle.ForeColor = Color.Black;
			addstyle = new DataGridViewCellStyle();
			addstyle.ForeColor = Color.DarkGreen;
			changestyle = new DataGridViewCellStyle();
			changestyle.ForeColor = Color.DarkBlue;

			_queuerForm = Tag as QueuerForm;
			if (_queuerForm != null)
			{
				(Tag as QueuerForm).controlTag.signersControl = this;

				dataGridViewSingers.AutoGenerateColumns = false;

				_scc = _queuerForm.scc;
				LoadTSCSingers(_queuerForm.controlTag.singersXml);
			}
			else
				_scc = new SingerDatabase();
		}

		private void LoadTSCSingers(XmlDocument doc)
		{
			gd_tscsingers = new BindingList<TSCSingers>();
			db_tscsingers = new List<TSCSingers>();
			if (doc != null)
			{
				XmlNodeList nodelist = doc.SelectNodes("/Root/Data");
				if (nodelist != null)
				{
					foreach (XmlNode node in nodelist)
					{
						TSCSingers tscsingers = new TSCSingers();
						tscsingers.SingerKey = Utility.GetXmlString(node, "SingerKey");
						tscsingers.SingerName = Utility.GetXmlString(node, "SingerName");
						tscsingers.SingerEmail = Utility.GetXmlString(node, "SingerEmail");
						tscsingers.SingerActivity = Utility.GetXmlInteger(node, "SingerActivity");
						gd_tscsingers.Add(tscsingers);
					}
					foreach (TSCSingers tscsingers in gd_tscsingers)
					{
						db_tscsingers.Add(new TSCSingers(tscsingers));
					}
				}
			}
			else
			{
			}
			dataGridViewSingers.DataSource = gd_tscsingers;
			dataGridViewSingers.Columns["SingerKey"].DataPropertyName = "SingerKey";
			dataGridViewSingers.Columns["SingerName"].DataPropertyName = "SingerName";
			dataGridViewSingers.Columns["SingerEmail"].DataPropertyName = "SingerEmail";
			dataGridViewSingers.Columns["SingerActivity"].DataPropertyName = "SingerActivity";
			SetHeaders();
		}

		private void SetHeaders()
		{
		}

		private void dataGridViewSingers_RowValidated(object sender, DataGridViewCellEventArgs e)
		{
			foreach (TSCSingers singer in gd_tscsingers)
				SetState(singer, db_tscsingers);
			if (e.RowIndex >= 0)
				if (e.RowIndex < gd_tscsingers.Count)
					SetRow(e.RowIndex, gd_tscsingers[e.RowIndex].state);
		}
		#endregion
		#region state
		private void RefreshGrid()
		{
			for (int i = 0; i < gd_tscsingers.Count; i++)
			{
				SetRow(i, gd_tscsingers[i].state);
			}
		}

		private void SetRow(int row, char state)
		{
			if (state == 'I')
				dataGridViewSingers.Rows[row].DefaultCellStyle = addstyle;
			else if (state == 'U')
				dataGridViewSingers.Rows[row].DefaultCellStyle = changestyle;
			else
				dataGridViewSingers.Rows[row].DefaultCellStyle = defaultstyle;
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
			if (gd_tscsingers.Count < 2)
				return true;
			for (int i = 0; i < gd_tscsingers.Count - 1; i++)
				if (gd_tscsingers[i].SingerKey == null && gd_tscsingers[i].SingerKey.Trim().Length == 0)
				{
					MessageBox.Show("Key field cannot be empty");
					return false;
				}
			List<string> keys = new List<string>();
			foreach (TSCSingers singer in gd_tscsingers)
			{
				if (singer.SingerKey != null && keys.Contains(singer.SingerKey.Trim().ToLower()))
				{
					MessageBox.Show("Duplicate key found: " + singer.SingerKey);
					return false;
				}
				else
					if (singer.SingerKey != null)
						keys.Add(singer.SingerKey.Trim().ToLower());
			}
			return true;
		}

		private void SetState(TSCSingers singer, BindingList<TSCSingers> list)
		{
			bool bFound = false;
			if (singer.SingerKey != null && singer.SingerKey.Trim().Length > 0)
			{
				foreach (TSCSingers s in list)
				{
					if (s.SingerKey != null && s.SingerKey.Trim().Length > 0 && s.KeyEquals(singer))
					{
						bFound = true;
						if (s == singer)
							singer.state = '=';
						else
							singer.state = 'U';
					}
				}
			}
			else
			{
				singer.state = '?';
				return;
			}
			if (bFound == false)
				singer.state = 'D';
		}

		private void SetState(TSCSingers singer, List<TSCSingers> list)
		{
			if (singer.SingerKey == null || singer.SingerKey.Trim().Length == 0)
				return;
			bool bFound = false;
			foreach (TSCSingers s in list)
			{
				if (s.KeyEquals(singer))
				{
					bFound = true;
					if (s == singer)
						singer.state = '=';
					else
						singer.state = 'U';
				}
			}
			if (bFound == false)
				singer.state = 'I';
		}

		public void UpdateSingerActivity(string singerkey)
		{
			if (singerkey != null && singerkey.Trim().Length > 0 && db_tscsingers != null && db_tscsingers.Count > 0)
			{
				foreach (TSCSingers singer in db_tscsingers)
				{
					if (singer.SingerKey != null && singer.SingerKey.Trim().Length > 0)
					{
						if (singer.SingerKey.Trim().ToLower() == singerkey.Trim().ToLower())
						{
							singer.SingerActivity += 1;
							string xml = singer.GetDataXml();
							_scc.GeneralStore("TSCSingers", _queuerForm.controlTag.singersXml, "UPDATE", xml);
							dataGridViewSingers.Refresh();
						}
					}
				}
			}
		}
		#endregion

		private void contextMenuStripSave_Click(object sender, EventArgs e)
		{
			int recordsadded = 0;
			int recordschanged = 0;
			int recordsremoved = 0;

			if (IsValid())
			{
				foreach (TSCSingers singer in gd_tscsingers)
					SetState(singer, db_tscsingers);
				foreach (TSCSingers singer in db_tscsingers)
					SetState(singer, gd_tscsingers);
				foreach (TSCSingers singer in gd_tscsingers)
				{
					if (singer.state == 'I')
					{
						string xml = singer.GetDataXml();
						string result = _scc.GeneralStore("TSCSingers", _queuerForm.controlTag.singersXml, "INSERT", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Save error: " + singer.SingerKey + " - " + result);
						}
						else
							recordsadded++;
					}
					else if (singer.state == 'U')
					{
						string xml = singer.GetDataXml();
						string result = _scc.GeneralStore("TSCSingers", _queuerForm.controlTag.singersXml, "UPDATE", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Save error: " + singer.SingerKey + " - " + result);
						}
						else
							recordschanged++;
					}
				}
				foreach (TSCSingers singer in db_tscsingers)
				{
					if (singer.state == 'D')
					{
						string xml = singer.GetDataXml();
						string result = _scc.GeneralStore("TSCSingers", _queuerForm.controlTag.singersXml, "DELETE", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Delete error: " + singer.SingerKey + " - " + result);
						}
						else
							recordsremoved++;
					}
				}
				if (recordsadded > 0 || recordschanged > 0 || recordsremoved > 0)
				{
					db_tscsingers.Clear();
					gd_tscsingers.Clear();
					if (_queuerForm != null)
					{
						_queuerForm.controlTag.venuesXml = _scc.GetTable("TSCSingers", "");
						LoadTSCSingers(_queuerForm.controlTag.venuesXml);
						RefreshGrid();
						_queuerForm.ReloadSingers();
					}
				}
			}
		}

		private void dataGridViewSingers_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
		{
			if (e.ColumnIndex == -1 && e.RowIndex == -1)
				e.ContextMenuStrip = contextMenuStripSave;
			else
				e.ContextMenuStrip = null;
		}
	}
}
