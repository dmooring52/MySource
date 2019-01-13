using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

using XmlUtility;

namespace KaraokeQueuer
{
	public partial class SingerHistory : Form
	{
		private SingerDatabase _scc = null;
		private string _singer = "";
		private int _rowIndex = -1;

		public SingerHistory()
		{
			InitializeComponent();
		}

		public void SetUp(SingerDatabase scc, string singer)
		{
			_scc = scc;
			_singer = singer;
		}

		private void SingerHistory_Load(object sender, EventArgs e)
		{
			if (_scc != null)
			{
				try
				{
					TSCQueue template = new TSCQueue();
					template.SingerKey = _singer;
					template.QueueRound = -1;
					string singerxml = template.GetDataXml();
					/*XmlDocument d = new XmlDocument();
					d.LoadXml(singerxml);
					XmlNodeList keys = d.SelectNodes("/Root/Data/KEYS/COLUMN_NAME");
					if (keys != null && keys.Count == 3)
					{
						keys[0].InnerText = "SingerKey";
						keys[2].InnerText = "EventKey";
					}
					singerxml = d.OuterXml;*/
					XmlDocument doc = _scc.GetTable("TSCQueue", template.EventKey);
					Dictionary<string, SingerHistoryStore> singerhistory = new Dictionary<string, SingerHistoryStore>();
					if (doc != null)
					{
						XmlNodeList nodelist = doc.SelectNodes("/Root/Data");
						foreach (XmlNode node in nodelist)
						{
							string tscevent = Utility.GetXmlString(node, "EventKey");
							string singer = Utility.GetXmlString(node, "SingerKey");
							string status = Utility.GetXmlString(node, "QueueState");
							string song = Utility.GetXmlString(node, "QueueSong");
							string artist = Utility.GetXmlString(node, "QueueArtist");
							string note = Utility.GetXmlString(node, "QueueNote");
							string link = Utility.GetXmlString(node, "QueueLink");

							if ((singer != null && singer.Trim().Length > 0 && status != null && status.Trim().ToLower() == "finished" && ((song != null && song.Trim().Length > 0) || (link != null && link.Trim().Length > 0))))
							{
								SingerHistoryRecord singerrecord = new SingerHistoryRecord(tscevent, singer, song, artist, note, link);
								if (singerhistory.ContainsKey(singer) == true)
									singerhistory[singer].history.Add(singerrecord);
								else
								{
									SingerHistoryStore shs = new SingerHistoryStore(singer);
									shs.history.Add(singerrecord);
									singerhistory.Add(singer, shs);
								}
							}
						}
					}
					if (singerhistory.Count > 0)
					{
						var list = singerhistory.Keys.ToList();
						list.Sort();
						foreach (var key in list) 
						{
							if (singerhistory[key].history.Count > 0)
							{
								foreach (SingerHistoryRecord shr in singerhistory[key].history)
								{
									int irow = dataGridViewHistory.Rows.Add();
									dataGridViewHistory.Rows[irow].Cells["TSCEvent"].Value = shr.TSCEvent;
									dataGridViewHistory.Rows[irow].Cells["Singer"].Value = shr.SingerKey;
									dataGridViewHistory.Rows[irow].Cells["Song"].Value = shr.Song;
									dataGridViewHistory.Rows[irow].Cells["Artist"].Value = shr.Artist;
									dataGridViewHistory.Rows[irow].Cells["Note"].Value = shr.Note;
									dataGridViewHistory.Rows[irow].Cells["Link"].Value = shr.Link;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_rowIndex >= 0 && _rowIndex < dataGridViewHistory.Rows.Count)
			{
				string song = "";
				string artist = "";
				string note = "";
				string link = "";
				if (dataGridViewHistory.Rows[_rowIndex].Cells["Song"].Value != null)
					song = dataGridViewHistory.Rows[_rowIndex].Cells["Song"].Value.ToString().Trim();
				if (dataGridViewHistory.Rows[_rowIndex].Cells["Artist"].Value != null)
					artist = dataGridViewHistory.Rows[_rowIndex].Cells["Artist"].Value.ToString().Trim();
				if (dataGridViewHistory.Rows[_rowIndex].Cells["Note"].Value != null)
					note = dataGridViewHistory.Rows[_rowIndex].Cells["Note"].Value.ToString().Trim();
				if (dataGridViewHistory.Rows[_rowIndex].Cells["Link"].Value != null)
					link = dataGridViewHistory.Rows[_rowIndex].Cells["Link"].Value.ToString().Trim();

				if (song.Length > 0 || artist.Length > 0 || note.Length > 0 || link.Length > 0)
				{
					Clipboard.Clear();
					string cells = string.Format("{0}	{1}	{2}	{3}", song, artist, note, link);
					Clipboard.SetText(cells);
				}
			}
		}

		private void dataGridViewHistory_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (e.ColumnIndex == -1 && e.RowIndex >= 0 && dataGridViewHistory.Rows[e.RowIndex].IsNewRow == false)
				{
					_rowIndex = e.RowIndex;
					contextMenuStripCopy.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
				}
			}
		}

		private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Excel.Application app = null;
			Excel.Workbook workbook = null;
			Excel.Worksheet worksheet = null;
			//Excel.Range worksheet_range = null;
			app = new Excel.Application();
			app.Visible = true;
			workbook = app.Workbooks.Add(1);
			worksheet = (Excel.Worksheet)workbook.Sheets[1];
			int row = 1;
			foreach (DataGridViewRow dgrow in dataGridViewHistory.Rows)
			{
				if (dgrow.Cells[0].Value != null)
					worksheet.Cells[row, 1] = dgrow.Cells[0].Value.ToString();
				if (dgrow.Cells[0].Value != null)
					worksheet.Cells[row, 2] = dgrow.Cells[1].Value.ToString();
				if (dgrow.Cells[0].Value != null)
					worksheet.Cells[row, 3] = dgrow.Cells[2].Value.ToString();
				if (dgrow.Cells[0].Value != null)
					worksheet.Cells[row, 4] = dgrow.Cells[3].Value.ToString();
				row++;
			}
			Excel.Range startcell = worksheet.Cells[1, 1];
			Excel.Range endcell = worksheet.Cells[row, 4];
			worksheet.Range[startcell, endcell].Columns.AutoFit();
		}

		private void exportToHTMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<html>");
			sb.AppendLine("<header/>");
			sb.AppendLine("<body>");
			sb.AppendLine("<table>");
			int row = 1;
			foreach (DataGridViewRow dgrow in dataGridViewHistory.Rows)
			{
				sb.Append("<tr><td>");
				if (dgrow.Cells[0].Value != null)
					sb.Append(dgrow.Cells[0].Value.ToString());
				sb.Append("</td><td>&nbsp;</td><td>");
				if (dgrow.Cells[1].Value != null)
					sb.Append(dgrow.Cells[1].Value.ToString());
				sb.Append("</td><td>&nbsp;</td><td>");
				if (dgrow.Cells[2].Value != null)
					sb.Append(dgrow.Cells[2].Value.ToString());
				sb.Append("</td><td>&nbsp;</td><td>");
				if (dgrow.Cells[3].Value != null)
					sb.Append(dgrow.Cells[3].Value.ToString());
				sb.Append("</td></tr>");
				sb.AppendLine();
				row++;
			}
			sb.AppendLine("</table>");
			sb.AppendLine("</body>");
			sb.AppendLine("</html>");
			ShowBox showbox = new ShowBox(sb.ToString());
			showbox.ShowDialog();
		}

		private void dataGridViewHistory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex == -1 && e.ColumnIndex == -1)
			{
				contextMenuStripSave.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
			}
		}
	}
}
