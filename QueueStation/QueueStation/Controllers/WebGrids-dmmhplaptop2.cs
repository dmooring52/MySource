using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Linq;
using System.Xml;
using System.Web;

using QueueStation.Models;

namespace QueueStation.Controllers
{
    public static class util
    {
        public static string textstyle(string target, string targetreference, bool islink = false)
        {
            string r = target;
            if (islink == true)
            {
                if (target.Contains('\\') == true)
                    r = ".F.";
                else if (target.Contains('/') == true)
                    r = ".U.";
                else
                    r = "...";
            }
            if (target == targetreference)
                return string.Format("<text>{0}</text>", r);
            else
                return string.Format("<text style='color:blue'>{0}</text>", r);
        }
        public static string datestyle(DateTime target, DateTime targetreference)
        {
            if (target == targetreference)
                return string.Format("<text>{0}</text>", target.ToShortDateString());
            else
                return string.Format("<text style='color:blue'>{0}</text>", target.ToShortDateString());
        }
    }
    public class QueueGrid
    {
        private int _QueueRound = 0;
        private WebGrid _grid = null;
        private List<MVCQueue> _queues = new List<MVCQueue>();
        private List<MVCQueue> _queuesref = new List<MVCQueue>();
        public int QueueRound { get { return _QueueRound; } set { _QueueRound = value; } }
        public WebGrid grid { get { return _grid; } set { _grid = value; } }
        public List<MVCQueue> queues { get { return _queues; } set { _queues = value; } }
        public List<MVCQueue> queuesref { get { return _queuesref; } set { _queuesref = value; } }

        public QueueGrid(int roundx) { _QueueRound = roundx; }

        public string GetLink(string s)
        {
            if (s.Contains('\\') == true)
                return "file";
            else if (s.Contains('/') == true)
                return "url";
            else
                return "empty";
        }
        public string QueueData(string singerkey, string column, string data)
        {
            MVCQueue target = null;
            MVCQueue targetreference = null;
            foreach (MVCQueue q in queues)
            {
                if (q.SingerKey == singerkey)
                {
                    target = q;
                    break;
                }
            }
            foreach (MVCQueue q in queuesref)
            {
                if (q.SingerKey == singerkey)
                {
                    targetreference = q;
                    break;
                }
            }
            if (target != null)
            {
                switch (column)
                {
                    case "QueueState":
                        return statestyle(target.QueueState, (targetreference == null) ? null : targetreference.QueueState);
                    case "QueueSong":
                        return util.textstyle(target.QueueSong, (targetreference == null) ? null : targetreference.QueueSong);
                    case "QueueArtist":
                        return util.textstyle(target.QueueArtist, (targetreference == null) ? null : targetreference.QueueArtist);
                    case "QueueNote":
                        return util.textstyle(target.QueueNote, (targetreference == null) ? null : targetreference.QueueNote);
                    case "QueueLink":
                        return util.textstyle(target.QueueLink, (targetreference == null) ? null : targetreference.QueueLink, true);
                    default:
                        return string.Format("<text>{0}</text>", data);
                }
            }
            else
                return string.Format("<text>{0}</text>", data);
        }

        private string statestyle(string target, string targetreference)
        {
            string sstyle = "img";
            if (target != targetreference)
                sstyle = "img-blue";
            switch (target.ToLower())
            {
                case "finished":
                    return string.Format("<text><img class=\"{0}\" src=\"/Images/State_Finished.png\" alt=\"Image\"/></text>", sstyle);
                case "gone home":
                    return string.Format("<text><img class=\"{0}\" src=\"/Images/State_GoneHome.png\" alt=\"Image\"/></text>", sstyle);
                case "not here":
                    return string.Format("<text><img class=\"{0}\" src=\"/Images/State_NotHereYet.png\" alt=\"Image\"/></text>", sstyle);
                default:
                    return string.Format("<text><img class=\"{0}\" src=\"/Images/State_Pending.png\" alt=\"Image\"/></text>", sstyle);
            }
        }

        public QueueGrid()
        {
        }

        public void Bind()
        {
            _grid = new WebGrid(source: queues, rowsPerPage: 15);
        }

        public string Save()
        {
            string result = "";
            int recordschanged = 0;
            SingingClubClient client = new SingingClubClient();
            client.Open();
            if (queues != null && queuesref != null)
            {
                List<MVCQueue> tobeadded = new List<MVCQueue>();
                foreach (MVCQueue q in queues)
                {
                    bool found = false;
                    foreach (MVCQueue qref in queuesref)
                    {
                        if (q.KeyEquals(qref) == true)
                        {
                            found = true;
                            if (MVCQueue.FieldsEqual(q, qref) == false)
                            {
                                result = client.GeneralStore("TSCQueue", "UPDATE", q.GetDataXml());
                                if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                                {
                                    recordschanged++;
                                }
                            }
                        }
                    }
                    if (found == false)
                    {
                        tobeadded.Add(q);
                    }
                }
                if (tobeadded.Count > 0)
                {
                    foreach (MVCQueue tba in tobeadded)
                    {
                        int thisround = tba.QueueRound;
                        string thisstate = tba.QueueState;
                        for (int i = 1; i <= Utility.NumberOfRounds; i++)
                        {
                            tba.QueueRound = i;
                            if (i < thisround)
                                tba.QueueState = "Not Here";
                            else if (i > thisround)
                                tba.QueueState = "Pending";
                            else
                                tba.QueueState = thisstate;
                            result = client.GeneralStore("TSCQueue", "INSERT", tba.GetDataXml());
                            if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                            {
                                recordschanged++;
                            }
                        }
                    }
                }
            }
            client.Close();
            return recordschanged.ToString();
        }
    }
    public class EventsGrid
    {
        private string _command = "";
        private WebGrid _grid = null;
        private List<MVCEvents> _events = new List<MVCEvents>();
        private List<MVCEvents> _eventsref = new List<MVCEvents>();
        private List<string> _VenueList = new List<string>();
        public string command { get { return _command; } set { _command = value; } }
        public WebGrid grid { get { return _grid; } set { _grid = value; } }
        public List<MVCEvents> events { get { return _events; } set { _events = value; } }
        public List<MVCEvents> eventsref { get { return _eventsref; } set { _eventsref = value; } }
        public List<string> VenueList { get { return _VenueList; } set { _VenueList = value; } }
        
        public string EventData(string eventkey, string column, string data)
        {
            MVCEvents target = null;
            MVCEvents targetreference = null;
            foreach (MVCEvents q in events)
            {
                if (q.EventKey == eventkey)
                {
                    target = q;
                    break;
                }
            }
            foreach (MVCEvents q in eventsref)
            {
                if (q.EventKey == eventkey)
                {
                    targetreference = q;
                    break;
                }
            }
            if (target != null)
            {
                switch (column)
                {
                    case "EventName":
                        return util.textstyle(target.EventName, (targetreference == null) ? null : targetreference.EventName);
                    case "EventDate":
                        return util.datestyle(target.EventDate, (targetreference == null) ? DateTime.MinValue : targetreference.EventDate);
                    case "EventAddress":
                        return util.textstyle(target.EventAddress, (targetreference == null) ? null : targetreference.EventAddress);
                    case "EventEmail":
                        return util.textstyle(target.EventEmail, (targetreference == null) ? null : targetreference.EventEmail);
                    default:
                        return string.Format("<text>{0}</text>", data);
                }
            }
            else
                return string.Format("<text>{0}</text>", data);
        }
        public string EventData(string eventkey, string column, DateTime data)
        {
            MVCEvents target = null;
            MVCEvents targetreference = null;
            foreach (MVCEvents q in events)
            {
                if (q.EventKey == eventkey)
                {
                    target = q;
                    break;
                }
            }
            foreach (MVCEvents q in eventsref)
            {
                if (q.EventKey == eventkey)
                {
                    targetreference = q;
                    break;
                }
            }
            if (target != null)
            {
                return util.datestyle(target.EventDate, (targetreference == null) ? DateTime.MinValue : targetreference.EventDate);
            }
            else
                return string.Format("<text>{0}</text>", data);
        }
        public EventsGrid()
        {
        }

        public void Bind()
        {
            _grid = new WebGrid(source: events, rowsPerPage: 15);
        }

        public static void Revert()
        {
            XmlDocument doct = SessionBag.Current.EventsXml as XmlDocument;
            XmlDocument docr = SessionBag.Current.EventsXmlReference as XmlDocument;
            if (doct != null && docr != null)
            {
                doct.LoadXml(docr.OuterXml);
            }
        }
        
        public string Save()
        {
            string result = "";
            int recordschanged = 0;
            SingingClubClient client = new SingingClubClient();
            client.Open();
            if (events != null && eventsref != null)
            {
                List<MVCEvents> tobeadded = new List<MVCEvents>();
                foreach (MVCEvents q in events)
                {
                    bool found = false;
                    foreach (MVCEvents qref in eventsref)
                    {
                        if (q.KeyEquals(qref) == true)
                        {
                            found = true;
                            if (MVCEvents.FieldsEqual(q, qref) == false)
                            {
                                result = client.GeneralStore("TSCEvents", "UPDATE", q.GetDataXml());
                                if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                                {
                                    recordschanged++;
                                }
                            }
                        }
                    }
                    if (found == false)
                    {
                        tobeadded.Add(q);
                    }
                }
                if (tobeadded.Count > 0)
                {
                    foreach (MVCEvents tba in tobeadded)
                    {
                        result = client.GeneralStore("TSCEvents", "INSERT", tba.GetDataXml());
                        if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                        {
                            recordschanged++;
                        }
                    }
                }
            }
            client.Close();
            return recordschanged.ToString();
        }
    }
    public class SongsGrid
    {
        private string _command = "";
        private WebGrid _grid = null;
        private List<MVCSongs> _songs = new List<MVCSongs>();
        private List<MVCSongs> _songsref = new List<MVCSongs>();
        public string command { get { return _command; } set { _command = value; } }
        public WebGrid grid { get { return _grid; } set { _grid = value; } }
        public List<MVCSongs> songs { get { return _songs; } set { _songs = value; } }
        public List<MVCSongs> songsref { get { return _songsref; } set { _songsref = value; } }

        public string SongData(string songkey, string column, string data)
        {
            MVCSongs target = null;
            MVCSongs targetreference = null;
            foreach (MVCSongs q in songs)
            {
                if (q.SongKey == songkey)
                {
                    target = q;
                    break;
                }
            }
            foreach (MVCSongs q in songsref)
            {
                if (q.SongKey == songkey)
                {
                    targetreference = q;
                    break;
                }
            }
            if (target != null)
            {
                switch (column)
                {
                    case "Title":
                        return util.textstyle(target.Title, (targetreference == null) ? null : targetreference.Title);
                    case "Artist":
                        return util.textstyle(target.Artist, (targetreference == null) ? null : targetreference.Artist);
                    case "Disk":
                        return util.textstyle(target.Disk, (targetreference == null) ? null : targetreference.Disk);
                    default:
                        return string.Format("<text>{0}</text>", data);
                }
            }
            else
                return string.Format("<text>{0}</text>", data);
        }

        public string SongData(string songkey, string column, DateTime data)
        {
            MVCSongs target = null;
            MVCSongs targetreference = null;
            foreach (MVCSongs q in songs)
            {
                if (q.SongKey == songkey)
                {
                    target = q;
                    break;
                }
            }
            foreach (MVCSongs q in songsref)
            {
                if (q.SongKey == songkey)
                {
                    targetreference = q;
                    break;
                }
            }
            //if (target != null)
            //{
            //   return datestyle(target.EventDate, (targetreference == null) ? DateTime.MinValue : targetreference.EventDate);
            //}
            //else
            return string.Format("<text>{0}</text>", data);
        }

        public SongsGrid()
        {
        }

        public void Bind()
        {
            _grid = new WebGrid(source: songs, rowsPerPage: 50);
        }

        public static void Revert()
        {
            XmlDocument doct = SessionBag.Current.SongsXml as XmlDocument;
            XmlDocument docr = SessionBag.Current.SongsXmlReference as XmlDocument;
            if (doct != null && docr != null)
            {
                doct.LoadXml(docr.OuterXml);
            }
        }

        public string Save()
        {
            string result = "";
            int recordschanged = 0;
            SingingClubClient client = new SingingClubClient();
            client.Open();
            if (songs != null && songsref != null)
            {
                List<MVCSongs> tobeadded = new List<MVCSongs>();
                foreach (MVCSongs q in songs)
                {
                    bool found = false;
                    foreach (MVCSongs qref in songsref)
                    {
                        if (q.KeyEquals(qref) == true)
                        {
                            found = true;
                            if (MVCSongs.FieldsEqual(q, qref) == false)
                            {
                                result = client.GeneralStore("TSCSongList_Main", "UPDATE", q.GetDataXml());
                                if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                                {
                                    recordschanged++;
                                }
                            }
                        }
                    }
                    if (found == false)
                    {
                        tobeadded.Add(q);
                    }
                }
                if (tobeadded.Count > 0)
                {
                    foreach (MVCSongs tba in tobeadded)
                    {
                        result = client.GeneralStore("TSCSongList_Main", "INSERT", tba.GetDataXml());
                        if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                        {
                            recordschanged++;
                        }
                    }
                }
            }
            client.Close();
            return recordschanged.ToString();
        }
    }
    public class SingersGrid
    {
        private string _command = "";
        private WebGrid _grid = null;
        private List<MVCSingers> _singers = new List<MVCSingers>();
        private List<MVCSingers> _singersref = new List<MVCSingers>();
        public string command { get { return _command; } set { _command = value; } }
        public WebGrid grid { get { return _grid; } set { _grid = value; } }
        public List<MVCSingers> singers { get { return _singers; } set { _singers = value; } }
        public List<MVCSingers> singersref { get { return _singersref; } set { _singersref = value; } }

        public string SingerData(string singerkey, string column, string data)
        {
            MVCSingers target = null;
            MVCSingers targetreference = null;
            foreach (MVCSingers q in singers)
            {
                if (q.SingerKey == singerkey)
                {
                    target = q;
                    break;
                }
            }
            foreach (MVCSingers q in singersref)
            {
                if (q.SingerKey == singerkey)
                {
                    targetreference = q;
                    break;
                }
            }
            if (target != null)
            {
                switch (column)
                {
                    case "SingerName":
                        return util.textstyle(target.SingerName, (targetreference == null) ? null : targetreference.SingerName);
                    case "SingerEmail":
                        return util.textstyle(target.SingerEmail, (targetreference == null) ? null : targetreference.SingerEmail);
                    default:
                        return string.Format("<text>{0}</text>", data);
                }
            }
            else
                return string.Format("<text>{0}</text>", data);
        }
 
        public string SingerData(string singerkey, string column, DateTime data)
        {
            MVCSingers target = null;
            MVCSingers targetreference = null;
            foreach (MVCSingers q in singers)
            {
                if (q.SingerKey == singerkey)
                {
                    target = q;
                    break;
                }
            }
            foreach (MVCSingers q in singersref)
            {
                if (q.SingerKey == singerkey)
                {
                    targetreference = q;
                    break;
                }
            }
            //if (target != null)
            //{
             //   return datestyle(target.EventDate, (targetreference == null) ? DateTime.MinValue : targetreference.EventDate);
            //}
            //else
                return string.Format("<text>{0}</text>", data);
        }
  
        public SingersGrid()
        {
        }

        public void Bind()
        {
            _grid = new WebGrid(source: singers, rowsPerPage: 15);
        }

        public static void Revert()
        {
            XmlDocument doct = SessionBag.Current.SingersXml as XmlDocument;
            XmlDocument docr = SessionBag.Current.SingersXmlReference as XmlDocument;
            if (doct != null && docr != null)
            {
                doct.LoadXml(docr.OuterXml);
            }
        }

        public string Save()
        {
            string result = "";
            int recordschanged = 0;
            SingingClubClient client = new SingingClubClient();
            client.Open();
            if (singers != null && singersref != null)
            {
                List<MVCSingers> tobeadded = new List<MVCSingers>();
                foreach (MVCSingers q in singers)
                {
                    bool found = false;
                    foreach (MVCSingers qref in singersref)
                    {
                        if (q.KeyEquals(qref) == true)
                        {
                            found = true;
                            if (MVCSingers.FieldsEqual(q, qref) == false)
                            {
                                result = client.GeneralStore("TSCSingers", "UPDATE", q.GetDataXml());
                                if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                                {
                                    recordschanged++;
                                }
                            }
                        }
                    }
                    if (found == false)
                    {
                        tobeadded.Add(q);
                    }
                }
                if (tobeadded.Count > 0)
                {
                    foreach (MVCSingers tba in tobeadded)
                    {
                        result = client.GeneralStore("TSCSingers", "INSERT", tba.GetDataXml());
                        if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                        {
                            recordschanged++;
                        }
                    }
                }
            }
            client.Close();
            return recordschanged.ToString();
        }
    }
    public class VenuesGrid
    {
        private string _command = "";
        private WebGrid _grid = null;
        private List<MVCVenues> _venues = new List<MVCVenues>();
        private List<MVCVenues> _venuesref = new List<MVCVenues>();
        public string command { get { return _command; } set { _command = value; } }
        public WebGrid grid { get { return _grid; } set { _grid = value; } }
        public List<MVCVenues> venues { get { return _venues; } set { _venues = value; } }
        public List<MVCVenues> venuesref { get { return _venuesref; } set { _venuesref = value; } }

        public string VenueData(string venuekey, string column, string data)
        {
            MVCVenues target = null;
            MVCVenues targetreference = null;
            foreach (MVCVenues q in venues)
            {
                if (q.VenueKey == venuekey)
                {
                    target = q;
                    break;
                }
            }
            foreach (MVCVenues q in venuesref)
            {
                if (q.VenueKey == venuekey)
                {
                    targetreference = q;
                    break;
                }
            }
            if (target != null)
            {
                switch (column)
                {
                    case "VenueName":
                        return util.textstyle(target.VenueName, (targetreference == null) ? null : targetreference.VenueName);
                    case "VenueEmail":
                        return util.textstyle(target.VenueEmail, (targetreference == null) ? null : targetreference.VenueEmail);
                    case "VenueAddress":
                        return util.textstyle(target.VenueAddress, (targetreference == null) ? null : targetreference.VenueAddress);
                    case "VenueContact":
                        return util.textstyle(target.VenueContact, (targetreference == null) ? null : targetreference.VenueContact);
                    case "VenuePhone":
                        return util.textstyle(target.VenuePhone, (targetreference == null) ? null : targetreference.VenuePhone);
                    default:
                        return string.Format("<text>{0}</text>", data);
                }
            }
            else
                return string.Format("<text>{0}</text>", data);
        }

        public string VenueData(string venuekey, string column, DateTime data)
        {
            MVCVenues target = null;
            MVCVenues targetreference = null;
            foreach (MVCVenues q in venues)
            {
                if (q.VenueKey == venuekey)
                {
                    target = q;
                    break;
                }
            }
            foreach (MVCVenues q in venuesref)
            {
                if (q.VenueKey == venuekey)
                {
                    targetreference = q;
                    break;
                }
            }
            //if (target != null)
            //{
            //   return datestyle(target.EventDate, (targetreference == null) ? DateTime.MinValue : targetreference.EventDate);
            //}
            //else
            return string.Format("<text>{0}</text>", data);
        }

        public VenuesGrid()
        {
        }

        public void Bind()
        {
            _grid = new WebGrid(source: venues, rowsPerPage: 15);
        }

        public static void Revert()
        {
            XmlDocument doct = SessionBag.Current.VenuesXml as XmlDocument;
            XmlDocument docr = SessionBag.Current.VenuesXmlReference as XmlDocument;
            if (doct != null && docr != null)
            {
                doct.LoadXml(docr.OuterXml);
            }
        }

        public string Save()
        {
            string result = "";
            int recordschanged = 0;
            SingingClubClient client = new SingingClubClient();
            client.Open();
            if (venues != null && venuesref != null)
            {
                List<MVCVenues> tobeadded = new List<MVCVenues>();
                foreach (MVCVenues q in venues)
                {
                    bool found = false;
                    foreach (MVCVenues qref in venuesref)
                    {
                        if (q.KeyEquals(qref) == true)
                        {
                            found = true;
                            if (MVCVenues.FieldsEqual(q, qref) == false)
                            {
                                result = client.GeneralStore("TSCVenues", "UPDATE", q.GetDataXml());
                                if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                                {
                                    recordschanged++;
                                }
                            }
                        }
                    }
                    if (found == false)
                    {
                        tobeadded.Add(q);
                    }
                }
                if (tobeadded.Count > 0)
                {
                    foreach (MVCVenues tba in tobeadded)
                    {
                        result = client.GeneralStore("TSCVenues", "INSERT", tba.GetDataXml());
                        if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                        {
                            recordschanged++;
                        }
                    }
                }
            }
            client.Close();
            return recordschanged.ToString();
        }
    }
}