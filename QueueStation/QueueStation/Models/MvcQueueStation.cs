using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Web.Helpers;
using System.Linq;
using System.Web;

namespace QueueStation.Models
{
    public class MvcList : IEnumerable
    {
        private List<object> m_Items = new List<object>();

        public MvcList()
        {
        }

        public void Add(object item)
        {
            m_Items.Add(item);
        }

        public int Count { get { int i = 0; foreach (object o in m_Items) { i++; } return i; } }

        // IEnumerable Member
        public IEnumerator GetEnumerator()
        {
            foreach (object o in m_Items)
            {
                // Lets check for end of list (its bad code since we used arrays)
                if (o == null)
                {
                    break;
                }

                // Return the current element and then on next function call 
                // resume from next element rather than starting all over again;
                yield return o;
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
        private string _name = "";
        private ColumnType _ctype = ColumnType._string;
        private string _value_string = "";
        private int _value_int = -1;
        private DateTime _value_datetime = DateTime.MinValue;

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
        public ColumnType CTypeEnum { get { return _ctype; } }

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

    public class SingerHistoryRecord
    {
        public string SingerKey;
        public string Song;
        public string Artist;
        public string TSCEvent;
        public string Note;
        public string Link;
        public SingerHistoryRecord()
        {
        }
        public SingerHistoryRecord(string tscevent, string singer, string song, string artist, string note, string link)
        {
            TSCEvent = tscevent;
            SingerKey = singer;
            Song = song;
            Artist = artist;
            Note = note;
            Link = link;
        }
    }

    public class SingerHistoryStore
    {
        public string SingerName;
        public List<SingerHistoryRecord> history;
        public SingerHistoryStore()
        {
            history = new List<SingerHistoryRecord>();
        }
        public SingerHistoryStore(string singerName)
            : this()
        {
            SingerName = singerName;
        }
    }

    public class TSCQueue : IComparable
    {
        public char state = '?';

        private ColumnClass _eventKey = new ColumnClass("EventKey", ColumnType._string);
        private ColumnClass _singerKey = new ColumnClass("SingerKey", ColumnType._string);
        private ColumnClass _queueRound = new ColumnClass("QueueRound", ColumnType._int);
        private ColumnClass _queueOrder = new ColumnClass("QueueOrder", ColumnType._int);
        private ColumnClass _queueSong = new ColumnClass("QueueSong", ColumnType._string);
        private ColumnClass _queueArtist = new ColumnClass("QueueArtist", ColumnType._string);
        private ColumnClass _queueNote = new ColumnClass("QueueNote", ColumnType._string);
        private ColumnClass _queueLink = new ColumnClass("QueueLink", ColumnType._string);
        private ColumnClass _queueState = new ColumnClass("QueueState", ColumnType._string);

        public string EventKey { get { return _eventKey.Value_string; } set { _eventKey.Value_string = value; } }
        public string SingerKey { get { return _singerKey.Value_string; } set { _singerKey.Value_string = value; } }
        public int QueueRound { get { return _queueRound.Value_int; } set { _queueRound.Value_int = value; } }
        public int QueueOrder { get { return _queueOrder.Value_int; } set { _queueOrder.Value_int = value; } }
        public string QueueSong { get { return _queueSong.Value_string; } set { _queueSong.Value_string = value; } }
        public string QueueArtist { get { return _queueArtist.Value_string; } set { _queueArtist.Value_string = value; } }
        public string QueueNote { get { return _queueNote.Value_string; } set { _queueNote.Value_string = value; } }
        public string QueueLink { get { return _queueLink.Value_string; } set { _queueLink.Value_string = value; } }
        public string QueueState { get { return _queueState.Value_string; } set { _queueState.Value_string = value; } }

        public string QueueStateBorder { 
            get {
                return "border:double";
            } 
        }

        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public Dictionary<string, ColumnClass> Columns = new Dictionary<string, ColumnClass>();
        public Dictionary<string, ColumnClass> Primary = new Dictionary<string, ColumnClass>();

        public TSCQueue()
        {
            Headers.Add("EventKey", "Key");
            Headers.Add("SingerKey", "Key");
            Headers.Add("QueueRound", "Round");
            Headers.Add("QueueOrder", "Order");
            Headers.Add("QueueSong", "Song");
            Headers.Add("QueueArtist", "Artist");
            Headers.Add("QueueNote", "Note");
            Headers.Add("QueueLink", "Link");
            Headers.Add("QueueState", "State");

            Columns.Add("EventKey", _eventKey);
            Columns.Add("SingerKey", _singerKey);
            Columns.Add("QueueRound", _queueRound);
            Columns.Add("QueueOrder", _queueOrder);
            Columns.Add("QueueSong", _queueSong);
            Columns.Add("QueueArtist", _queueArtist);
            Columns.Add("QueueNote", _queueNote);
            Columns.Add("QueueLink", _queueLink);
            Columns.Add("QueueState", _queueState);

            Primary.Add("EventKey", _eventKey);
            Primary.Add("QueueRound", _queueRound);
            Primary.Add("SingerKey", _singerKey);
        }

        public TSCQueue(string eventKey, int queueRound, string singerKey)
            : this()
        {
            _eventKey = new ColumnClass("EventKey", ColumnType._string, eventKey);
            _queueRound = new ColumnClass("QueueRound", ColumnType._int, queueRound);
            _singerKey = new ColumnClass("SingerKey", ColumnType._string, singerKey);
        }

        public TSCQueue(TSCQueue tscqueue)
            : this()
        {
            EventKey = new string(tscqueue.EventKey.ToArray());
            SingerKey = new string(tscqueue.SingerKey.ToArray());
            QueueRound = tscqueue.QueueRound;
            QueueOrder = tscqueue.QueueOrder;
            QueueSong = new string(tscqueue.QueueSong.ToArray());
            QueueArtist = new string(tscqueue.QueueArtist.ToArray());
            QueueNote = new string(tscqueue.QueueNote.ToArray());
            QueueLink = new string(tscqueue.QueueLink.ToArray());
            QueueState = new string(tscqueue.QueueState.ToArray());
        }

        public TSCQueue(XmlNode node)
            : this()
        {
            EventKey = Utility.GetXmlString(node, "EventKey");
            SingerKey = Utility.GetXmlString(node, "SingerKey");
            QueueRound = Utility.GetXmlInteger(node, "QueueRound");
            QueueOrder = Utility.GetXmlInteger(node, "QueueOrder");
            QueueSong = Utility.GetXmlString(node, "QueueSong");
            QueueArtist = Utility.GetXmlString(node, "QueueArtist");
            QueueNote = Utility.GetXmlString(node, "QueueNote");
            QueueLink = Utility.GetXmlString(node, "QueueLink");
            QueueState = Utility.GetXmlString(node, "QueueState");
        }

        public bool AppendToDocument(XmlDocument doc)
        {
            XmlNode data = doc.CreateNode(XmlNodeType.Element, "Data", null);
            XmlNode root = doc.SelectSingleNode("/Root");
            if (data != null && root != null)
            {
                if (EventKey != null && EventKey.Trim().Length > 0)
                    AddElement(doc, data, "EventKey", EventKey);
                if (SingerKey != null && SingerKey.Trim().Length > 0)
                    AddElement(doc, data, "SingerKey", SingerKey);
                if (QueueRound > 0)
                    AddElement(doc, data, "QueueRound", QueueRound.ToString());
                if (QueueOrder > 0)
                    AddElement(doc, data, "QueueOrder", QueueOrder.ToString());
                if (QueueSong != null && QueueSong.Trim().Length > 0)
                    AddElement(doc, data, "QueueSong", QueueSong);
                if (QueueArtist != null && QueueArtist.Trim().Length > 0)
                    AddElement(doc, data, "QueueArtist", QueueArtist);
                if (QueueNote != null && QueueNote.Trim().Length > 0)
                    AddElement(doc, data, "QueueNote", QueueNote);
                if (QueueLink != null && QueueLink.Trim().Length > 0)
                    AddElement(doc, data, "QueueLink", QueueLink);
                if (QueueState != null && QueueState.Trim().Length > 0)
                    AddElement(doc, data, "QueueState", QueueState);
                root.AppendChild(data);
            }
            return true;
        }

        private bool AddElement(XmlDocument doc, XmlNode root, string name, string value)
        {
            XmlNode node = doc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            root.AppendChild(node);
            return true;
        }

        public string GetRowEdit()
        {
            return "~/round";
        }
        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            TSCQueue q = obj as TSCQueue;
            if (q == null)
                return 1;
            return QueueOrder.CompareTo(q.QueueOrder);
        }

        public static bool operator ==(TSCQueue a, TSCQueue b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            return FieldsEqual(a, b);
        }

        public static bool operator !=(TSCQueue a, TSCQueue b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            TSCQueue a = obj as TSCQueue;
            if (a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public bool Equals(TSCQueue a)
        {
            if ((object)a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public static bool FieldsEqual(TSCQueue a, TSCQueue b)
        {
            return (
                (a.EventKey == b.EventKey) &&
                (a.SingerKey == b.SingerKey) &&
                (a.QueueRound == b.QueueRound) &&
                (a.QueueSong == b.QueueSong) &&
                (a.QueueArtist == b.QueueArtist) &&
                (a.QueueNote == b.QueueNote) &&
                (a.QueueLink == b.QueueLink) &&
                (a.QueueState == b.QueueState) &&
                (true)
                );
        }

        public static bool RoundsEqual(TSCQueue a, TSCQueue b)
        {
            return (
                (a.QueueSong == b.QueueSong) &&
                (a.QueueArtist == b.QueueArtist) &&
                (a.QueueNote == b.QueueNote) &&
                (a.QueueLink == b.QueueLink) &&
                (a.QueueState == b.QueueState) &&
                (true)
                );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool KeyEquals(TSCQueue a)
        {
            if ((object)a == null)
                return false;
            return (
            (a.EventKey.Trim().ToLower() == EventKey.Trim().ToLower()) &&
            (a.QueueRound == QueueRound) &&
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
    }


    public class TSCEvents : IComparable
    {
        public char state = '?';

        private ColumnClass _eventKey = new ColumnClass("EventKey", ColumnType._string);
        private ColumnClass _eventName = new ColumnClass("EventName", ColumnType._string);
        private ColumnClass _venueKey = new ColumnClass("VenueKey", ColumnType._string);
        private ColumnClass _eventDate = new ColumnClass("EventDate", ColumnType._DateTime);
        private ColumnClass _eventEmail = new ColumnClass("EventEmail", ColumnType._string);
        private ColumnClass _eventAddress = new ColumnClass("EventAddress", ColumnType._string);

        public string EventKey { get { return _eventKey.Value_string; } set { _eventKey.Value_string = value; } }
        public string EventName { get { return _eventName.Value_string; } set { _eventName.Value_string = value; } }
        public string VenueKey { get { return _venueKey.Value_string; } set { _venueKey.Value_string = value; } }
        public DateTime EventDate { get { return _eventDate.Value_DateTime; } set { _eventDate.Value_DateTime = value; } }
        public string EventEmail { get { return _eventEmail.Value_string; } set { _eventEmail.Value_string = value; } }
        public string EventAddress { get { return _eventAddress.Value_string; } set { _eventAddress.Value_string = value; } }

        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public Dictionary<string, ColumnClass> Columns = new Dictionary<string, ColumnClass>();
        public Dictionary<string, ColumnClass> Primary = new Dictionary<string, ColumnClass>();

        public TSCEvents()
        {
            Headers.Add("EventKey", "Key");
            Headers.Add("EventName", "Name");
            Headers.Add("VenueKey", "Key");
            Headers.Add("EventDate", "Date");
            Headers.Add("EventEmail", "Email");
            Headers.Add("EventAddress", "Address");

            Columns.Add("EventKey", _eventKey);
            Columns.Add("EventName", _eventName);
            Columns.Add("VenueKey", _venueKey);
            Columns.Add("EventDate", _eventDate);
            Columns.Add("EventEmail", _eventEmail);
            Columns.Add("EventAddress", _eventAddress);

            Primary.Add("EventKey", _eventKey);
        }

        public TSCEvents(string eventKey)
            : this()
        {
            _eventKey = new ColumnClass("EventKey", ColumnType._string, eventKey);
        }

        public TSCEvents(TSCEvents tscevents)
            : this()
        {
            EventKey = new string(tscevents.EventKey.ToArray());
            EventName = new string(tscevents.EventName.ToArray());
            VenueKey = new string(tscevents.VenueKey.ToArray());
            EventDate = tscevents.EventDate;
            EventEmail = new string(tscevents.EventEmail.ToArray());
            EventAddress = new string(tscevents.EventAddress.ToArray());
        }

        public TSCEvents(XmlNode node)
            : this()
        {
            EventKey = Utility.GetXmlString(node, "EventKey");
            EventName = Utility.GetXmlString(node, "EventName");
            VenueKey = Utility.GetXmlString(node, "VenueKey");
            EventDate = Utility.GetXmlDateTime(node, "EventDate");
            EventEmail = Utility.GetXmlString(node, "EventEmail");
            EventAddress = Utility.GetXmlString(node, "EventAddress");
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            TSCEvents e = obj as TSCEvents;
            if (e == null)
                return 1;
            return EventDate.CompareTo(e.EventDate);
        }

        public static bool operator ==(TSCEvents a, TSCEvents b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            return FieldsEqual(a, b);
        }

        public static bool operator !=(TSCEvents a, TSCEvents b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            TSCEvents a = obj as TSCEvents;
            if (a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public bool Equals(TSCEvents a)
        {
            if ((object)a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public static bool FieldsEqual(TSCEvents a, TSCEvents b)
        {
            return (
                (a.EventKey == b.EventKey) &&
                (a.EventName == b.EventName) &&
                (a.VenueKey == b.VenueKey) &&
                (a.EventDate == b.EventDate) &&
                (a.EventEmail == b.EventEmail) &&
                (a.EventAddress == b.EventAddress) &&
                (true)
                );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool KeyEquals(TSCEvents a)
        {
            if ((object)a == null)
                return false;
            return (
            (a.EventKey.Trim().ToLower() == EventKey.Trim().ToLower())
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


        public TSCSingers(XmlNode node)
            : this()
        {
            SingerKey = Utility.GetXmlString(node, "SingerKey");
            SingerName = Utility.GetXmlString(node, "SingerName");
            SingerEmail = Utility.GetXmlString(node, "SingerEmail");
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

        public static bool FieldsEqual(TSCSingers a, TSCSingers b)
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


    public class TSCVenues : INotifyPropertyChanged
    {
        public char state = '?';

        private ColumnClass _venueKey = new ColumnClass("VenueKey", ColumnType._string);
        private ColumnClass _venueName = new ColumnClass("VenueName", ColumnType._string);
        private ColumnClass _venueEmail = new ColumnClass("VenueEmail", ColumnType._string);
        private ColumnClass _venueAddress = new ColumnClass("VenueAddress", ColumnType._string);
        private ColumnClass _venueContact = new ColumnClass("VenueContact", ColumnType._string);
        private ColumnClass _venuePhone = new ColumnClass("VenuePhone", ColumnType._string);

        public string VenueKey { get { return _venueKey.Value_string; } set { _venueKey.Value_string = value; NotifyPropertyChanged(); } }
        public string VenueName { get { return _venueName.Value_string; } set { _venueName.Value_string = value; NotifyPropertyChanged(); } }
        public string VenueEmail { get { return _venueEmail.Value_string; } set { _venueEmail.Value_string = value; NotifyPropertyChanged(); } }
        public string VenueAddress { get { return _venueAddress.Value_string; } set { _venueAddress.Value_string = value; NotifyPropertyChanged(); } }
        public string VenueContact { get { return _venueContact.Value_string; } set { _venueContact.Value_string = value; NotifyPropertyChanged(); } }
        public string VenuePhone { get { return _venuePhone.Value_string; } set { _venuePhone.Value_string = value; NotifyPropertyChanged(); } }

        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public Dictionary<string, ColumnClass> Columns = new Dictionary<string, ColumnClass>();
        public Dictionary<string, ColumnClass> Primary = new Dictionary<string, ColumnClass>();

        public event PropertyChangedEventHandler PropertyChanged;

        public TSCVenues()
        {
            Headers.Add("VenueKey", "Key");
            Headers.Add("VenueName", "Name");
            Headers.Add("VenueEmail", "Email");
            Headers.Add("VenueAddress", "Address");
            Headers.Add("VenueContact", "Contact");
            Headers.Add("VenuePhone", "Phone");

            Columns.Add("VenueKey", _venueKey);
            Columns.Add("VenueName", _venueName);
            Columns.Add("VenueEmail", _venueEmail);
            Columns.Add("VenueAddress", _venueAddress);
            Columns.Add("VenueContact", _venueContact);
            Columns.Add("VenuePhone", _venuePhone);

            Primary.Add("VenueKey", _venueKey);
        }

        public TSCVenues(string venueKey)
            : this()
        {
            _venueKey = new ColumnClass("VenueKey", ColumnType._string, venueKey);
        }

        public TSCVenues(TSCVenues tscvenues)
            : this()
        {
            VenueKey = new string(tscvenues.VenueKey.ToArray());
            VenueName = new string(tscvenues.VenueName.ToArray());
            VenueEmail = new string(tscvenues.VenueEmail.ToArray());
            VenueAddress = new string(tscvenues.VenueAddress.ToArray());
            VenueContact = new string(tscvenues.VenueContact.ToArray());
            VenuePhone = new string(tscvenues.VenuePhone.ToArray());
        }

        public TSCVenues(XmlNode node)
            : this()
        {
            VenueKey = Utility.GetXmlString(node, "VenueKey");
            VenueName = Utility.GetXmlString(node, "VenueName");
            VenueEmail = Utility.GetXmlString(node, "VenueEmail");
            VenueAddress = Utility.GetXmlString(node, "VenueAddress");
            VenueContact = Utility.GetXmlString(node, "VenueContact");
            VenuePhone = Utility.GetXmlString(node, "VenuePhone");
        }

        public static bool operator ==(TSCVenues a, TSCVenues b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            return FieldsEqual(a, b);
        }

        public static bool operator !=(TSCVenues a, TSCVenues b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            TSCVenues a = obj as TSCVenues;
            if (a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public bool Equals(TSCVenues a)
        {
            if ((object)a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public static bool FieldsEqual(TSCVenues a, TSCVenues b)
        {
            return (
                (a.VenueKey == b.VenueKey) &&
                (a.VenueName == b.VenueName) &&
                (a.VenueEmail == b.VenueEmail) &&
                (a.VenueAddress == b.VenueAddress) &&
                (a.VenueContact == b.VenueContact) &&
                (a.VenuePhone == b.VenuePhone) &&
                (true)
                );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool KeyEquals(TSCVenues a)
        {
            if ((object)a == null)
                return false;
            return (
            (a.VenueKey.Trim().ToLower() == VenueKey.Trim().ToLower())
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

    public class TSCSongs : INotifyPropertyChanged
    {
        public char state = '?';

        private ColumnClass _title = new ColumnClass("Title", ColumnType._string);
        private ColumnClass _artist = new ColumnClass("Artist", ColumnType._string);
        private ColumnClass _disk = new ColumnClass("Disk", ColumnType._string);
        private ColumnClass _duetArtist = new ColumnClass("DuetArtist", ColumnType._string);
        private ColumnClass _oneDriveMP4 = new ColumnClass("OneDriveMP4", ColumnType._string);
        private ColumnClass _oneDriveZip = new ColumnClass("OneDriveZip", ColumnType._string);
        private ColumnClass _filePath = new ColumnClass("FilePath", ColumnType._string);
        private string _whereClause = "";
        private string _pageOrder = "";
        private bool _pageByDisk = false;
        private int _pageOffset = -1;
        private int _pageReturn = -1;
        private string _pageSearchString = "";

        public string Title { get { return _title.Value_string; } set { _title.Value_string = value; NotifyPropertyChanged(); } }
        public string Artist { get { return _artist.Value_string; } set { _artist.Value_string = value; NotifyPropertyChanged(); } }
        public string Disk { get { return _disk.Value_string; } set { _disk.Value_string = value; NotifyPropertyChanged(); } }
        public string DuetArtist { get { return _duetArtist.Value_string; } set { _duetArtist.Value_string = value; NotifyPropertyChanged(); } }
        public string OneDriveMP4 { get { return _oneDriveMP4.Value_string; } set { _oneDriveMP4.Value_string = value; NotifyPropertyChanged(); } }
        public string OneDriveZip { get { return _oneDriveZip.Value_string; } set { _oneDriveZip.Value_string = value; NotifyPropertyChanged(); } }
        public string FilePath { get { return _filePath.Value_string; } set { _filePath.Value_string = value; NotifyPropertyChanged(); } }
        public string SongKey { get { return _artist.Value_string + "_" + _title.Value_string + "_" + _disk.Value_string; } }

        public string WhereClause { get { return _whereClause; } set { _whereClause = value; } }
        public string PageOrder { get { return _pageOrder; } set { _pageOrder = value; } }
        public string ByPageOrder 
        { 
            get 
            { 
                if (PageOrder.ToLower().Contains("title"))
                {
                    if (PageByDisk == true)
                        return "[Disk], [Title], [Artist]";
                    else
                        return "[Title], [Artist], [Disk]";
                }
                else
                {
                    if (PageByDisk == true)
                        return "[Disk], [Artist], [Title]";
                    else
                        return "[Artist], [Title], [Disk]";
                }
            }
        }
        public bool PageByDisk { get { return _pageByDisk; } set { _pageByDisk = value; } }
        public int PageOffset { get { return _pageOffset; } set { _pageOffset = value; } }
        public int PageReturn { get { return _pageReturn; } set { _pageReturn = value; } }
        public string PageSearchString { get { return _pageSearchString; } set { _pageSearchString = value; } }

        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public Dictionary<string, ColumnClass> Columns = new Dictionary<string, ColumnClass>();
        public Dictionary<string, ColumnClass> Primary = new Dictionary<string, ColumnClass>();

        public event PropertyChangedEventHandler PropertyChanged;

        public TSCSongs()
        {
            Headers.Add("Title", "Title");
            Headers.Add("Artist", "Artist");
            Headers.Add("Disk", "Disk");
            Headers.Add("DuetArtist", "Duet Artist");
            Headers.Add("OneDriveMP4", "OneDriveMP4");
            Headers.Add("OneDriveZip", "OneDriveZip");
            Headers.Add("FilePath", "Path");

            Columns.Add("Title", _title);
            Columns.Add("Artist", _artist);
            Columns.Add("Disk", _disk);
            Columns.Add("DuetArtist", _duetArtist);
            Columns.Add("OneDriveMP4", _oneDriveMP4);
            Columns.Add("OneDriveZip", _oneDriveZip);
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
            OneDriveMP4 = new string(tscsongs.OneDriveMP4.ToArray());
            OneDriveZip = new string(tscsongs.OneDriveZip.ToArray());
            FilePath = new string(tscsongs.FilePath.ToArray());
        }


        public TSCSongs(XmlNode node)
            : this()
        {
            Title = Utility.GetXmlString(node, "Title");
            Artist = Utility.GetXmlString(node, "Artist");
            Disk = Utility.GetXmlString(node, "Disk");
            DuetArtist = Utility.GetXmlString(node, "DuetArtist");
            OneDriveMP4 = Utility.GetXmlString(node, "OneDriveMP4");
            OneDriveZip = Utility.GetXmlString(node, "OneDriveZip");
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
                (a.OneDriveMP4 == b.OneDriveMP4) &&
                (a.OneDriveZip == b.OneDriveZip) &&
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
                sb.AppendLine(string.Format(" <PAGE>{0} OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY</PAGE>", ByPageOrder, PageOffset, PageReturn));

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

    public class TSCSingersSongs : INotifyPropertyChanged
    {
        public char state = '?';

        private ColumnClass _singerKey = new ColumnClass("SingerKey", ColumnType._string);
        private ColumnClass _title = new ColumnClass("Title", ColumnType._string);
        private ColumnClass _artist = new ColumnClass("Artist", ColumnType._string);
        private ColumnClass _onedrivemp4 = new ColumnClass("OneDriveMP4", ColumnType._string);
        private ColumnClass _onedrivezip = new ColumnClass("OneDriveZip", ColumnType._string);
        private ColumnClass _path = new ColumnClass("Path", ColumnType._string);
        private ColumnClass _date = new ColumnClass("Date", ColumnType._DateTime);
        private string _whereClause = "";
        private string _pageOrder = "";
        private int _pageOffset = -1;
        private int _pageReturn = -1;
        private string _pageSearchString = "";

        public string SingerKey { get { return _singerKey.Value_string; } set { _singerKey.Value_string = value; NotifyPropertyChanged(); } }
        public string Title { get { return _title.Value_string; } set { _title.Value_string = value; NotifyPropertyChanged(); } }
        public string Artist { get { return _artist.Value_string; } set { _artist.Value_string = value; NotifyPropertyChanged(); } }
        public string OneDriveMP4 { get { return _onedrivemp4.Value_string; } set { _onedrivemp4.Value_string = value; NotifyPropertyChanged(); } }
        public string OneDriveZip { get { return _onedrivezip.Value_string; } set { _onedrivezip.Value_string = value; NotifyPropertyChanged(); } }
        public string Path { get { return _path.Value_string; } set { _path.Value_string = value; NotifyPropertyChanged(); } }
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
            Headers.Add("OneDriveMP4", "OneDriveMP4");
            Headers.Add("OneDriveZip", "OneDriveZip");
            Headers.Add("Path", "Path");
            Headers.Add("Date", "Date");

            Columns.Add("SingerKey", _singerKey);
            Columns.Add("Title", _title);
            Columns.Add("Artist", _artist);
            Columns.Add("OneDriveMP4", _onedrivemp4);
            Columns.Add("OneDriveZip", _onedrivezip);
            Columns.Add("Path", _path);
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
            OneDriveMP4 = new string(tscsongs.OneDriveMP4.ToArray());
            OneDriveZip = new string(tscsongs.OneDriveZip.ToArray());
            Path = new string(tscsongs.Path.ToArray());
            Date = tscsongs.Date;
        }


        public TSCSingersSongs(XmlNode node)
            : this()
        {
            SingerKey = Utility.GetXmlString(node, "SingerKey");
            Title = Utility.GetXmlString(node, "Title");
            Artist = Utility.GetXmlString(node, "Artist");
            OneDriveMP4 = Utility.GetXmlString(node, "OneDriveMP4");
            OneDriveZip = Utility.GetXmlString(node, "OneDriveZip");
            Path = Utility.GetXmlString(node, "Path");
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
                (a.OneDriveMP4 == b.OneDriveMP4) &&
                (a.OneDriveZip == b.OneDriveZip) &&
                (a.Path == b.Path) &&
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
                foreach (KeyValuePair<string, ColumnClass> keyentry in Primary)
                {
                    if (keyentry.Key == entry.Key)
                    {
                        if (entry.Value.CTypeEnum == ColumnType._DateTime)
                        {
                            if (entry.Value.Value == null || entry.Value.Value_DateTime == DateTime.MinValue)
                                return "Primary key of type DateTime cannot be null";
                        }
                    }
                }
                if (!(entry.Value.CTypeEnum == ColumnType._DateTime && entry.Value.Value_DateTime == DateTime.MinValue))
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

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}