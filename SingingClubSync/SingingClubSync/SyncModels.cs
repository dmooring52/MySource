using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Web;

using SingingClubSync;

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
    /*
    public class ControlTag
    {
        public QueuerForm queuerForm = null;
        public VenuesControl venuesControl = null;
        public EventsControl eventsControl = null;
        public SingersControl signersControl = null;
        public QueueControl queueControl1 = null;
        public QueueControl queueControl2 = null;
        public QueueControl queueControl3 = null;
        public QueueControl queueControl4 = null;
        public QueueControl queueControl5 = null;
        public QueueControl queueControl6 = null;

        public string venuesXml = "";
        public string eventsXml = "";
        public string singersXml = "";
        public string queue1Xml = "";
        public string queue2Xml = "";
        public string queue3Xml = "";
        public string queue4Xml = "";
        public string queue5Xml = "";
        public string queue6Xml = "";

        public List<TSCQueue> NextUp = null;

        public ControlTag()
        {
        }

        public void SetNext()
        {
            NextUp = new List<TSCQueue>();
            while (true)
            {
                if (queue1Xml != null && queue1Xml.Trim().Length > 0)
                {
                    LoadNext(queue1Xml);
                    if (NextUp.Count >= 2)
                        break;
                }
                if (queue2Xml != null && queue2Xml.Trim().Length > 0)
                {
                    LoadNext(queue2Xml);
                    if (NextUp.Count >= 2)
                        break;
                }
                if (queue3Xml != null && queue3Xml.Trim().Length > 0)
                {
                    LoadNext(queue3Xml);
                    if (NextUp.Count >= 2)
                        break;
                }
                if (queue4Xml != null && queue4Xml.Trim().Length > 0)
                {
                    LoadNext(queue4Xml);
                    if (NextUp.Count >= 2)
                        break;
                }
                if (queue5Xml != null && queue5Xml.Trim().Length > 0)
                {
                    LoadNext(queue5Xml);
                    if (NextUp.Count >= 2)
                        break;
                }
                if (queue6Xml != null && queue6Xml.Trim().Length > 0)
                {
                    LoadNext(queue6Xml);
                    if (NextUp.Count >= 2)
                        break;
                }
                break;
            }
        }

        public void RefreshQueue(BindingList<TSCQueue> queue)
        {
            if (queueControl1 != null)
                queueControl1.RefreshQueue(queue);
            if (queueControl2 != null)
                queueControl2.RefreshQueue(queue);
            if (queueControl3 != null)
                queueControl3.RefreshQueue(queue);
            if (queueControl4 != null)
                queueControl4.RefreshQueue(queue);
            if (queueControl5 != null)
                queueControl5.RefreshQueue(queue);
            if (queueControl6 != null)
                queueControl6.RefreshQueue(queue);
            SetNext();
        }

        private void LoadNext(string xml)
        {
            try
            {
                if (xml != null && xml.Trim().Length > 0)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNodeList nodelist = doc.SelectNodes("/Root/Data");
                    if (nodelist != null)
                    {
                        List<TSCQueue> tscqueues = new List<TSCQueue>();
                        foreach (XmlNode node in nodelist)
                        {
                            TSCQueue tscqueue = new TSCQueue();
                            tscqueue.EventKey = Utility.GetXmlString(node, "EventKey");
                            tscqueue.SingerKey = Utility.GetXmlString(node, "SingerKey");
                            tscqueue.QueueRound = Utility.GetXmlInteger(node, "QueueRound");
                            tscqueue.QueueOrder = Utility.GetXmlInteger(node, "QueueOrder");
                            tscqueue.QueueSong = Utility.GetXmlString(node, "QueueSong");
                            tscqueue.QueueArtist = Utility.GetXmlString(node, "QueueArtist");
                            tscqueue.QueueNote = Utility.GetXmlString(node, "QueueNote");
                            tscqueue.QueueLink = Utility.GetXmlString(node, "QueueLink");
                            tscqueue.QueueState = Utility.GetXmlString(node, "QueueState");
                            tscqueues.Add(tscqueue);
                        }
                        tscqueues.Sort();
                        foreach (TSCQueue tscqueue in tscqueues)
                        {
                            if (tscqueue.QueueState != null && (tscqueue.QueueState.Trim().Length == 0 || tscqueue.QueueState.Trim().ToLower() == "pending"))
                            {
                                if (NextUp.Count < 2)
                                    NextUp.Add(tscqueue);
                                else
                                    break;
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
    */

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
        public string SqlInsert
        {
            get
            {
                string fmt = "Insert TSCQueue (";
                fmt += "EventKey,";
                fmt += "SingerKey,";
                fmt += "QueueRound,";
                fmt += "QueueOrder,";
                fmt += "QueueSong,";
                fmt += "QueueArtist,";
                fmt += "QueueNote,";
                fmt += "QueueLink,";
                fmt += "QueueState";
                fmt += ") Values (";
                fmt += "{0},{1},{2},{3},{4},{5},{6},{7},{8}";
                fmt += ")";
                return string.Format(fmt,
                    Utility.QuotedSQLString(EventKey),
                    Utility.QuotedSQLString(SingerKey),
                    QueueRound.ToString(),
                    QueueOrder.ToString(),
                    Utility.QuotedSQLString(QueueSong),
                    Utility.QuotedSQLString(QueueArtist),
                    Utility.QuotedSQLString(QueueNote),
                    Utility.QuotedSQLString(QueueLink),
                    Utility.QuotedSQLString(QueueState)
                );
            }
        }
        public string SqlDelete
        {
            get
            {
                string fmt = "Delete TSCQueue Where ";
                fmt += "EventKey = {0} and QueueRound = {1} and SingerKey = {2}";
                return string.Format(fmt,
                    Utility.QuotedSQLString(EventKey),
                    QueueRound.ToString(),
                    Utility.QuotedSQLString(SingerKey)
                );
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

        private static bool FieldsEqual(TSCQueue a, TSCQueue b)
        {
            return (
                (a.EventKey == b.EventKey) &&
                (a.SingerKey == b.SingerKey) &&
                (a.QueueRound == b.QueueRound) &&
                (a.QueueOrder == b.QueueOrder) &&
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


    public class TSCEvents : INotifyPropertyChanged
    {
        public char state = '?';

        private ColumnClass _eventKey = new ColumnClass("EventKey", ColumnType._string);
        private ColumnClass _eventName = new ColumnClass("EventName", ColumnType._string);
        private ColumnClass _venueKey = new ColumnClass("VenueKey", ColumnType._string);
        private ColumnClass _eventDate = new ColumnClass("EventDate", ColumnType._DateTime);
        private ColumnClass _eventEmail = new ColumnClass("EventEmail", ColumnType._string);
        private ColumnClass _eventAddress = new ColumnClass("EventAddress", ColumnType._string);

        public string EventKey { get { return _eventKey.Value_string; } set { _eventKey.Value_string = value; NotifyPropertyChanged(); } }
        public string EventName { get { return _eventName.Value_string; } set { _eventName.Value_string = value; NotifyPropertyChanged(); } }
        public string VenueKey { get { return _venueKey.Value_string; } set { _venueKey.Value_string = value; NotifyPropertyChanged(); } }
        public DateTime EventDate { get { return _eventDate.Value_DateTime; } set { _eventDate.Value_DateTime = value; NotifyPropertyChanged(); } }
        public string EventEmail { get { return _eventEmail.Value_string; } set { _eventEmail.Value_string = value; NotifyPropertyChanged(); } }
        public string EventAddress { get { return _eventAddress.Value_string; } set { _eventAddress.Value_string = value; NotifyPropertyChanged(); } }
        public string SqlInsert 
        { 
            get 
            {
                string fmt = "Insert TSCEvents (";
                fmt += "EventKey,";
                fmt += "EventName,";
                fmt += "VenueKey,";
                fmt += "EventDate,";
                fmt += "EventEmail,";
                fmt += "EventAddress";
                fmt += ") Values (";
                fmt += "{0},{1},{2},{3},{4},{5}";
                fmt += ")";
                return string.Format(fmt,
                    Utility.QuotedSQLString(EventKey),
                    Utility.QuotedSQLString(EventName),
                    Utility.QuotedSQLString(VenueKey),
                    Utility.QuotedSQLDate(EventDate),
                    Utility.QuotedSQLString(EventEmail),
                    Utility.QuotedSQLString(EventAddress)
                );
            }
        }
        public string SqlDelete
        {
            get
            {
                string fmt = "Delete TSCEvents Where ";
                fmt += "EventKey = {0}";
                return string.Format(fmt,
                    Utility.QuotedSQLString(EventKey)
                );
            }
        }

        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public Dictionary<string, ColumnClass> Columns = new Dictionary<string, ColumnClass>();
        public Dictionary<string, ColumnClass> Primary = new Dictionary<string, ColumnClass>();

        public event PropertyChangedEventHandler PropertyChanged;

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

        private static bool FieldsEqual(TSCEvents a, TSCEvents b)
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

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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

        private static bool FieldsEqual(TSCVenues a, TSCVenues b)
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

    public class TSCSongListItem
    {
        private string _Title;
        private string _Artist;
        private string _Disk;
        private string _IsHelper;
        private string _IsDuet;
        private string _DuetArtist;
        private string _OneDriveMP4;
        private string _OneDriveZip;
        private string _FilePath;

        public string Title { get { return _Title; } set { _Title = value; } }
        public string Artist { get { return _Artist; } set { _Artist = value; } }
        public string Disk { get { return _Disk; } set { _Disk = value; } }
        public string IsHelper { get { return _IsHelper; } set { _IsHelper = value; } }
        public string IsDuet { get { return _IsDuet; } set { _IsDuet = value; } }
        public string DuetArtist { get { return _DuetArtist; } set { _DuetArtist = value; } }
        public string OneDriveMP4 { get { return _OneDriveMP4; } set { _OneDriveMP4 = value; } }
        public string OneDriveZip { get { return _OneDriveZip; } set { _OneDriveZip = value; } }
        public string FilePath { get { return _FilePath; } set { _FilePath = value; } }

        public TSCSongListItem() : this("", "", "", "", "", "", "", "", "") { }
        public TSCSongListItem(string title, string artist, string disk, string ishelper, string isduet, string duetartist, string onedrivemp4, string onedrivezip, string filepath)
        {
            _Title = title;
            _Artist = artist;
            _Disk = disk;
            _IsHelper = ishelper;
            _IsDuet = isduet;
            _DuetArtist = duetartist;
            _OneDriveMP4 = onedrivemp4;
            _OneDriveZip = onedrivezip;
            _FilePath = filepath;
        }
        public static TSCSongListItem GetXml(XmlNode node)
        {
            if (node != null)
            {
                string title = Utility.GetXmlString(node, "Title");
                string artist = Utility.GetXmlString(node, "Artist");
                string disk = Utility.GetXmlString(node, "Disk");
                if (title.Length > 0)
                {
                    if (artist.Length == 0)
                        artist = "Unknown";
                    if (disk.Length == 0)
                        disk = "None";
                    return new TSCSongListItem(title, artist, disk,
                        Utility.GetXmlString(node, "IsHelper"),
                        Utility.GetXmlString(node, "IsDuet"),
                        Utility.GetXmlString(node, "DuetArtist"),
                        Utility.GetXmlString(node, "OneDriveMP4"),
                        Utility.GetXmlString(node, "OneDriveZip"),
                        Utility.GetXmlString(node, "FilePath"));
                }
            }
            return null;
        }
    }

}
