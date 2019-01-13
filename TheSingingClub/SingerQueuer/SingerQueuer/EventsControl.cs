using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

using XmlUtility;

namespace SingerQueuer
{
	public partial class EventsControl : UserControl
	{
		#region member data
		public DataGridView eventsDataGrid;
		public BindingList<TSCEvents> gd_tscevents = null;
		public List<TSCEvents> db_tscevents = null;

		private SingerDatabase _scc = null;
		private DataGridViewCellStyle defaultstyle;
		private DataGridViewCellStyle addstyle;
		private DataGridViewCellStyle changestyle;

		private QueuerForm _queuerForm = null;
		#endregion
		#region constructors
		public EventsControl()
		{
			InitializeComponent();
			eventsDataGrid = dataGridViewEvents;

		}
		#endregion
		#region events
		private void EventsControl_Load(object sender, EventArgs e)
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
				_queuerForm.controlTag.eventsControl = this;

				dataGridViewEvents.AutoGenerateColumns = false;

				if (_queuerForm == null || _queuerForm.controlTag == null || _queuerForm.controlTag.singersXml == null || _queuerForm.controlTag.singersXml.Trim().Length == 0)
					return;
				LoadVenues();
				_scc = _queuerForm.scc;
				string xml = _queuerForm.controlTag.eventsXml;
				LoadTSCEvents(xml);
			}
			else
				_scc = new SingerDatabase();
		}

		public void LoadVenues()
		{
			if (_queuerForm == null || _queuerForm.controlTag == null || _queuerForm.controlTag.singersXml == null || _queuerForm.controlTag.singersXml.Trim().Length == 0)
				return;
			DataGridViewComboBoxColumn cbc = dataGridViewEvents.Columns["VenueKey"] as DataGridViewComboBoxColumn;
			cbc.Items.Clear();

			string venuesXml = _queuerForm.controlTag.venuesXml;
			try
			{
				if (venuesXml != null && venuesXml.Trim().Length > 0)
				{
					XmlDocument doc = new XmlDocument();
					doc.LoadXml(venuesXml);
					XmlNodeList nodelist = doc.SelectNodes("/Root/Data");
					foreach (XmlNode node in nodelist)
						cbc.Items.Add(Utility.GetXmlString(node, "VenueKey"));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void LoadTSCEvents(string xml)
		{
			gd_tscevents = new BindingList<TSCEvents>();
			db_tscevents = new List<TSCEvents>();
			if (xml != null && xml.Trim().Length > 0)
			{
				if (Utility.IsValidXml(xml))
				{
					XmlDocument doc = new XmlDocument();
					doc.LoadXml(xml);
					XmlNodeList nodelist = doc.SelectNodes("/Root/Data");
					if (nodelist != null)
					{
						foreach (XmlNode node in nodelist)
						{
							TSCEvents tscevents = new TSCEvents();
							tscevents.EventKey = Utility.GetXmlString(node, "EventKey");
							tscevents.EventName = Utility.GetXmlString(node, "EventName");
							tscevents.VenueKey = Utility.GetXmlString(node, "VenueKey");
							tscevents.EventDate = Utility.GetXmlDateTime(node, "EventDate");
							tscevents.EventEmail = Utility.GetXmlString(node, "EventEmail");
							tscevents.EventAddress = Utility.GetXmlString(node, "EventAddress");
							gd_tscevents.Add(tscevents);
						}
						foreach (TSCEvents tscevents in gd_tscevents)
						{
							db_tscevents.Add(new TSCEvents(tscevents));
						}
					}
				}
				else
				{
					MessageBox.Show(xml);
				}
			}
			else
			{
			}
			dataGridViewEvents.DataSource = gd_tscevents;
			dataGridViewEvents.Columns["EventKey"].DataPropertyName = "EventKey";
			dataGridViewEvents.Columns["EventName"].DataPropertyName = "EventName";
			dataGridViewEvents.Columns["VenueKey"].DataPropertyName = "VenueKey";
			dataGridViewEvents.Columns["EventDate"].DataPropertyName = "EventDate";
			dataGridViewEvents.Columns["EventEmail"].DataPropertyName = "EventEmail";
			dataGridViewEvents.Columns["EventAddress"].DataPropertyName = "EventAddress";
			SetHeaders();
		}

		private void SetHeaders()
		{
			//dataGridViewEvents.Columns["VenueKey"].HeaderText = "Venue";
		}

		private void dataGridViewEvents_RowValidated(object sender, DataGridViewCellEventArgs e)
		{
			foreach (TSCEvents tscevent in gd_tscevents)
				SetState(tscevent, db_tscevents);
			if (e.RowIndex >= 0)
				if (e.RowIndex < gd_tscevents.Count)
					SetRow(e.RowIndex, gd_tscevents[e.RowIndex].state);
		}
		#endregion
		#region state
		private void RefreshGrid()
		{
			for (int i = 0; i < gd_tscevents.Count; i++)
			{
				SetRow(i, gd_tscevents[i].state);
			}
		}

		private void SetRow(int row, char state)
		{
			if (state == 'I')
				dataGridViewEvents.Rows[row].DefaultCellStyle = addstyle;
			else if (state == 'U')
				dataGridViewEvents.Rows[row].DefaultCellStyle = changestyle;
			else
				dataGridViewEvents.Rows[row].DefaultCellStyle = defaultstyle;
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
			if (gd_tscevents.Count < 2)
				return true;
			for (int i = 0; i < gd_tscevents.Count - 1; i++)
				if (gd_tscevents[i].EventKey == null && gd_tscevents[i].EventKey.Trim().Length == 0)
				{
					MessageBox.Show("Key field cannot be empty");
					return false;
				}
			List<string> keys = new List<string>();
			foreach (TSCEvents tscevent in gd_tscevents)
			{
				if (tscevent.EventKey != null && keys.Contains(tscevent.EventKey.Trim().ToLower()))
				{
					MessageBox.Show("Duplicate key found: " + tscevent.EventKey);
					return false;
				}
				else
					if (tscevent.EventKey != null)
						keys.Add(tscevent.EventKey.Trim().ToLower());
			}
			return true;
		}

		private void SetState(TSCEvents tscevent, BindingList<TSCEvents> list)
		{
			bool bFound = false;
			if (tscevent.EventKey != null && tscevent.EventKey.Trim().Length > 0)
			{
				foreach (TSCEvents s in list)
				{
					if (s.EventKey != null && s.EventKey.Trim().Length > 0 && s.KeyEquals(tscevent))
					{
						bFound = true;
						if (s == tscevent)
							tscevent.state = '=';
						else
							tscevent.state = 'U';
					}
				}
			}
			else
			{
				tscevent.state = '?';
				return;
			}
			if (bFound == false)
				tscevent.state = 'D';
		}

		private void SetState(TSCEvents tscevent, List<TSCEvents> list)
		{
			if (tscevent.EventKey == null || tscevent.EventKey.Trim().Length == 0)
				return;
			bool bFound = false;
			foreach (TSCEvents s in list)
			{
				if (s.KeyEquals(tscevent))
				{
					bFound = true;
					if (s == tscevent)
						tscevent.state = '=';
					else
						tscevent.state = 'U';
				}
			}
			if (bFound == false)
				tscevent.state = 'I';
		}
		#endregion

		private void contextMenuStripSave_Click(object sender, EventArgs e)
		{
			int recordsadded = 0;
			int recordschanged = 0;
			int recordsremoved = 0;

			if (IsValid())
			{
				foreach (TSCEvents tscevent in gd_tscevents)
					SetState(tscevent, db_tscevents);
				foreach (TSCEvents tscevent in db_tscevents)
					SetState(tscevent, gd_tscevents);
				foreach (TSCEvents tscevent in gd_tscevents)
				{
					if (tscevent.state == 'I')
					{
						string xml = tscevent.GetDataXml();
						string result = _scc.GeneralStore("TSCEvents", "INSERT", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Save error: " + tscevent.EventKey + " - " + result);
						}
						else
							recordsadded++;
					}
					else if (tscevent.state == 'U')
					{
						string xml = tscevent.GetDataXml();
						string result = _scc.GeneralStore("TSCEvents", "UPDATE", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Save error: " + tscevent.EventKey + " - " + result);
						}
						else
							recordschanged++;
					}
				}
				foreach (TSCEvents tscevent in db_tscevents)
				{
					if (tscevent.state == 'D')
					{
						string xml = tscevent.GetDataXml();
						string result = _scc.GeneralStore("TSCEvents", "DELETE", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Delete error: " + tscevent.EventKey + " - " + result);
						}
						else
							recordsremoved++;
					}
				}
				if (recordsadded > 0 || recordschanged > 0 || recordsremoved > 0)
				{
					db_tscevents.Clear();
					gd_tscevents.Clear();
					string xml = _scc.GeneralStore("TSCEvents", "GET", (new TSCEvents()).GetDataXml());
					LoadTSCEvents(xml);
					RefreshGrid();
					if (_queuerForm != null)
						_queuerForm.ReloadEvents();
				}
			}
		}

		private void dataGridViewEvents_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
		{
			if (e.ColumnIndex == -1 && e.RowIndex == -1)
				e.ContextMenuStrip = contextMenuStripSave;
			else
				e.ContextMenuStrip = null;
		}
	}
}
