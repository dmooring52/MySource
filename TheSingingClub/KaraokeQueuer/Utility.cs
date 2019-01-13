﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

using XmlUtility;

namespace KaraokeQueuer
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
		public DateTime Value_DateTime { get { return _value_datetime; } set { if (_ctype == ColumnType._DateTime) _value_datetime = value;} }
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

		public ColumnClass(string name, ColumnType ctype, string value) : this(name, ctype)
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

		public XmlDocument venuesXml = new XmlDocument();
		public XmlDocument eventsXml = new XmlDocument();
		public XmlDocument singersXml = new XmlDocument();
		public XmlDocument queueXml = new XmlDocument();

		public List<TSCQueue> NextUp = null;

		public ControlTag()
		{
		}

		public void SetNext()
		{
			NextUp = new List<TSCQueue>();
			while (true && queueXml != null)
			{
					LoadNext(queueXml, 1);
					if (NextUp.Count >= 2)
						break;
					LoadNext(queueXml, 2);
					if (NextUp.Count >= 2)
						break;
					LoadNext(queueXml, 3);
					if (NextUp.Count >= 2)
						break;
					LoadNext(queueXml, 4);
					if (NextUp.Count >= 2)
						break;
					LoadNext(queueXml, 5);
					if (NextUp.Count >= 2)
						break;
					LoadNext(queueXml, 6);
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

		private void LoadNext(XmlDocument doc, int iround)
		{
			try
			{
				if (doc != null)
				{
					XmlNode nodedata = doc.SelectSingleNode("/Root/Data");
					if (nodedata != null)
					{
						XmlNode noderound = nodedata.SelectSingleNode("QueueRound[@round='" + iround.ToString() + "']");
						string eventkey = Utility.GetXmlString(nodedata, "EventKey");
						XmlNodeList nodesingers = noderound.SelectNodes("SingerKey");
						List<TSCQueue> tscqueues = new List<TSCQueue>();
						if (nodesingers != null)
						{
							foreach (XmlNode singer in nodesingers)
							{
								TSCQueue tscqueue = new TSCQueue();
								tscqueue.EventKey = eventkey;
								tscqueue.QueueRound = iround;
								tscqueue.SingerKey = Utility.GetXmlString(singer, "key", true);
								tscqueue.QueueOrder = Utility.GetXmlInteger(singer, "QueueOrder");
								tscqueue.QueueSong = Utility.GetXmlString(singer, "QueueSong");
								tscqueue.QueueArtist = Utility.GetXmlString(singer, "QueueArtist");
								tscqueue.QueueNote = Utility.GetXmlString(singer, "QueueNote");
								tscqueue.QueueLink = Utility.GetXmlString(singer, "QueueLink");
								tscqueue.QueueState = Utility.GetXmlString(singer, "QueueState");
								tscqueues.Add(tscqueue);
							}
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
		public SingerHistoryStore(string singerName) : this()
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
			sb.AppendLine("		<XPath>");
			sb.AppendLine("			<Key>/Root/Data/EventKey</Key>");
			sb.AppendLine("			<Root>/Root</Root>");
			sb.AppendLine("			<RootNode>Data</RootNode>");
			sb.AppendLine("		</XPath>");
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
}