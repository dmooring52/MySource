using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

using XmlUtility;

namespace SingerQueuer
{
    public partial class QueuerForm : Form
    {
		public ControlTag controlTag = null;
		public ComboBox cmb_events = null;
		public TextBox txt_invisible = null;
		public SingerDatabase scc = null;
		private bool _initialized = false;
		private bool _loadingEvents = false;

        public QueuerForm()
        {
            InitializeComponent();
			if (!_initialized)
			{
				_initialized = true;
				UserInitialize();
			}
        }

		private void UserInitialize()
		{
			cmb_events = cmbEvent;
			txt_invisible = txtInvisible;
			Tag = controlTag = new ControlTag();
			venuesControl1.Tag = this;
			eventsControl1.Tag = this;
			singersControl1.Tag = this;
			queueControl1.Tag = this;
			queueControl2.Tag = this;
			queueControl3.Tag = this;
			queueControl4.Tag = this;
			queueControl5.Tag = this;
			queueControl6.Tag = this;

			scc = new SingerDatabase();
			controlTag.venuesXml = InitVenues();
			controlTag.eventsXml = InitEvents();
			controlTag.singersXml = InitSingers();
			LoadEventsCombo(controlTag.eventsXml);
			if (cmbEvent.Items.Count > 0)
				cmbEvent.SelectedIndex = cmbEvent.Items.Count - 1;
			Reload();
		}

		private void LoadEventsCombo(string xml)
		{
			_loadingEvents = true;
			try
			{
				if (xml != null && xml.Trim().Length > 0)
				{
					XmlDocument doc = new XmlDocument();
					doc.LoadXml(xml);
					XmlNodeList nodelist = doc.SelectNodes("/Root/Data");
					if (nodelist != null)
					{
						cmbEvent.Items.Clear();
						cmbEvent.DisplayMember = "EventKey";
						foreach (XmlNode node in nodelist)
						{
							TSCEvents events = new TSCEvents();
							events.EventKey = Utility.GetXmlString(node, "EventKey");
							events.EventName = Utility.GetXmlString(node, "EventName");
							events.EventDate = Utility.GetXmlDateTime(node, "EventDate");
							events.EventEmail = Utility.GetXmlString(node, "EventEmail");
							events.EventAddress = Utility.GetXmlString(node, "EventAddress");
							events.VenueKey = Utility.GetXmlString(node, "VenueKey");
							cmbEvent.Items.Add(events);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				_loadingEvents = false;
			}
		}

		private string InitVenues()
		{
			return scc.GeneralStore("TSCVenues", "GET", (new TSCVenues()).GetDataXml());
		}

		private string InitEvents()
		{
			return scc.GeneralStore("TSCEvents", "GET", (new SingerQueuer.TSCEvents()).GetDataXml());
		}

		private string InitSingers()
		{
			return scc.GeneralStore("TSCSingers", "GET", (new TSCSingers()).GetDataXml());
		}

		public void UpdateSingerActivity(string singerkey)
		{
			if (controlTag != null && controlTag.signersControl != null)
				controlTag.signersControl.UpdateSingerActivity(singerkey);

		}

		public void SetNext()
		{
			if (controlTag != null && controlTag.NextUp != null)
			{
				if (controlTag.NextUp.Count > 0)
				{
					txtNowSinging.Tag = controlTag.NextUp[0];
					txtNowSinging.Text = controlTag.NextUp[0].SingerKey;
					txtNowSingingRound.Text = controlTag.NextUp[0].QueueRound.ToString();
					btnRun.Enabled = true;
				}
				else
				{
					txtNowSinging.Tag = null;
					txtNowSinging.Text = "";
					txtNowSingingRound.Text = "";
					btnRun.Enabled = false;
				}
				if (controlTag.NextUp.Count > 1)
				{
					txtOnDeck.Text = controlTag.NextUp[1].SingerKey;
					txtOnDeckRound.Text = controlTag.NextUp[1].QueueRound.ToString();
				}
				else
				{
					txtOnDeck.Text = "";
					txtOnDeckRound.Text = "";
				}
			}
		}

		public void ReloadEvents()
		{
			TSCEvents currentEvent = null;
			controlTag.eventsXml = InitEvents();
			if (cmbEvent.SelectedIndex >= 0 && cmbEvent.SelectedIndex < cmbEvent.Items.Count)
				currentEvent = cmbEvent.Items[cmbEvent.SelectedIndex] as TSCEvents;
			LoadEventsCombo(controlTag.eventsXml);
			if (currentEvent != null && currentEvent.EventKey != null && currentEvent.EventKey.Trim().Length > 0)
			{
				foreach (object o in cmbEvent.Items)
				{
					TSCEvents tsce = o as TSCEvents;
					if (tsce != null)
					{
						if (tsce.EventKey.Trim().ToLower() == currentEvent.EventKey.Trim().ToLower())
						{
							cmbEvent.SelectedItem = tsce;
							break;
						}
					}
				}
			}
		}

		public void ReloadVenues()
		{
			controlTag.venuesXml = InitVenues();
			if (controlTag.eventsControl != null)
				controlTag.eventsControl.LoadVenues();
		}

		public void ReloadSingers()
		{
			controlTag.singersXml = InitSingers();
			if (queueControl1 != null)
				queueControl1.SetSingers();
			if (queueControl2 != null)
				queueControl2.SetSingers();
			if (queueControl3 != null)
				queueControl3.SetSingers();
			if (queueControl4 != null)
				queueControl4.SetSingers();
			if (queueControl5 != null)
				queueControl5.SetSingers();
			if (queueControl6 != null)
				queueControl6.SetSingers();
		}

		public void Save()
		{
			if (queueControl1 != null)
				queueControl1.Save();
			if (queueControl2 != null)
				queueControl2.Save();
			if (queueControl3 != null)
				queueControl3.Save();
			if (queueControl4 != null)
				queueControl4.Save();
			if (queueControl5 != null)
				queueControl5.Save();
			if (queueControl6 != null)
				queueControl6.Save();
		}

		public void SetGoneHome(string SingerKey)
		{
			if (queueControl1 != null)
				queueControl1.SetGoneHome(SingerKey);
			if (queueControl2 != null)
				queueControl2.SetGoneHome(SingerKey);
			if (queueControl3 != null)
				queueControl3.SetGoneHome(SingerKey);
			if (queueControl4 != null)
				queueControl4.SetGoneHome(SingerKey);
			if (queueControl5 != null)
				queueControl5.SetGoneHome(SingerKey);
			if (queueControl6 != null)
				queueControl6.SetGoneHome(SingerKey);
		}

		private void cmbEvent_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_loadingEvents == false)
			{
				if (queueControl1 != null)
					queueControl1.QueueControl_Reload();
				if (queueControl2 != null)
					queueControl2.QueueControl_Reload();
				if (queueControl3 != null)
					queueControl3.QueueControl_Reload();
				if (queueControl4 != null)
					queueControl4.QueueControl_Reload();
				if (queueControl5 != null)
					queueControl5.QueueControl_Reload();
				if (queueControl6 != null)
					queueControl6.QueueControl_Reload();
				Reload();
				queueControl1.MasterRefresh();
				if (controlTag != null)
					controlTag.SetNext();
				SetNext();
			}
		}

		private void btnCompleted_Click(object sender, EventArgs e)
		{
			if (txtNowSinging.Tag != null && controlTag != null)
			{
				TSCQueue q = txtNowSinging.Tag as TSCQueue;
				if (q != null)
				{
					switch (q.QueueRound)
					{
						case 1:
							if (controlTag.queueControl1 != null)
								controlTag.queueControl1.SetFinished(q);
							break;
						case 2:
							if (controlTag.queueControl2 != null)
								controlTag.queueControl2.SetFinished(q);
							break;
						case 3:
							if (controlTag.queueControl3 != null)
								controlTag.queueControl3.SetFinished(q);
							break;
						case 4:
							if (controlTag.queueControl4 != null)
								controlTag.queueControl4.SetFinished(q);
							break;
						case 5:
							if (controlTag.queueControl5 != null)
								controlTag.queueControl5.SetFinished(q);
							break;
						case 6:
							if (controlTag.queueControl6 != null)
								controlTag.queueControl6.SetFinished(q);
							break;
					}
				}
			}
		}

		private void btnQueueRefresh_Click(object sender, EventArgs e)
		{

		}

		private void QueuerForm_Load(object sender, EventArgs e)
		{
			Reload();
			tabSingingClub.TabPages["tabRound1"].Show();
			tabSingingClub.TabPages["tabRound2"].Show();
			tabSingingClub.TabPages["tabRound3"].Show();
			tabSingingClub.TabPages["tabRound4"].Show();
			tabSingingClub.TabPages["tabRound5"].Show();
			tabSingingClub.TabPages["tabRound6"].Show();
			if (controlTag != null)
				controlTag.SetNext();
			SetNext();
		}

		private void Reload()
		{
			if (cmbEvent.Text != null && cmbEvent.Text.Trim().Length > 0)
			{
				for (int i = 1; i < 7; i++)
				{
					TSCQueue templateQueue = new TSCQueue();
					templateQueue.EventKey = cmbEvent.Text;
					templateQueue.QueueRound = i;
					switch (i)
					{
						case 1:
							controlTag.queue1Xml = scc.GeneralStore("TSCQueue", "GET", templateQueue.GetDataXml());
							break;
						case 2:
							controlTag.queue2Xml = scc.GeneralStore("TSCQueue", "GET", templateQueue.GetDataXml());
							break;
						case 3:
							controlTag.queue3Xml = scc.GeneralStore("TSCQueue", "GET", templateQueue.GetDataXml());
							break;
						case 4:
							controlTag.queue4Xml = scc.GeneralStore("TSCQueue", "GET", templateQueue.GetDataXml());
							break;
						case 5:
							controlTag.queue5Xml = scc.GeneralStore("TSCQueue", "GET", templateQueue.GetDataXml());
							break;
						case 6:
							controlTag.queue6Xml = scc.GeneralStore("TSCQueue", "GET", templateQueue.GetDataXml());
							break;
					}
				}
			}
			/*
			string s = "";
			StringBuilder sb = new StringBuilder();
			s = startdoc();
			s = s.Replace("{event}", cmbEvent.Text.Trim());
			sb.Append(s);
			Directory.CreateDirectory(@"C:\Temp\Foo\" + cmbEvent.Text.Trim());
			StreamWriter sw = File.CreateText(@"C:\Temp\Foo\" + cmbEvent.Text.Trim() + @"\TSCQueue.xml");
			for (int i = 1; i < 7; i++)
			{
				s = startround();
				s = s.Replace("{round}", i.ToString());
				sb.Append(s);
				XmlDocument doc = new XmlDocument();
				if (i == 1)
					doc.LoadXml(controlTag.queue1Xml);
				else if (i == 2)
					doc.LoadXml(controlTag.queue2Xml);
				else if (i == 3)
					doc.LoadXml(controlTag.queue3Xml);
				else if (i == 4)
					doc.LoadXml(controlTag.queue4Xml);
				else if (i == 5)
					doc.LoadXml(controlTag.queue5Xml);
				else if (i == 6)
					doc.LoadXml(controlTag.queue6Xml);
				else
					break;
				XmlNodeList nodes = doc.SelectNodes("/Root/Data");
				if (nodes.Count > 0)
				{
					foreach (XmlNode node in nodes)
					{
						XmlNode QueueOrder = node.SelectSingleNode("QueueOrder");
						XmlNode QueueSong = node.SelectSingleNode("QueueSong");
						XmlNode QueueArtist = node.SelectSingleNode("QueueArtist");
						XmlNode QueueNote = node.SelectSingleNode("QueueNote");
						XmlNode QueueLink = node.SelectSingleNode("QueueLink");
						XmlNode QueueState = node.SelectSingleNode("QueueState");
						XmlNode SingerKey = node.SelectSingleNode("SingerKey");
						if (SingerKey != null && SingerKey.InnerText.Trim().Length > 0)
						{
							s = singerdoc();
							s = s.Replace("{singer}", SingerKey.InnerText.Trim());
							if (QueueOrder != null && QueueOrder.InnerText.Trim().Length > 0)
								s = s.Replace("{order}", QueueOrder.InnerText.Trim());
							else
								s = s.Replace("{order}", "");
							if (QueueSong != null && QueueSong.InnerText.Trim().Length > 0)
								s = s.Replace("{song}", XmlUtility.Utility.XmlEscape(QueueSong.InnerText.Trim()));
							else
								s = s.Replace("{song}", "");
							if (QueueArtist != null && QueueArtist.InnerText.Trim().Length > 0)
								s = s.Replace("{artist}", XmlUtility.Utility.XmlEscape(QueueArtist.InnerText.Trim()));
							else
								s = s.Replace("{artist}", "");
							if (QueueNote != null && QueueNote.InnerText.Trim().Length > 0)
								s = s.Replace("{note}", XmlUtility.Utility.XmlEscape(QueueNote.InnerText.Trim()));
							else
								s = s.Replace("{note}", "");
							if (QueueLink != null && QueueLink.InnerText.Trim().Length > 0)
								s = s.Replace("{link}", XmlUtility.Utility.XmlEscape(QueueLink.InnerText.Trim()));
							else
								s = s.Replace("{link}", "");
							if (QueueState != null && QueueState.InnerText.Trim().Length > 0)
								s = s.Replace("{state}", XmlUtility.Utility.XmlEscape(QueueState.InnerText.Trim()));
							else
								s = s.Replace("{state}", "");
							sb.Append(s);
						}
					}
					sb.Append(endround());
				}
			}
			sb.Append(enddoc());
			sw.Write(sb.ToString());
			sw.Close();
			*/
		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			if (controlTag.NextUp != null && controlTag.NextUp.Count > 0)
				if (controlTag.queueControl1 != null)
					controlTag.queueControl1.Launch(controlTag.NextUp[0].QueueLink);
		}

		private void btnNotHere_Click(object sender, EventArgs e)
		{
			if (txtNowSinging.Tag != null && controlTag != null)
			{
				TSCQueue q = txtNowSinging.Tag as TSCQueue;
				if (q != null)
				{
					switch (q.QueueRound)
					{
						case 1:
							if (controlTag.queueControl1 != null)
								controlTag.queueControl1.SetNotHere(q);
							break;
						case 2:
							if (controlTag.queueControl2 != null)
								controlTag.queueControl2.SetNotHere(q);
							break;
						case 3:
							if (controlTag.queueControl3 != null)
								controlTag.queueControl3.SetNotHere(q);
							break;
						case 4:
							if (controlTag.queueControl4 != null)
								controlTag.queueControl4.SetNotHere(q);
							break;
						case 5:
							if (controlTag.queueControl5 != null)
								controlTag.queueControl5.SetNotHere(q);
							break;
						case 6:
							if (controlTag.queueControl6 != null)
								controlTag.queueControl6.SetNotHere(q);
							break;
					}
				}
			}
		}

		private void btnHistory_Click(object sender, EventArgs e)
		{
			/*
			string xml = "";
			if (cmbEvent.SelectedItem != null)
			{
				TSCEvents tscevent = cmbEvent.SelectedItem as TSCEvents;
				if (tscevent != null)
				{
					xml = scc.GetSingerHistoryForVenue(tscevent.VenueKey);
				}
			}
			else
			{
				xml = scc.GetSingerHistoryForVenue("");
			}
			if (xml.Trim().Length > 0)
			{
				Dictionary<string, SingerHistoryStore> history = new Dictionary<string, SingerHistoryStore>();
				XmlDataDocument doc = new XmlDataDocument();
				if (Utility.IsValidXml(xml) == true)
				{
					doc.LoadXml(xml);
					XmlNodeList nodes = doc.SelectNodes("/Root/Data");

					foreach (XmlNode node in nodes)
					{
						string singerKey = Utility.GetXmlString(node, "SingerKey").Trim();
						DateTime EventDate = Utility.GetXmlDateTime(node, "EventDate");
						string song = Utility.GetXmlString(node, "QueueSong").Trim();
						string artist = Utility.GetXmlString(node, "QueueArtist").Trim();
						if (singerKey.Length > 0 && song.Length > 0)
						{
							SingerHistoryRecord record = new SingerHistoryRecord(singerKey, song, artist, EventDate);
							if (history.ContainsKey(singerKey) == true)
							{
								history[singerKey].history.Add(record);
							}
							else
							{
								SingerHistoryStore store = new SingerHistoryStore();
								store.history.Add(record);
								history.Add(singerKey, store);
							}
						}
					}
				}
				if (history != null && history.Count > 0)
				{
					Excel.Application app = null;
					Excel.Workbook workbook = null;
					Excel.Worksheet worksheet = null;
					Excel.Range worksheet_range = null;
					app = new Excel.Application();
					app.Visible = true;
					workbook = app.Workbooks.Add(1);
					worksheet = (Excel.Worksheet)workbook.Sheets[1];
					int row = 1;
					foreach (KeyValuePair<string, SingerHistoryStore> pair in history)
					{
						foreach (SingerHistoryRecord record in pair.Value.history)
						{
							worksheet.Cells[row, 1] = record.SingerKey;
							worksheet.Cells[row, 2] = record.date;
							worksheet.Cells[row, 3] = record.Song;
							worksheet.Cells[row, 4] = record.Artist;
							row++;
						}
					}
					Excel.Range startcell = worksheet.Cells[1, 1];
					Excel.Range endcell = worksheet.Cells[row, 4];
					worksheet.Range[startcell, endcell].Columns.AutoFit();
				}
			}
			*/
			SingerHistory sh = new SingerHistory();
			sh.SetUp(scc, "");
			sh.ShowDialog();
		}

		public string startdoc()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<Root>");
			sb.AppendLine("	<Data>");
			sb.AppendLine("		<EventKey>{event}</EventKey>");
			return sb.ToString();
		}

		public string startround()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("		<QueueRound round=\"{round}\">");
			return sb.ToString();
		}

		public string singerdoc()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("			<SingerKey key=\"{singer}\">");
			sb.AppendLine("				<QueueOrder>{order}</QueueOrder>");
			sb.AppendLine("				<QueueSong>{song}</QueueSong>");
			sb.AppendLine("				<QueueArtist>{artist}</QueueArtist>");
			sb.AppendLine("				<QueueNote>{note}</QueueNote>");
			sb.AppendLine("				<QueueLink>{link}</QueueLink>");
			sb.AppendLine("				<QueueState>{state}</QueueState>");
			sb.AppendLine("			</SingerKey>");
			return sb.ToString();
		}

		public string endround()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("		</QueueRound>");
			return sb.ToString();
		}

		public string enddoc()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("	</Data>");
			sb.AppendLine("</Root>");
			return sb.ToString();
		}
    }
}

