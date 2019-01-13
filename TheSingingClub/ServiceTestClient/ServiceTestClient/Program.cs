using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using ServiceTestClient.SingingClubDataReference;
using XmlUtility;

namespace ServiceTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
			TSCSingers singers = new TSCSingers();
            SingingClubClient scc = new SingingClubDataReference.SingingClubClient();// new SingingClubClient();
            string s = scc.GeneralStore("TSCSingers", "GET", singers.GetDataXml());
            TSCSongs songs = new TSCSongs();
            songs.PageOrder = "[Title], [Artist], [Disk]";
            songs.PageOffset = 0;
            songs.PageReturn = 50;
            s = scc.GeneralStore("TSCSongList_Main", "GET", songs.GetDataXml());
            s = "";
            */
            SingingClubClient client = new SingingClubClient();
            TSCSongs songs = new TSCSongs();
            songs.Title = "Beautiful Noise";
            songs.Artist = "Neil Diamond";
            songs.Disk = "MJT";
            //string singerkey = "Major Tom";
            //TSCSingersSongs singerssongs = new TSCSingersSongs(singerkey, songs.Title, songs.Artist);
            string xml = client.GeneralStore("TSCSongList_Main", "GET", songs.GetDataXml());
            /*
            if (xml != null && xml.Trim().Length == 0)
            {
                singerssongs.URL = songs.OneDrive;
                xml = client.GeneralStore("TSCSingersSongs", "INSERT", singerssongs.GetDataXml());
            }
            */
        }
    }

	public class TSCSingers : INotifyPropertyChanged
	{
		public char state = '?';

		private ColumnClass _singerKey = new ColumnClass("SingerKey", ColumnType._string);
		private ColumnClass _singerName = new ColumnClass("SingerName", ColumnType._string);
		private ColumnClass _singerEmail = new ColumnClass("SingerEmail", ColumnType._string);
		private ColumnClass _singerActivity = new ColumnClass("SingerActivity", ColumnType._int);

		public string SingerKey { get { return _singerKey.Value_string; } set { _singerKey.Value_string = value; NotifyPropertyChanged(); } }
		public string SingerName { get { return _singerName.Value_string; } set { _singerName.Value_string = value; NotifyPropertyChanged(); } }
		public string SingerEmail { get { return _singerEmail.Value_string; } set { _singerEmail.Value_string = value; NotifyPropertyChanged(); } }
		public int SingerActivity { get { return _singerActivity.Value_int; } set { _singerActivity.Value_int = value; NotifyPropertyChanged(); } }

		public Dictionary<string, string> Headers = new Dictionary<string, string>();
		public Dictionary<string, ColumnClass> Columns = new Dictionary<string, ColumnClass>();
		public Dictionary<string, ColumnClass> Primary = new Dictionary<string, ColumnClass>();

		public event PropertyChangedEventHandler PropertyChanged;

		public TSCSingers()
		{
			Headers.Add("SingerKey", "Key");
			Headers.Add("SingerName", "Name");
			Headers.Add("SingerEmail", "Email");
			Headers.Add("SingerActivity", "Activity");

			Columns.Add("SingerKey", _singerKey);
			Columns.Add("SingerName", _singerName);
			Columns.Add("SingerEmail", _singerEmail);
			Columns.Add("SingerActivity", _singerActivity);

			Primary.Add("SingerKey", _singerKey);
		}

		public TSCSingers(string singerKey)
			: this()
		{
			_singerKey = new ColumnClass("SingerKey", ColumnType._string, singerKey);
		}

		public TSCSingers(TSCSingers tscsingers)
			: this()
		{
			SingerKey = new string(tscsingers.SingerKey.ToArray());
			SingerName = new string(tscsingers.SingerName.ToArray());
			SingerEmail = new string(tscsingers.SingerEmail.ToArray());
			SingerActivity = tscsingers.SingerActivity;
		}


		public static bool operator ==(TSCSingers a, TSCSingers b)
		{
			if (System.Object.ReferenceEquals(a, b))
				return true;
			if ((object)a == null || (object)b == null)
				return false;
			return FieldsEqual(a, b);
		}

		public static bool operator !=(TSCSingers a, TSCSingers b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			TSCSingers a = obj as TSCSingers;
			if (a == null)
				return false;
			return FieldsEqual(this, a);
		}

		public bool Equals(TSCSingers a)
		{
			if ((object)a == null)
				return false;
			return FieldsEqual(this, a);
		}

		private static bool FieldsEqual(TSCSingers a, TSCSingers b)
		{
			return (
				(a.SingerKey == b.SingerKey) &&
				(a.SingerName == b.SingerName) &&
				(a.SingerEmail == b.SingerEmail) &&
				(a.SingerActivity == b.SingerActivity) &&
				(true)
				);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public bool KeyEquals(TSCSingers a)
		{
			if ((object)a == null)
				return false;
			return (
			(a.SingerKey.Trim().ToLower() == SingerKey.Trim().ToLower())
			);
		}

		public string GetDataXml()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<Root>");
			sb.AppendLine("	<Data>");
            foreach (KeyValuePair<string, ColumnClass> entry in Columns)
			{
				sb.AppendLine("		<COLUMNS>");
				sb.AppendLine(string.Format("			<COLUMN_NAME>{0}</COLUMN_NAME>", entry.Key));
				sb.AppendLine(string.Format("			<COLUMN_TYPE>{0}</COLUMN_TYPE>", entry.Value.CType));
				sb.AppendLine(string.Format("			<COLUMN_VALUE>{0}</COLUMN_VALUE>", Utility.Encode4bit(entry.Value.Value)));
				sb.AppendLine("		</COLUMNS>");
			}
			foreach (KeyValuePair<string, ColumnClass> entry in Primary)
			{
				sb.AppendLine("		<KEYS>");
				sb.AppendLine(string.Format("			<COLUMN_NAME>{0}</COLUMN_NAME>", entry.Key));
				sb.AppendLine(string.Format("			<COLUMN_TYPE>{0}</COLUMN_TYPE>", entry.Value.CType));
				sb.AppendLine(string.Format("			<COLUMN_VALUE>{0}</COLUMN_VALUE>", Utility.Encode4bit(entry.Value.Value)));
				sb.AppendLine("		</KEYS>");
			}
			sb.AppendLine("	</Data>");
			sb.AppendLine("</Root>");
			return sb.ToString();
		}

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}

    public class TSCSongs
    {
        public char state = '?';

        private ColumnClass _title = new ColumnClass("Title", ColumnType._string);
        private ColumnClass _artist = new ColumnClass("Artist", ColumnType._string);
        private ColumnClass _disk = new ColumnClass("Disk", ColumnType._string);
        private ColumnClass _duetArtist = new ColumnClass("DuetArtist", ColumnType._string);
        private ColumnClass _oneDrive = new ColumnClass("OneDrive", ColumnType._string);
        private ColumnClass _filePath = new ColumnClass("FilePath", ColumnType._string);
        private ColumnClass _IsHelper = new ColumnClass("IsHelper", ColumnType._string);
        private ColumnClass _IsDuet = new ColumnClass("IsDuet", ColumnType._string);
        private string _whereClause = "";
        private string _pageOrder = "";
        private int _pageOffset = -1;
        private int _pageReturn = -1;
        private string _pageSearchString = "";

        public string Title { get { return _title.Value_string; } set { _title.Value_string = value; } }
        public string Artist { get { return _artist.Value_string; } set { _artist.Value_string = value; } }
        public string Disk { get { return _disk.Value_string; } set { _disk.Value_string = value; } }
        public string DuetArtist { get { return _duetArtist.Value_string; } set { _duetArtist.Value_string = value; } }
        public string OneDrive { get { return _oneDrive.Value_string; } set { _oneDrive.Value_string = value; } }
        public string FilePath { get { return _filePath.Value_string; } set { _filePath.Value_string = value; } }
        public string IsHelper { get { return _IsHelper.Value_string; } set { _IsHelper.Value_string = value; } }
        public string IsDuet { get { return _IsDuet.Value_string; } set { _IsDuet.Value_string = value; } }


        public string WhereClause { get { return _whereClause; } set { _whereClause = value; } }
        public string PageOrder { get { return _pageOrder; } set { _pageOrder = value; } }
        public int PageOffset { get { return _pageOffset; } set { _pageOffset = value; } }
        public int PageReturn { get { return _pageReturn; } set { _pageReturn = value; } }
        public string PageSearchString { get { return _pageSearchString; } set { _pageSearchString = value; } }

        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public Dictionary<string, ColumnClass> Columns = new Dictionary<string, ColumnClass>();
        public Dictionary<string, ColumnClass> Primary = new Dictionary<string, ColumnClass>();

        public TSCSongs()
        {
            Headers.Add("Title", "Title");
            Headers.Add("Artist", "Artist");
            Headers.Add("Disk", "Disk");
            Headers.Add("DuetArtist", "Duet Artist");
            Headers.Add("IsHelper", "Helper");
            Headers.Add("IsDuet", "Duet");
            Headers.Add("OneDrive", "OneDrive");
            Headers.Add("FilePath", "Path");

            Columns.Add("Title", _title);
            Columns.Add("Artist", _artist);
            Columns.Add("Disk", _disk);
            Columns.Add("DuetArtist", _duetArtist);
            Columns.Add("IsHelper", _IsHelper);
            Columns.Add("IsDuet", _IsDuet);
            Columns.Add("OneDrive", _oneDrive);
            Columns.Add("FilePath", _filePath);

            Primary.Add("Title", _title);
            Primary.Add("Artist", _artist);
            Primary.Add("Disk", _disk);
        }

        public TSCSongs(string title)
            : this()
        {
            _title = new ColumnClass("Title", ColumnType._string, title);
        }

        public TSCSongs(string title, string artist, string disk)
            : this()
        {
            _title = new ColumnClass("Title", ColumnType._string, title);
            _artist = new ColumnClass("Artist", ColumnType._string, artist);
            _disk = new ColumnClass("Disk", ColumnType._string, disk);
        }

        public TSCSongs(TSCSongs tscsongs)
            : this()
        {
            Title = new string(tscsongs.Title.ToArray());
            Artist = new string(tscsongs.Artist.ToArray());
            Disk = new string(tscsongs.Disk.ToArray());
            DuetArtist = new string(tscsongs.DuetArtist.ToArray());
            IsHelper = new string(tscsongs.IsHelper.ToArray());
            IsDuet = new string(tscsongs.IsDuet.ToArray());
            OneDrive = new string(tscsongs.OneDrive.ToArray());
            FilePath = new string(tscsongs.FilePath.ToArray());
        }


        public TSCSongs(XmlNode node)
            : this()
        {
            Title = Utility.GetXmlString(node, "Title");
            Artist = Utility.GetXmlString(node, "Artist");
            Disk = Utility.GetXmlString(node, "Disk");
            DuetArtist = Utility.GetXmlString(node, "DuetArtist");
            IsHelper = Utility.GetXmlString(node, "IsHelper");
            IsDuet = Utility.GetXmlString(node, "IsDuet");
            OneDrive = Utility.GetXmlString(node, "OneDrive");
            FilePath = Utility.GetXmlString(node, "FilePath");
        }


        public static bool operator ==(TSCSongs a, TSCSongs b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            return FieldsEqual(a, b);
        }

        public static bool operator !=(TSCSongs a, TSCSongs b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            TSCSongs a = obj as TSCSongs;
            if (a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public bool Equals(TSCSongs a)
        {
            if ((object)a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public static bool FieldsEqual(TSCSongs a, TSCSongs b)
        {
            return (
                (a.Title == b.Title) &&
                (a.Artist == b.Artist) &&
                (a.Disk == b.Disk) &&
                (a.DuetArtist == b.DuetArtist) &&
                (a.IsHelper == b.IsHelper) &&
                (a.IsDuet == b.IsDuet) &&
                (a.OneDrive == b.OneDrive) &&
                (a.FilePath == b.FilePath) &&
                (true)
                );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool KeyEquals(TSCSongs a)
        {
            if ((object)a == null)
                return false;
            return (
            a.Title.Trim().ToLower() == Title.Trim().ToLower() &&
            a.Artist.Trim().ToLower() == Artist.Trim().ToLower() &&
            a.Disk.Trim().ToLower() == Disk.Trim().ToLower()
            );
        }

        public string GetDataXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Root>");
            sb.AppendLine("	<Data>");
            if (WhereClause != null && WhereClause.Trim().Length > 0)
                sb.AppendLine(string.Format(" <WHERE>{0}</WHERE>", WhereClause));
            if (PageOffset >= 0 && PageReturn > 0)
                sb.AppendLine(string.Format(" <PAGE>{0} OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY</PAGE>", PageOrder, PageOffset, PageReturn));

            foreach (KeyValuePair<string, ColumnClass> entry in Columns)
            {
                bool iskey = false;
                foreach (KeyValuePair<string, ColumnClass> keyentry in Primary)
                {
                    if (keyentry.Key == entry.Key)
                    {
                        iskey = true;
                        break;
                    }
                }
                if (iskey == true)
                {
                    sb.AppendLine("		<COLUMNS>");
                    sb.AppendLine(string.Format("			<COLUMN_NAME>{0}</COLUMN_NAME>", entry.Key));
                    sb.AppendLine(string.Format("			<COLUMN_TYPE>{0}</COLUMN_TYPE>", entry.Value.CType));
                    sb.AppendLine(string.Format("			<COLUMN_VALUE>{0}</COLUMN_VALUE>", Utility.Encode4bit(entry.Value.Value)));
                    sb.AppendLine("		</COLUMNS>");
                }
            }
            foreach (KeyValuePair<string, ColumnClass> entry in Primary)
            {
                sb.AppendLine("		<KEYS>");
                sb.AppendLine(string.Format("			<COLUMN_NAME>{0}</COLUMN_NAME>", entry.Key));
                sb.AppendLine(string.Format("			<COLUMN_TYPE>{0}</COLUMN_TYPE>", entry.Value.CType));
                sb.AppendLine(string.Format("			<COLUMN_VALUE>{0}</COLUMN_VALUE>", Utility.Encode4bit(entry.Value.Value)));
                sb.AppendLine("		</KEYS>");
            }
            sb.AppendLine("	</Data>");
            sb.AppendLine("</Root>");
            return sb.ToString();
        }
    }

    public class TSCSingersSongs : INotifyPropertyChanged
    {
        public char state = '?';

        private ColumnClass _singerKey = new ColumnClass("SingerKey", ColumnType._string);
        private ColumnClass _title = new ColumnClass("Title", ColumnType._string);
        private ColumnClass _artist = new ColumnClass("Artist", ColumnType._string);
        private ColumnClass _url = new ColumnClass("URL", ColumnType._string);
        private ColumnClass _date = new ColumnClass("Date", ColumnType._DateTime);
        private string _whereClause = "";
        private string _pageOrder = "";
        private int _pageOffset = -1;
        private int _pageReturn = -1;
        private string _pageSearchString = "";

        public string SingerKey { get { return _singerKey.Value_string; } set { _singerKey.Value_string = value; NotifyPropertyChanged(); } }
        public string Title { get { return _title.Value_string; } set { _title.Value_string = value; NotifyPropertyChanged(); } }
        public string Artist { get { return _artist.Value_string; } set { _artist.Value_string = value; NotifyPropertyChanged(); } }
        public string URL { get { return _url.Value_string; } set { _url.Value_string = value; NotifyPropertyChanged(); } }
        public DateTime Date { get { return _date.Value_DateTime; } set { _date.Value_DateTime = value; NotifyPropertyChanged(); } }

        public string WhereClause { get { return _whereClause; } set { _whereClause = value; } }
        public string PageOrder { get { return _pageOrder; } set { _pageOrder = value; } }
        public int PageOffset { get { return _pageOffset; } set { _pageOffset = value; } }
        public int PageReturn { get { return _pageReturn; } set { _pageReturn = value; } }
        public string PageSearchString { get { return _pageSearchString; } set { _pageSearchString = value; } }

        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public Dictionary<string, ColumnClass> Columns = new Dictionary<string, ColumnClass>();
        public Dictionary<string, ColumnClass> Primary = new Dictionary<string, ColumnClass>();

        public event PropertyChangedEventHandler PropertyChanged;

        public TSCSingersSongs()
        {
            Headers.Add("SingerKey", "Singer");
            Headers.Add("Title", "Title");
            Headers.Add("Artist", "Artist");
            Headers.Add("URL", "URL");
            Headers.Add("Date", "Date");

            Columns.Add("SingerKey", _singerKey);
            Columns.Add("Title", _title);
            Columns.Add("Artist", _artist);
            Columns.Add("URL", _url);
            Columns.Add("Date", _date);

            Primary.Add("SingerKey", _singerKey);
            Primary.Add("Title", _title);
            Primary.Add("Artist", _artist);
        }

        public TSCSingersSongs(string singerkey)
            : this()
        {
            _singerKey = new ColumnClass("Title", ColumnType._string, singerkey);
        }

        public TSCSingersSongs(string singerkey, string title, string artist)
            : this()
        {
            _singerKey.Value = singerkey;
            _title.Value = title;
            _artist.Value = artist;
        }

        public TSCSingersSongs(TSCSingersSongs tscsongs)
            : this()
        {
            SingerKey = new string(tscsongs.SingerKey.ToArray());
            Title = new string(tscsongs.Title.ToArray());
            Artist = new string(tscsongs.Artist.ToArray());
            URL = new string(tscsongs.URL.ToArray());
            Date = tscsongs.Date;
        }


        public TSCSingersSongs(XmlNode node)
            : this()
        {
            SingerKey = Utility.GetXmlString(node, "SingerKey");
            Title = Utility.GetXmlString(node, "Title");
            Artist = Utility.GetXmlString(node, "Artist");
            URL = Utility.GetXmlString(node, "URL");
            Date = Utility.GetXmlDateTime(node, "Date");
        }


        public static bool operator ==(TSCSingersSongs a, TSCSingersSongs b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            return FieldsEqual(a, b);
        }

        public static bool operator !=(TSCSingersSongs a, TSCSingersSongs b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            TSCSingersSongs a = obj as TSCSingersSongs;
            if (a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public bool Equals(TSCSingersSongs a)
        {
            if ((object)a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public static bool FieldsEqual(TSCSingersSongs a, TSCSingersSongs b)
        {
            return (
                (a.SingerKey == b.SingerKey) &&
                (a.Title == b.Title) &&
                (a.Artist == b.Artist) &&
                (a.URL == b.URL) &&
                (true)
                );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool KeyEquals(TSCSingersSongs a)
        {
            if ((object)a == null)
                return false;
            return (
            a.SingerKey.Trim().ToLower() == SingerKey.Trim().ToLower() &&
            a.Title.Trim().ToLower() == Title.Trim().ToLower() &&
            a.Artist.Trim().ToLower() == Artist.Trim().ToLower()
            );
        }

        public string GetDataXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Root>");
            sb.AppendLine("	<Data>");
            if (WhereClause != null && WhereClause.Trim().Length > 0)
                sb.AppendLine(string.Format(" <WHERE>{0}</WHERE>", WhereClause));
            if (PageOffset >= 0 && PageReturn > 0)
                sb.AppendLine(string.Format(" <PAGE>{0} OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY</PAGE>", PageOrder, PageOffset, PageReturn));

            foreach (KeyValuePair<string, ColumnClass> entry in Columns)
            {
                //bool iskey = false;
                //foreach (KeyValuePair<string, ColumnClass> keyentry in Primary)
                //{
                //    if (keyentry.Key == entry.Key)
                //    {
                //        iskey = true;
                //        break;
                //    }
                //}
                //if (iskey == true)
                //{
                sb.AppendLine("		<COLUMNS>");
                sb.AppendLine(string.Format("			<COLUMN_NAME>{0}</COLUMN_NAME>", entry.Key));
                sb.AppendLine(string.Format("			<COLUMN_TYPE>{0}</COLUMN_TYPE>", entry.Value.CType));
                sb.AppendLine(string.Format("			<COLUMN_VALUE>{0}</COLUMN_VALUE>", Utility.Encode4bit(entry.Value.Value)));
                sb.AppendLine("		</COLUMNS>");
                //}
            }
            foreach (KeyValuePair<string, ColumnClass> entry in Primary)
            {
                sb.AppendLine("		<KEYS>");
                sb.AppendLine(string.Format("			<COLUMN_NAME>{0}</COLUMN_NAME>", entry.Key));
                sb.AppendLine(string.Format("			<COLUMN_TYPE>{0}</COLUMN_TYPE>", entry.Value.CType));
                sb.AppendLine(string.Format("			<COLUMN_VALUE>{0}</COLUMN_VALUE>", Utility.Encode4bit(entry.Value.Value)));
                sb.AppendLine("		</KEYS>");
            }
            sb.AppendLine("	</Data>");
            sb.AppendLine("</Root>");
            return sb.ToString();
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

	public enum ColumnType
	{
		_none,
		_string,
		_int,
		_DateTime
	}
	public class ColumnClass
	{
		private string _name;
		private ColumnType _ctype;
		private string _value_string;
		private int _value_int;
		private DateTime _value_datetime;

		public string Name { get { return _name; } }
		public int Value_int { get { return _value_int; } set { if (_ctype == ColumnType._int) _value_int = value; } }
		public DateTime Value_DateTime { get { return _value_datetime; } set { if (_ctype == ColumnType._DateTime) _value_datetime = value; } }
		public String Value_string { get { return _value_string; } set { if (_ctype == ColumnType._string) _value_string = value; } }
		public string Value
		{
			get
			{
				switch (_ctype)
				{
					case ColumnType._DateTime:
						return _value_datetime.ToString();
					case ColumnType._int:
						return _value_int.ToString();
					case ColumnType._string:
						return _value_string;
					default:
						return null;
				};
			}
			set
			{
				switch (_ctype)
				{
					case ColumnType._DateTime:
						_value_datetime = DateTime.Parse(value);
						break;
					case ColumnType._int:
						_value_int = int.Parse(value);
						break;
					case ColumnType._string:
						_value_string = value;
						break;
				}
			}
		}
		public string CType
		{
			get
			{
				switch (_ctype)
				{
					case ColumnType._string:
						return "string";
					case ColumnType._int:
						return "int";
					case ColumnType._DateTime:
						return "DateTime";
					default:
						return "";
				}
			}
		}

		public ColumnClass(string name, ColumnType ctype)
		{
			_name = name;
			_ctype = ctype;
		}

		public ColumnClass(string name, ColumnType ctype, string value)
			: this(name, ctype)
		{
			Value_string = value;
		}

		public ColumnClass(string name, ColumnType ctype, int value)
			: this(name, ctype)
		{
			Value_int = value;
		}

		public ColumnClass(string name, ColumnType ctype, DateTime value)
			: this(name, ctype)
		{
			Value_DateTime = value;
		}
	}
}
