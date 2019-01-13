using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;

using XmlUtility;

namespace WcfSingingClub
{
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

    public class SongItem
    {
        public SongItem() { }
        public SongItem(XmlNode node)
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
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Disk { get; set; }
        public string IsHelper { get; set; }
        public string IsDuet { get; set; }
        public string DuetArtist { get; set; }
        public string FilePath { get; set; }
        public string OneDrive { get; set; }
    }
}