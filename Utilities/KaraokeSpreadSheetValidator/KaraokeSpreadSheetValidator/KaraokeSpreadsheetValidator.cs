using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Core;
using Excel=Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace KaraokeSpreadSheetValidator
{
	public partial class KaraokeSpreadsheetValidator : Form
	{
		public SpreadsheetValidator sv = null;
		public int row = 2;
		public int nrows = 0;

		public KaraokeSpreadsheetValidator()
		{
			InitializeComponent();
		}

		private void btnValidate_Click(object sender, EventArgs e)
		{
			sv = new SpreadsheetValidator();

			nrows = sv.nCnt;
			timerKSV.Start();
		}

		private void timerKSV_Tick(object sender, EventArgs e)
		{
			btnValidate.Enabled = false;
			btnZips.Enabled = false;
			timerKSV.Stop();
			if (row < nrows)
			{
				row++;
				sv.Run(row);
				toolStripStatusLabelKSV.Text = sv.status;
				//statusStripKSV.Text = sv.status;
				statusStripKSV.Refresh();
				timerKSV.Start();
			}
			else
			{
				sv.Wrapup();
				MessageBox.Show(string.Format("Done: Rows: {0} URLs: {1} CDGs: {2} ZIPS: {3} Bins: {4} Good: {5} Bads: {6} Not Found: {7} Null: {8}", 
					sv.Rows, 
					sv.URLs, 
					sv.CDGs,
					sv.ZIPs,
					sv.Bins,
					sv.URLs + sv.CDGs + sv.ZIPs + sv.Bins,
					sv.Bads,
					sv.NotFound, 
					sv.NULLs
				), "Task Completed");
			}
		}

		private void btnZips_Click(object sender, EventArgs e)
		{
			sv = new SpreadsheetValidator(true);

			nrows = sv.nCnt;
			timerKSV.Start();
		}
	}

	public class SpreadsheetValidator
	{
		public Excel.Application xlApp;
		public Excel.Workbook xlWorkBook;
		public Excel.Worksheet xlWorkSheet;
		public Excel.Range range;

		public int rCnt = 0;
		public int nCnt = 0;

		public int Rows = 0;
		public int URLs = 0;
		public int NULLs = 0;
		public int Bins = 0;
		public int CDGs = 0;
		public int ZIPs = 0;
		public int Bads = 0;
		public int NotFound = 0;
		public bool Zip = false;

		public List<string> PathNotFound = new List<string>();
		public List<string> Tracks = new List<string>();
		public List<OneDriveFile> OneDriveFiles = new List<OneDriveFile>();
        private Dictionary<string, int> _cols = null;

		public string status = "";

		public SpreadsheetValidator(bool zip)
		{
			Zip = zip;
			Init();
		}

		public SpreadsheetValidator() : this(false)
		{
		}

		private void Init()
		{
            _cols = new Dictionary<string, int>();
			xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(@"C:\Users\dmooring\OneDrive\Karaoke\CDG_MP3\Index\sl.xlsm", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
			xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

			range = xlWorkSheet.UsedRange;

			nCnt = range.Rows.Count;
            
            StreamReader sr = File.OpenText(@"C:\Users\dmooring\OneDrive\Karaoke\CDG_MP3\Index\MP4.txt");
			while (sr.EndOfStream == false)
			{
				string line = sr.ReadLine();
				if (line != null && line.Trim().Length > 0)
				{
					string[] parts = line.Split('\t');
					if (parts.Length > 1)
					{
						OneDriveFiles.Add(new OneDriveFile(parts[0], parts[1]));
					}
				}
			}
            for (int i = 1; i < 40; i++)
            {
                string colname = (string)(range.Cells[1, i] as Excel.Range).Value2;
                if (colname != null && colname.Trim().Length > 0)
                    _cols.Add(colname, i);
            }

			sr.Close();
		}

        private int GetColumn(string colname)
        {
            if (_cols != null)
                return _cols[colname];
            else
                return -1;
        }
		
		public void Wrapup()
		{
            try
            {
                xlWorkBook.SaveAs(@"C:\Temp\sl.xlsm");
                xlWorkBook.Close(false, null, null);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception occurred while saving workbook");
            }
		}

		public void Run(int row)
		{
			if (Zip == true)
				RunZip(row);
			else
				RunCDG(row);
		}

		public void RunCDG(int row)
		{
			Rows++;
			string filepath = (string)(range.Cells[row, GetColumn("Path")] as Excel.Range).Value2;
			string song = "";
			string artist = "";
			try
			{
				song = (string)(range.Cells[row, GetColumn("Title")] as Excel.Range).Value2;
				artist = (string)(range.Cells[row, GetColumn("Artist")] as Excel.Range).Value2;
			}
			catch { }
			if (filepath != null)
			{
				status = filepath;
				if (File.Exists(filepath) == false)
				{
					if (filepath.ToLower().Trim().StartsWith("http") == false)
					{
						NotFound++;
						PathNotFound.Add(filepath);
					}
					else
					{
						URLs++;
					}
				}
				else
				{
					string track = filepath.Substring(filepath.Length - 12).ToLower().Trim();
					if (track.Contains("track") == true && track.Contains(".cdg") == true)
					{
						string tracknstring = TrackString(track);
						if (tracknstring.Length > 0)
						{
							string filedir = Path.GetDirectoryName(filepath);
							string filename = string.Format("{0} - {1}{2}.cdg", artist, song, tracknstring);
							string mp3name = string.Format("{0} - {1}{2}.mp3", artist, song, tracknstring);
							string mp3path = filepath.Substring(0, filepath.Length - 4) + ".mp3";
							if (File.Exists(mp3path) == true)
							{
								Tracks.Add(string.Format("rename \"{0}\" \"{1}\"", filepath, filename));
								Tracks.Add(string.Format("rename \"{0}\" \"{1}\"", mp3path, mp3name));
							}
						}
					}
                    if (filepath.Trim().ToLower().StartsWith(@"c:\users\dmooring\onedrive\karaoke\cdg_mp3\") == true)
					{
						string dir = Path.GetDirectoryName(filepath);
						string ext = Path.GetExtension(filepath);
						if (ext.ToLower() == ".cdg")
						{
							string mp3path = Path.Combine(dir, Path.GetFileNameWithoutExtension(filepath) + ".mp3");
							if (File.Exists(mp3path) == false)
							{
								NotFound++;
								PathNotFound.Add(mp3path);
							}
							else
								CDGs++;
						}
						else if (ext.ToLower() == ".bin")
						{
							Bins++;
						}
						else
						{
							Bads++;
							PathNotFound.Add(filepath);
						}
					}
                    else
                    {
                        status = filepath;
                        PathNotFound.Add(filepath);
                        Bads++;
                    }
				}
			}
			else
			{
				status = "NULL";
				NULLs++;
			}
		}

		public void RunZip(int row)
		{
			Rows++;
			string filepath = (string)(range.Cells[row, GetColumn("Path")] as Excel.Range).Value2;
			string song = "";
			string artist = "";
			try
			{
				song = (string)(range.Cells[row, GetColumn("Title")] as Excel.Range).Value2;
				artist = (string)(range.Cells[row, GetColumn("Artist")] as Excel.Range).Value2;
			}
			catch { }
			if (filepath != null)
			{
				filepath = filepath.Trim();
				status = filepath;
				if (File.Exists(filepath) == false)
				{
					if (filepath.ToLower().Trim().StartsWith("http") == false)
					{
						NotFound++;
						PathNotFound.Add(filepath);
					}
					else
					{
						URLs++;
					}
				}
				else
				{
					string lcpath = filepath.ToLower();
					if (lcpath.StartsWith("c:") == true && lcpath.EndsWith(".cdg") == true)
					{
						string starget = "CDG_MP3";
						int s = lcpath.IndexOf(starget.ToLower());
						if (s > 0)
						{
							int e = s + starget.Length;
							string s1 = filepath.Substring(0, s);
							string s2 = "CDG_ZIP";
							string s3 = filepath.Substring(e, filepath.Length - (e + 4)) + ".zip";
							filepath = s1 + s2 + s3;
						}
					}
                    string swith = @"C:\Users\dmooring\OneDrive\Karaoke\CDG_ZIP";
					if (filepath.Trim().ToLower().StartsWith(swith.ToLower()) == true)
					{
						string dir = Path.GetDirectoryName(filepath);
						string ext = Path.GetExtension(filepath);
						if (ext.ToLower() == ".zip")
						{
							if (File.Exists(filepath) == false)
							{
								NotFound++;
								PathNotFound.Add(filepath);
							}
							else
							{
								ZIPs++;
								//(range.Cells[row, GetColumn("MP4")] as Excel.Range).Value2 = GetPath(filepath);
							}
						}
						else if (ext.ToLower() == ".bin")
						{
							Bins++;
						}
						else
						{
							Bads++;
							PathNotFound.Add(filepath);
						}
					}
				}
			}
			else
			{
				status = "NULL";
				NULLs++;
			}
		}

		private string GetPath(string filepath)
		{
			foreach (OneDriveFile file in OneDriveFiles)
			{
				if (filepath.Contains(file.path.Substring(1).Replace('/', '\\')))
					return file.link;
			}
			return filepath;
		}

		private string TrackString(string track)
		{
			string n = "0123456789";
			string r = "";
			bool start = false;
			foreach (char c in track)
			{
				if (n.Contains(c) == true)
				{
					start = true;
					r += c;
				}
				else
				{
					if (start == true)
						return r;
				}
			}
			return r;
		}

		private void releaseObject(object obj)
		{
			try
			{
				System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
				obj = null;
			}
			catch (Exception ex)
			{
				obj = null;
				MessageBox.Show("Unable to release the Object " + ex.ToString());
			}
			finally
			{
				GC.Collect();
			}
		}
	}
	public class OneDriveFile
	{
		public string path { get; set; }
		public string link { get; set; }
		public OneDriveFile(string _path, string _link)
		{
			path = _path;
			link = _link;
		}
	}
}
