using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace KaraokeValidator
{
    public class SpreadsheetValidator
    {
        public Excel.Application xlApp = null;
        public Excel.Workbook xlWorkBook = null;
        public Excel.Worksheet xlWorkSheet = null;
        public Microsoft.Office.Interop.Excel.Range xlrange = null;

        public int rCnt = 0;
        public int nCnt = 0;
        public int nCol = 0;

        public int Rows = 0;
        public int URLs = 0;
        public int NULLs = 0;
        public int CDGs = 0;
        public int ZIPs = 0;
        public int MP4s = 0;
        public int Bads = 0;
        public int NotFound = 0;
        public bool Link = false;
        public string excelpath = "";
        public string rootpath = "";
        //public StringBuilder zipdelete = null;
        //public StringBuilder zipadd = null;
        //public StringBuilder id3 = null;
        private Dictionary<string, string> _cols = null;

        public List<string> PathNotFound = new List<string>();
        public List<LinkFile> mp4Links = null;
        //public List<SkyDriveFile> OneDriveFiles = new List<SkyDriveFile>();

        public string status = "";

        public SpreadsheetValidator(string path, bool link)
        {
            string testpath = path.ToLower().Trim().Replace('/', '\\');
            int ix = testpath.IndexOf(@"\karaoke\");

            if (ix > 0)
            {
                rootpath = path.Trim().Substring(0, ix);
            }
            excelpath = path;
            Link = link;
            Init();
        }

        public SpreadsheetValidator(string path)
            : this(path, false)
        {
        }

        private void Init()
        {
            mp4Links = new List<LinkFile>();
            _cols = new Dictionary<string, string>();
            if (File.Exists(excelpath) == true)
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(excelpath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlrange = xlWorkSheet.UsedRange;
                nCnt = xlrange.Rows.Count;
                nCol = xlrange.Columns.Count;
            }
            
            StreamReader sr = File.OpenText(@"C:\Users\dmooring\OneDrive\Karaoke\CDG_MP3\Index\MP4.txt");
            while (sr.EndOfStream == false)
            {
                string line = sr.ReadLine();
                if (line != null && line.Trim().Length > 0)
                {
                    string[] parts = line.Split('\t');
                    if (parts.Length > 1)
                    {
                        mp4Links.Add(new LinkFile(parts[0], parts[1]));
                    }
                }
            }
            
            for (int i = 1; i < nCol + 2; i++)
            {
                string columnName = GetAddress(xlWorkSheet.Columns[i].Address);
                string colname = (string)(xlrange.Cells[1, i] as Excel.Range).Value2;
                if (colname != null && colname.Trim().Length > 0)
                    _cols.Add(colname, columnName);
            }
            if (!(_cols.ContainsKey("Title") == true &&
                _cols.ContainsKey("Artist") == true &&
                _cols.ContainsKey("Disk") == true &&
                _cols.ContainsKey("Path") == true &&
                _cols.ContainsKey("OneDrive") == true &&
                _cols.ContainsKey("MP4") == true))
            {
                throw (new Exception("Excel columns must include: Artist, Title, Disk, Path, OneDrive, MP4"));
            }

            //zipadd = new StringBuilder();
            //zipdelete = new StringBuilder();
            //id3 = new StringBuilder();
        }

        public void Wrapup()
        {
            if (xlWorkBook != null)
            {
                //if (Link == true)
                {
                    string save = @"C:\Temp\sl.xlsm";
                    if (File.Exists(save) == true)
                        File.Delete(save);
                    xlWorkBook.SaveAs(save);
                }
                xlWorkBook.Close(false, null, null);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                xlWorkBook = null;
            }
        }

        public OneDriveItem Run(int row)
        {
            if (xlApp == null || xlWorkBook == null || xlWorkSheet == null)
                return null;
            return RunCDG(row);
        }

        public void WriteLink(int row, string link)
        {
            string rr = string.Format("{0}{1}", GetColumn("OneDrive"), row);
            try
            {
                xlWorkSheet.Range[rr].Value = link;
            }
            catch { }
        }
        private string RangeRow(string rangeColumn, int row)
        {
            string rr = string.Format("{0}{1}", GetColumn(rangeColumn), row);
            try
            {
                rr = xlWorkSheet.Range[rr].Value;
                if (rr == null)
                    return "";
                else
                    return rr;
            }
            catch { }
            return "";
        }
        private string FilePath(string excel)
        {
            if (excel == null || excel.Trim().Length == 0)
                return "";
            string testpath = excel.Trim().ToLower().Replace('/', '\\');
            if (testpath.StartsWith("http") || testpath.StartsWith("www"))
                return excel;
            int ix = testpath.IndexOf(@"\karaoke\");
            if (ix > 0 && rootpath.Length > 0)
            {
                return rootpath + excel.Substring(ix);
            }
            return excel;
        }

        private string GetAddress(string address)
        {
            if (address != null && address.Trim().Length > 0 && address.Contains(":$") == true)
            {
                return address.Substring(address.LastIndexOf("$") + 1);
            }
            else
                return "";
        }
        private string GetColumn(string colname)
        {
            if (_cols != null)
                return _cols[colname];
            else
                return "";
        }

        public OneDriveItem RunCDG(int row)
        {
            Rows++;
            string filepath = FilePath(RangeRow("Path", row));
            string filelink = FilePath(RangeRow("OneDrive", row));
            string filemp4 = FilePath(RangeRow("MP4", row));
            string song = "";
            string artist = "";
            string disk = "";
            OneDriveItem cdgzip = null;
            try
            {
                song = RangeRow("Title", row);
                artist = RangeRow("Artist", row);
                disk = RangeRow("Disk", row);
            }
            catch { }
            if (filepath != null)
            {
                filepath = filepath.Trim();
                status = filepath;
                if (File.Exists(filepath) == false)
                {
                    if (filepath.ToLower().Trim().StartsWith("http") == false && filepath.ToLower().Trim().StartsWith("www") == false)
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
                    cdgzip = new OneDriveItem(filepath, filelink, row);
                    if (cdgzip != null)
                    {
                        string swith = @"\karaoke\cdg_mp3\";
                        string filepathcdg = cdgzip.PathCDG;
                        if (filepathcdg.Trim().ToLower().Contains(swith) == true)
                        {
                            string dir = Path.GetDirectoryName(filepathcdg);
                            string ext = Path.GetExtension(filepathcdg);
                            if (ext.ToLower() == ".cdg")
                            {
                                string mp3path = Path.Combine(dir, Path.GetFileNameWithoutExtension(filepathcdg) + ".mp3");
                                if (File.Exists(mp3path) == false)
                                {
                                    NotFound++;
                                    PathNotFound.Add(mp3path);
                                }
                                else
                                {
                                    
                                    //zipdelete.AppendLine(string.Format("\"C:\\Program Files\\7-Zip\\7z.exe\" d \"{0}\"", cdgzip.PathZIP));
                                    //zipadd.AppendLine(string.Format("\"C:\\Program Files\\7-Zip\\7z.exe\" a \"{0}\" \"{1}\" \"{2}\"", cdgzip.PathZIP, cdgzip.PathCDG, mp3path));
                                    //id3.AppendLine(string.Format("id3 -2 -a \"{0}\" -t \"{1}\" -l \"{2}\" -g \"Karaoke\" \"{3}\"", artist.Trim(), song.Trim(), disk.Trim(), mp3path));
                                    CDGs++;
                                }
                            }
                            else
                            {
                                Bads++;
                                PathNotFound.Add(filepathcdg);
                            }
                        }

                        swith = @"\karaoke\cdg_zip";
                        filepathcdg = cdgzip.PathZIP;
                        if (filepathcdg.Trim().ToLower().Contains(swith) == true)
                        {
                            string dir = Path.GetDirectoryName(filepathcdg);
                            string ext = Path.GetExtension(filepathcdg);
                            if (ext.ToLower() == ".zip")
                            {
                                if (File.Exists(filepathcdg) == false)
                                {
                                    NotFound++;
                                    PathNotFound.Add(filepathcdg);
                                }
                                else
                                {
                                    ZIPs++;
                                    if (Link == true)
                                    {
                                        string rr = string.Format("{0}{1}", GetColumn("Path"), row);
                                        xlWorkSheet.Range[rr].Value = filepathcdg;
                                        rr = string.Format("{0}{1}", GetColumn("OneDrive"), row);
                                        xlWorkSheet.Range[rr].Value = filelink;
                                    }
                                }
                            }
                            else
                            {
                                Bads++;
                                PathNotFound.Add(filepathcdg);
                            }
                        }
                        if (filemp4 != null && filemp4.Trim().Length > 0)
                        {
                            MP4s++;
                            //string rr = string.Format("{0}{1}", GetColumn("MP4"), row);
                            //xlWorkSheet.Range[rr].Value = filemp4;
                        }
                        if (mp4Links != null && mp4Links.Count > 0)
                        {
                            string mp4link = FindMP4(filepath.ToLower().Trim());
                            if (mp4link != null && mp4link.Trim().Length > 0 && mp4link != filemp4)
                            {
                                string rr = string.Format("{0}{1}", GetColumn("MP4"), row);
                                xlWorkSheet.Range[rr].Value = mp4link;
                            }
                        }
                    }
                    else
                    {
                        NotFound++;
                        PathNotFound.Add(filepath);
                    }
                }
            }
            else
            {
                status = "NULL";
                NULLs++;
            }
            return cdgzip;
        }

        private string FindMP4(string path)
        {
            foreach (LinkFile lf in mp4Links)
            {
                if (lf.HasPath(path))
                    return lf.link;
            }
            return "";
        }
        public bool IsDrive(string path)
        {
            string alpha = "abcdefghijklmnopqrstuvwxyz";
            if (path.Trim().Length > 1)
                if (alpha.Contains(path.Trim().ToLower()[0]) == true)
                    if (path.Trim()[1] == ':')
                        return true;
            return false;
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
                //MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
    public class SkyDriveFile
    {
        public string path { get; set; }
        public string link { get; set; }
        public SkyDriveFile(string _path, string _link)
        {
            path = _path;
            link = _link;
        }
    }
    public class OneDriveFile
    {
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string source { get; set; }
        public string parent { get; set; }
        public OneDriveFile(string _parent, string _id, string _name, string _link, string _source)
        {
            parent = _parent;
            id = _id;
            name = _name;
            link = _link;
            source = _source;
        }
    }
    public class OneDriveItem
    {
        public string Path;
        public int Row;
        public string Link;
        public bool isnew = false;
        private const string _cdgmp3 = @"\cdg_mp3\";
        private const string _cdgzip = @"\cdg_zip\";
        private const string _extcdg = ".cdg";
        private const string _extzip = ".zip";
        public OneDriveItem(string path, string link, int row)
        {
            Path = path;
            Link = link;
            Row = row;
        }

        public string PathCDG 
        { 
            get 
            { 
                if (Path != null && Path.Trim().Length > 0)
                {
                    if (Path.ToLower().Contains(_cdgmp3) == true)
                        return Path;
                    else
                        return GetCDGZIP(_cdgzip, _extzip, _cdgmp3, _extcdg);
                }
                return ""; 
            } 
        }
        public string PathZIP
        {
            get
            {
                if (Path != null && Path.Trim().Length > 0)
                {
                    if (Path.ToLower().Contains(_cdgzip) == true)
                        return Path;
                    else
                        return GetCDGZIP(_cdgmp3, _extcdg, _cdgzip, _extzip);
                }
                return "";
            }
        }

        private string GetCDGZIP(string cdg, string ext, string tcdg, string text)
        {
            string lowpath = Path.ToLower();
            int ncdg = 0;
            int next = 0;
            ncdg = lowpath.IndexOf(cdg);
            if (ncdg > 0)
            {
                next = lowpath.LastIndexOf(ext);
                if (!(next == lowpath.Length - ext.Length))
                {
                    next = lowpath.LastIndexOf(ext);
                    if (!(next == lowpath.Length - ext.Length))
                    {
                        next = 0;
                    }
                }
                if (next > 0)
                {
                    return
                        Path.Substring(0, ncdg) + tcdg.ToUpper() +
                        Path.Substring(ncdg + cdg.Length, Path.Length - (ncdg + cdg.Length + ext.Length)) +
                        text;
                }
            }
            return "";
        }
    }
    public class OneDriveFolder
    {
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string parent { get; set; }

        private List<OneDriveFile> _files = new List<OneDriveFile>();
        public List<OneDriveFile> files { get { return _files; } }
        public OneDriveFolder(string _parent, string _id, string _name, string _link)
        {
            parent = _parent;
            id = _id;
            name = _name;
            link = _link;
        }
    }
    public class LinkFile
    {
        public string path { get; set; }
        public string link { get; set; }
        public LinkFile(string _path, string _link)
        {
            path = _path;
            link = _link;
        }
        public bool HasPath(string localpath)
        {
            string mp3 = "/karaoke/cdg_mp3";
            string zip = "/karaoke/cdg_zip";
            string ext = "";
            if (localpath != null && localpath.Trim().Length > 0 && path != null && path.Trim().Length > 0)
            {
                localpath = localpath.Trim().Replace('\\', '/').Replace("//", "/");
                string thepath = localpath.Trim().ToLower();
                int ix = thepath.IndexOf(mp3);
                if (ix < 0)
                {
                    ix = thepath.IndexOf(zip);
                    ext = ".zip";
                }
                else
                    ext = ".cdg";
                if (ix > 0)
                {
                    string thispath = path.Trim().ToLower().Replace(".mp4", ext).Replace('\\', '/').Replace("//", "/");
                    int iy = thispath.IndexOf("$");
                    if (iy >= 0)
                    {
                        if (thepath.Substring(ix + mp3.Length) == thispath.Substring(iy + 1))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
