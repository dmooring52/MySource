using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

using XmlUtility;


namespace KaraokeQueuer
{
	public partial class VenuesControl : UserControl
	{
		#region member data
		public DataGridView venuesDataGrid;
		public BindingList<TSCVenues> gd_tscvenues = null;
		public List<TSCVenues> db_tscvenues = null;

		private SingerDatabase _scc = null;
		private DataGridViewCellStyle defaultstyle;
		private DataGridViewCellStyle addstyle;
		private DataGridViewCellStyle changestyle;

		private QueuerForm _queuerForm = null;
		#endregion
		#region constructors
		public VenuesControl()
		{
			InitializeComponent();
			venuesDataGrid = dataGridViewVenues;
		}
		#endregion
		#region events
		private void VenuesControl_Load(object sender, EventArgs e)
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
				(Tag as QueuerForm).controlTag.venuesControl = this;

				dataGridViewVenues.AutoGenerateColumns = false;

				_scc = _queuerForm.scc;
				LoadTSCVenues(_queuerForm.controlTag.venuesXml);
			}
			else
				_scc = new SingerDatabase();
		}

		private void LoadTSCVenues(XmlDocument doc)
		{
			gd_tscvenues = new BindingList<TSCVenues>();
			db_tscvenues = new List<TSCVenues>();
			if (doc != null)
			{
				XmlNodeList nodelist = doc.SelectNodes("/Root/Data");
				if (nodelist != null)
				{
					foreach (XmlNode node in nodelist)
					{
						TSCVenues tscvenues = new TSCVenues();
						tscvenues.VenueKey = Utility.GetXmlString(node, "VenueKey");
						tscvenues.VenueName = Utility.GetXmlString(node, "VenueName");
						tscvenues.VenueEmail = Utility.GetXmlString(node, "VenueEmail");
						tscvenues.VenueAddress = Utility.GetXmlString(node, "VenueAddress");
						tscvenues.VenueContact = Utility.GetXmlString(node, "VenueContact");
						tscvenues.VenuePhone = Utility.GetXmlString(node, "VenuePhone");
						gd_tscvenues.Add(tscvenues);
					}
					foreach (TSCVenues tscvenues in gd_tscvenues)
					{
						db_tscvenues.Add(new TSCVenues(tscvenues));
					}
				}
			}
			else
			{
			}
			dataGridViewVenues.DataSource = gd_tscvenues;
			dataGridViewVenues.Columns["VenueKey"].DataPropertyName = "VenueKey";
			dataGridViewVenues.Columns["VenueName"].DataPropertyName = "VenueName";
			dataGridViewVenues.Columns["VenueEmail"].DataPropertyName = "VenueEmail";
			dataGridViewVenues.Columns["VenueAddress"].DataPropertyName = "VenueAddress";
			dataGridViewVenues.Columns["VenueContact"].DataPropertyName = "VenueContact";
			dataGridViewVenues.Columns["VenuePhone"].DataPropertyName = "VenuePhone";
			SetHeaders();
		}

		private void SetHeaders()
		{
		}

		private void dataGridViewVenues_RowValidated(object sender, DataGridViewCellEventArgs e)
		{
			foreach (TSCVenues venue in gd_tscvenues)
				SetState(venue, db_tscvenues);
			if (e.RowIndex >= 0)
				if (e.RowIndex < gd_tscvenues.Count)
					SetRow(e.RowIndex, gd_tscvenues[e.RowIndex].state);
		}
		#endregion
		#region state
		private void RefreshGrid()
		{
			for (int i = 0; i < gd_tscvenues.Count; i++)
			{
				SetRow(i, gd_tscvenues[i].state);
			}
		}

		private void SetRow(int row, char state)
		{
			if (state == 'I')
				dataGridViewVenues.Rows[row].DefaultCellStyle = addstyle;
			else if (state == 'U')
				dataGridViewVenues.Rows[row].DefaultCellStyle = changestyle;
			else
				dataGridViewVenues.Rows[row].DefaultCellStyle = defaultstyle;
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
			if (gd_tscvenues.Count < 2)
				return true;
			for (int i = 0; i < gd_tscvenues.Count - 1; i++)
				if (gd_tscvenues[i].VenueKey == null && gd_tscvenues[i].VenueKey.Trim().Length == 0)
				{
					MessageBox.Show("Key field cannot be empty");
					return false;
				}
			List<string> keys = new List<string>();
			foreach (TSCVenues venue in gd_tscvenues)
			{
				if (venue.VenueKey != null && keys.Contains(venue.VenueKey.Trim().ToLower()))
				{
					MessageBox.Show("Duplicate key found: " + venue.VenueKey);
					return false;
				}
				else
					if (venue.VenueKey != null)
						keys.Add(venue.VenueKey.Trim().ToLower());
			}
			return true;
		}

		private void SetState(TSCVenues venue, BindingList<TSCVenues> list)
		{
			bool bFound = false;
			if (venue.VenueKey != null && venue.VenueKey.Trim().Length > 0)
			{
				foreach (TSCVenues s in list)
				{
					if (s.VenueKey != null && s.VenueKey.Trim().Length > 0 && s.KeyEquals(venue))
					{
						bFound = true;
						if (s == venue)
							venue.state = '=';
						else
							venue.state = 'U';
					}
				}
			}
			else
			{
				venue.state = '?';
				return;
			}
			if (bFound == false)
				venue.state = 'D';
		}

		private void SetState(TSCVenues venue, List<TSCVenues> list)
		{
			if (venue.VenueKey == null || venue.VenueKey.Trim().Length == 0)
				return;
			bool bFound = false;
			foreach (TSCVenues s in list)
			{
				if (s.KeyEquals(venue))
				{
					bFound = true;
					if (s == venue)
						venue.state = '=';
					else
						venue.state = 'U';
				}
			}
			if (bFound == false)
				venue.state = 'I';
		}
		#endregion

		private void contextMenuStripSave_Click(object sender, EventArgs e)
		{
			int recordsadded = 0;
			int recordschanged = 0;
			int recordsremoved = 0;

			if (IsValid())
			{
				foreach (TSCVenues venue in gd_tscvenues)
					SetState(venue, db_tscvenues);
				foreach (TSCVenues venue in db_tscvenues)
					SetState(venue, gd_tscvenues);
				foreach (TSCVenues venue in gd_tscvenues)
				{
					if (venue.state == 'I')
					{
						string xml = venue.GetDataXml();
						string result = _scc.GeneralStore("TSCVenues", _queuerForm.controlTag.venuesXml, "INSERT", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Save error: " + venue.VenueKey + " - " + result);
						}
						else
							recordsadded++;
					}
					else if (venue.state == 'U')
					{
						string xml = venue.GetDataXml();
						string result = _scc.GeneralStore("TSCVenues", _queuerForm.controlTag.venuesXml, "UPDATE", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Save error: " + venue.VenueKey + " - " + result);
						}
						else
							recordschanged++;
					}
				}
				foreach (TSCVenues venue in db_tscvenues)
				{
					if (venue.state == 'D')
					{
						string xml = venue.GetDataXml();
						string result = _scc.GeneralStore("TSCVenues", _queuerForm.controlTag.venuesXml, "DELETE", xml);
						if (!(result != null && IsNumber(result) && int.Parse(result) > 0))
						{
							MessageBox.Show("Delete error: " + venue.VenueKey + " - " + result);
						}
						else
							recordsremoved++;
					}
				}
				if (recordsadded > 0 || recordschanged > 0 || recordsremoved > 0)
				{
					db_tscvenues.Clear();
					gd_tscvenues.Clear();
					if (_queuerForm != null)
					{
						_queuerForm.controlTag.venuesXml = _scc.GetTable("TSCVenues", "");
						LoadTSCVenues(_queuerForm.controlTag.venuesXml);
						RefreshGrid();
						_queuerForm.ReloadVenues();
					}
				}
			}
		}

		private void dataGridViewVenues_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
		{
			if (e.ColumnIndex == -1 && e.RowIndex == -1)
				e.ContextMenuStrip = contextMenuStripSave;
			else
				e.ContextMenuStrip = null;
		}
	}
}
