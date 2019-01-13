using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using QueueStation.Models;

namespace SingingClubSync
{
    public enum TableName
    {
        TSCVenues,
        TSCEvents,
        TSCSingers,
        TSCQueue
    }
    public enum TableAction
    {
        GET,
        UPDATE,
        INSERT,
        DELETE
    }

    public partial class SyncForm : Form
    {
        private StringBuilder _sb = new StringBuilder();
        private string _eventsxml = "";

        public SyncForm()
        {
            InitializeComponent();
        }

        public string TSCTable(TableName table)
        {
            return table.ToString();
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            HideButtons();
            try
            {
                MvcList localVenues = null;
                MvcList localEvents = null;
                MvcList localSingers = null;
                MvcList localQueues = null;
                MvcList remoteVenus = null;
                MvcList remoteEvents = null;
                MvcList remoteSingers = null;
                MvcList remoteQueues = null;
                SingingClub scLocal = new SingingClub("Server=dmmlenovo1;Database=TheSingingClub;Integrated Security=true");
                SingingClub scRemote = new SingingClub("Data Source=SQL5012.Smarterasp.net;Initial Catalog=DB_9BA515_LocalSingingClub;User Id=DB_9BA515_LocalSingingClub_admin;Password=Groovy52!;");
                localVenues = GetVenues(scLocal);
                localEvents = GetEvents(scLocal);
                localSingers = GetSingers(scLocal);

                /*
                string eventKey = GetLatestEventKey();
                if (eventKey.Trim().Length > 0)
                {
                    TSCQueue q = new TSCQueue();
                    q.EventKey = eventKey;
                    q.QueueRound = 1;
                    string r1xml = scRemote.GeneralStore(TableName.TSCQueue, TableAction.GET, q.GetDataXml());
                }
                */

                localQueues = GetQueues(scLocal);

                remoteVenus = GetVenues(scRemote);
                remoteEvents = GetEvents(scRemote);
                remoteSingers = GetSingers(scRemote);
                remoteQueues = GetQueues(scRemote);

                SyncVenues(localVenues, remoteVenus, scRemote);
                SyncEvents(localEvents, remoteEvents, scRemote);
                SyncSingers(localSingers, remoteSingers, scRemote);
                SyncQueues(localQueues, remoteQueues, scRemote);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception occurred");
            }
            finally
            {
                UnHideButtons();
            }
        }
        private void btnSongList_Click(object sender, EventArgs e)
        {
            HideButtons();
            try
            { 
                SingingClub scRemote = new SingingClub("Data Source=SQL5012.Smarterasp.net;Initial Catalog=DB_9BA515_LocalSingingClub;User Id=DB_9BA515_LocalSingingClub_admin;Password=Groovy52!;");
                SingingClub scLocal = new SingingClub("Server=dmmlenovo1;Database=TheSingingClub;Integrated Security=true");
                StreamReader sr = File.OpenText(@"C:\Users\dmooring\OneDrive\Karaoke\CDG_MP3\Index\Songlist.txt");
                List<string> errs = new List<string>();
                List<TSCSongListItem> songs = new List<TSCSongListItem>();
                List<TSCSongListItem> dupes = new List<TSCSongListItem>();
                //List<TSCSongListItem> dbsongs = scRemote.GetSongList();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line != null && line.Trim().Length > 0 && line.Contains('\t') == true)
                    {
                        string[] fields = line.Split('\t');
                        if (fields.Length >= 9)
                        {
                            string genericpath = GenericPath(Val(fields[8]));
                            if (Val(fields[0]).Length > 0 && genericpath.Length > 0)
                            {
                                TSCSongListItem sli = new TSCSongListItem(
                                    Val(fields[0]), Val(fields[1]), Val(fields[2]),
                                    Val(fields[3]), Val(fields[4]), Val(fields[5]), Val(fields[6]), Val(fields[7]), genericpath);
                                if (sli.Artist.Length == 0)
                                    sli.Artist = "None";
                                if (sli.Disk.Length == 0)
                                    sli.Disk = "None";
                                if (HasKey(songs, sli))
                                {
                                    dupes.Add(sli);
                                }
                                else
                                {
                                    songs.Add(sli);
                                    scRemote.AddSong(sli);
                                    //scLocal.AddSong(sli);
                                }
                            }
                            else
                            {
                                errs.Add("3");
                            }
                        }
                        else
                        {
                            errs.Add("2");
                        }
                    }
                    else
                        errs.Add("1");
                }
                sr.Close();
                if (dupes.Count > 0 || errs.Count > 0)
                {
                    string s = "hi";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception occurred");
            }
            finally
            {
                UnHideButtons();
            }
        }
        private void btnArchive_Click(object sender, EventArgs e)
        {
            HideButtons();
            try
            {
                List<TSCEvents> localEvents = null;
                List<TSCQueue> localQueues = null;
                List<TSCEvents> remoteEvents = null;
                List<TSCQueue> remoteQueues = null;
                SingingClub scLocal = new SingingClub("Server=dmmlenovo1;Database=TheSingingClub;Integrated Security=true");
                SingingClub scRemote = new SingingClub("Data Source=SQL5012.Smarterasp.net;Initial Catalog=DB_9BA515_LocalSingingClub;User Id=DB_9BA515_LocalSingingClub_admin;Password=Groovy52!;");

                localEvents = GetEventList(scLocal);
                localQueues = GetQueueList(scLocal);

                remoteEvents = GetEventList(scRemote);
                remoteQueues = GetQueueList(scRemote);

                ArchiveData(localEvents, localQueues, "local");
                ArchiveData(remoteEvents, remoteQueues, "remote");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception occurred");
            }
            finally
            {
                UnHideButtons();
            }
        }
        private void ArchiveData(List<TSCEvents> Events, List<TSCQueue> Queues, string name)
        {
            var events = Events.Where(n => n.EventDate < DateTime.Now - TimeSpan.FromDays(365.00));
            var queues = Queues.Where(n => events.Count(m => m.EventKey == n.EventKey) > 0);
            queues.OrderBy(n => n.EventKey);
            events.OrderBy(n => n.EventKey);
            StringBuilder iev = new StringBuilder();
            StringBuilder iqu = new StringBuilder();
            foreach (TSCEvents ev in events)
                iev.AppendLine(ev.SqlInsert);
            foreach (TSCQueue qu in queues)
                iqu.AppendLine(qu.SqlInsert);
            StreamWriter sw = File.CreateText(string.Format(@"c:\temp\sqlinsert_{0}.txt", name));
            sw.Write(iev.ToString());
            sw.Write(iqu.ToString());
            sw.Close();

            iev = new StringBuilder();
            iqu = new StringBuilder();
            foreach (TSCQueue qu in queues)
                iqu.AppendLine(qu.SqlDelete);
            foreach (TSCEvents ev in events)
                iev.AppendLine(ev.SqlDelete);
            sw = File.CreateText(string.Format(@"c:\temp\sqldelete_{0}.txt", name));
            sw.Write(iev.ToString());
            sw.Write(iqu.ToString());
            sw.Close();
        }
        private void HideButtons()
        {
            btnRun.Enabled = false;
            btnSongList.Enabled = false;
            btnArchive.Enabled = false;
        }
        private void UnHideButtons()
        {
            btnRun.Enabled = true;
            btnSongList.Enabled = true;
            btnArchive.Enabled = true;
        }
        private string GetLatestEventKey()
        {
            try
            {
                string xml = _eventsxml;
                if (xml != null && xml.Trim().Length > 0)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                    string eventkey = "";
                    DateTime maxdt = DateTime.MinValue;
                    foreach (XmlNode node in nodes)
                    {
                        DateTime dt = Utility.GetXmlDateTime(node, "EventDate");
                        if(dt > maxdt)
                        {
                            string evt = Utility.GetXmlString(node, "EventKey");
                            if (evt.Trim().Length > 0)
                            {
                                maxdt = dt;
                                eventkey = evt.Trim();
                            }
                        }
                    }
                    if (maxdt > DateTime.MinValue)
                        return eventkey;
                }
            }
            catch {}
            return "";
        }

        #region Venues
        private MvcList GetVenues(SingingClub sc)
        {
            MvcList list = new MvcList();
            string xml = sc.GeneralStore(TableName.TSCVenues, TableAction.GET, (new TSCVenues().GetDataXml()));
            if (xml.Trim().Length > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    TSCVenues venues = new TSCVenues(node);
                    list.Add(venues);
                }
            }
            return list;
        }
        private List<TSCVenues> GetVenueList(SingingClub sc)
        {
            List<TSCVenues> list = new List<TSCVenues>();
            string xml = sc.GeneralStore(TableName.TSCVenues, TableAction.GET, (new TSCVenues().GetDataXml()));
            if (xml.Trim().Length > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    TSCVenues venues = new TSCVenues(node);
                    list.Add(venues);
                }
            }
            return list;
        }
        private void SyncVenues(MvcList local, MvcList remote, SingingClub scRemote)
        {
            foreach (object o in local)
            {
                TSCVenues localitem = o as TSCVenues;
                if (localitem != null && localitem.VenueKey.Trim().Length > 0)
                {
                    bool found = false;
                    foreach (object oremote in remote)
                    {
                        TSCVenues remoteitem = oremote as TSCVenues;
                        if (remoteitem != null && remoteitem.VenueKey.Trim().Length > 0)
                        {
                            if (localitem.KeyEquals(remoteitem) == true)
                            {
                                if (localitem.Equals(remoteitem) == false)
                                    UpdateVenue(localitem, remoteitem, scRemote);
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found == false)
                        InsertVenue(localitem, scRemote);
                }
            }



            foreach (object o in remote)
            {
                TSCVenues remoteitem = o as TSCVenues;
                if (remoteitem != null && remoteitem.VenueKey.Trim().Length > 0)
                {
                    bool found = false;
                    foreach (object olocal in local)
                    {
                        TSCVenues localitem = olocal as TSCVenues;
                        if (localitem != null && localitem.VenueKey.Trim().Length > 0)
                        {
                            if (remoteitem.KeyEquals(localitem) == true)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found == false)
                        DeleteVenue(remoteitem, scRemote);
                }
            }

        }

        private void UpdateVenue(TSCVenues local, TSCVenues remote, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Update: {0}", local.VenueKey));
            scRemote.GeneralStore(TableName.TSCVenues, TableAction.UPDATE, local.GetDataXml());
        }

        private void InsertVenue(TSCVenues local, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Insert: {0}", local.VenueKey));
            scRemote.GeneralStore(TableName.TSCVenues, TableAction.INSERT, local.GetDataXml());
        }
 
        private void DeleteVenue(TSCVenues remote, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Delete: {0}", remote.VenueKey));
            scRemote.GeneralStore(TableName.TSCVenues, TableAction.DELETE, remote.GetDataXml());
        }
        #endregion

        #region Events
        private MvcList GetEvents(SingingClub sc)
        {
            MvcList list = new MvcList();
            string xml = _eventsxml = sc.GeneralStore(TableName.TSCEvents, TableAction.GET, (new TSCEvents().GetDataXml()));
            if (xml.Trim().Length > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    TSCEvents events = new TSCEvents(node);
                    list.Add(events);
                }
            }
            return list;
        }
        private List<TSCEvents> GetEventList(SingingClub sc)
        {
            List<TSCEvents> list = new List<TSCEvents>();
            string xml = _eventsxml = sc.GeneralStore(TableName.TSCEvents, TableAction.GET, (new TSCEvents().GetDataXml()));
            if (xml.Trim().Length > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    TSCEvents events = new TSCEvents(node);
                    list.Add(events);
                }
            }
            return list;
        }
        private void SyncEvents(MvcList local, MvcList remote, SingingClub scRemote)
        {
            foreach (object o in local)
            {
                TSCEvents localitem = o as TSCEvents;
                if (localitem != null && localitem.EventKey.Trim().Length > 0)
                {
                    bool found = false;
                    foreach (object oremote in remote)
                    {
                        TSCEvents remoteitem = oremote as TSCEvents;
                        if (remoteitem != null && remoteitem.EventKey.Trim().Length > 0)
                        {
                            if (localitem.KeyEquals(remoteitem) == true)
                            {
                                if (localitem.Equals(remoteitem) == false)
                                    UpdateEvent(localitem, remoteitem, scRemote);
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found == false)
                        InsertEvent(localitem, scRemote);
                }
            }

            foreach (object o in remote)
            {
                TSCEvents remoteitem = o as TSCEvents;
                if (remoteitem != null && remoteitem.EventKey.Trim().Length > 0)
                {
                    bool found = false;
                    foreach (object olocal in local)
                    {
                        TSCEvents localitem = olocal as TSCEvents;
                        if (localitem != null && localitem.EventKey.Trim().Length > 0)
                        {
                            if (remoteitem.KeyEquals(localitem) == true)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found == false)
                        DeleteEvent(remoteitem, scRemote);
                }
            }

        }

        private void UpdateEvent(TSCEvents local, TSCEvents remote, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Update: {0}", local.EventKey));
            scRemote.GeneralStore(TableName.TSCEvents, TableAction.UPDATE, local.GetDataXml());
        }

        private void InsertEvent(TSCEvents local, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Insert: {0}", local.EventKey));
            scRemote.GeneralStore(TableName.TSCEvents, TableAction.INSERT, local.GetDataXml());
        }

        private void DeleteEvent(TSCEvents remote, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Delete: {0}", remote.EventKey));
            scRemote.GeneralStore(TableName.TSCEvents, TableAction.DELETE, remote.GetDataXml());
        }
        #endregion

        #region Singers
        private MvcList GetSingers(SingingClub sc)
        {
            MvcList list = new MvcList();
            string xml = sc.GeneralStore(TableName.TSCSingers, TableAction.GET, (new TSCSingers().GetDataXml()));
            if (xml.Trim().Length > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    TSCSingers singers = new TSCSingers(node);
                    list.Add(singers);
                }
            }
            return list;
        }
        private List<TSCSingers> GetSingerList(SingingClub sc)
        {
            List<TSCSingers> list = new List<TSCSingers>();
            string xml = sc.GeneralStore(TableName.TSCSingers, TableAction.GET, (new TSCSingers().GetDataXml()));
            if (xml.Trim().Length > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    TSCSingers singers = new TSCSingers(node);
                    list.Add(singers);
                }
            }
            return list;
        }
        private void SyncSingers(MvcList local, MvcList remote, SingingClub scRemote)
        {
            foreach (object o in local)
            {
                TSCSingers localitem = o as TSCSingers;
                if (localitem != null && localitem.SingerKey.Trim().Length > 0)
                {
                    bool found = false;
                    foreach (object oremote in remote)
                    {
                        TSCSingers remoteitem = oremote as TSCSingers;
                        if (remoteitem != null && remoteitem.SingerKey.Trim().Length > 0)
                        {
                            if (localitem.KeyEquals(remoteitem) == true)
                            {
                                if (localitem.Equals(remoteitem) == false)
                                    UpdateSinger(localitem, remoteitem, scRemote);
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found == false)
                        InsertSinger(localitem, scRemote);
                }
            }

            foreach (object o in remote)
            {
                TSCSingers remoteitem = o as TSCSingers;
                if (remoteitem != null && remoteitem.SingerKey.Trim().Length > 0)
                {
                    bool found = false;
                    foreach (object olocal in local)
                    {
                        TSCSingers localitem = olocal as TSCSingers;
                        if (localitem != null && localitem.SingerKey.Trim().Length > 0)
                        {
                            if (remoteitem.KeyEquals(localitem) == true)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found == false)
                        DeleteSinger(remoteitem, scRemote);
                }
            }

        }

        private void UpdateSinger(TSCSingers local, TSCSingers remote, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Update: {0}", local.SingerKey));
            scRemote.GeneralStore(TableName.TSCSingers, TableAction.UPDATE, local.GetDataXml());
        }

        private void InsertSinger(TSCSingers local, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Insert: {0}", local.SingerKey));
            scRemote.GeneralStore(TableName.TSCSingers, TableAction.INSERT, local.GetDataXml());
        }

        private void DeleteSinger(TSCSingers remote, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Delete: {0}", remote.SingerKey));
            scRemote.GeneralStore(TableName.TSCSingers, TableAction.DELETE, remote.GetDataXml());
        }
        #endregion

        #region Queues
        private MvcList GetQueues(SingingClub sc)
        {
            MvcList list = new MvcList();
            TSCQueue q = new TSCQueue();
            q.QueueRound = -1;
            string xml = sc.GeneralStore(TableName.TSCQueue, TableAction.GET, (new TSCQueue().GetDataXml()));
            if (xml.Trim().Length > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    TSCQueue queues = new TSCQueue(node);
                    list.Add(queues);
                }
            }
            return list;
        }
        private List<TSCQueue> GetQueueList(SingingClub sc)
        {
            List<TSCQueue> list = new List<TSCQueue>();
            TSCQueue q = new TSCQueue();
            q.QueueRound = -1;
            string xml = sc.GeneralStore(TableName.TSCQueue, TableAction.GET, (new TSCQueue().GetDataXml()));
            if (xml.Trim().Length > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    TSCQueue queues = new TSCQueue(node);
                    list.Add(queues);
                }
            }
            return list;
        }
        private void SyncQueues(MvcList local, MvcList remote, SingingClub scRemote)
        {
            foreach (object o in local)
            {
                TSCQueue localitem = o as TSCQueue;
                if (localitem != null && localitem.EventKey.Trim().Length > 0 && localitem.SingerKey.Trim().Length > 0 && localitem.QueueRound > 0)
                {
                    bool found = false;
                    foreach (object oremote in remote)
                    {
                        TSCQueue remoteitem = oremote as TSCQueue;
                        if (remoteitem != null && remoteitem.EventKey.Trim().Length > 0 && remoteitem.SingerKey.Trim().Length > 0 && remoteitem.QueueRound > 0)
                        {
                            if (localitem.KeyEquals(remoteitem) == true)
                            {
                                if (localitem.Equals(remoteitem) == false)
                                    UpdateQueue(localitem, remoteitem, scRemote);
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found == false)
                        InsertQueue(localitem, scRemote);
                }
            }

            foreach (object o in remote)
            {
                TSCQueue remoteitem = o as TSCQueue;
                if (remoteitem != null && remoteitem.EventKey.Trim().Length > 0 && remoteitem.SingerKey.Trim().Length > 0 && remoteitem.QueueRound > 0)
                {
                    bool found = false;
                    foreach (object olocal in local)
                    {
                        TSCQueue localitem = olocal as TSCQueue;
                        if (localitem != null && localitem.EventKey.Trim().Length > 0 && localitem.SingerKey.Trim().Length > 0 && localitem.QueueRound > 0)
                        {
                            if (remoteitem.KeyEquals(localitem) == true)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found == false)
                        DeleteQueue(remoteitem, scRemote);
                }
            }

        }

        private void UpdateQueue(TSCQueue local, TSCQueue remote, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Update: {0}.{1}.{2}", local.EventKey, local.SingerKey, local.QueueRound));
            scRemote.GeneralStore(TableName.TSCQueue, TableAction.UPDATE, local.GetDataXml());
        }

        private void InsertQueue(TSCQueue local, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Insert: {0}.{1}.{2}", local.EventKey, local.SingerKey, local.QueueRound));
            scRemote.GeneralStore(TableName.TSCQueue, TableAction.INSERT, local.GetDataXml());
        }

        private void DeleteQueue(TSCQueue remote, SingingClub scRemote)
        {
            _sb.AppendLine(string.Format("Delete: {0}.{1}.{2}", remote.EventKey, remote.SingerKey, remote.QueueRound));
            scRemote.GeneralStore(TableName.TSCQueue, TableAction.DELETE, remote.GetDataXml());
        }
        #endregion

        private string Val(string val)
        {
            char[] tr = { ' ', '"' };
            if (val == null || val.Trim().Length == 0)
                return "";
            else
                return val.Trim(tr);
        }
        private string GenericPath(string path)
        {
            string rstring = "";
            if (path != null && path.Contains('\\') == true)
            {
                string dname = Path.GetDirectoryName(path);
                string fname = Path.GetFileNameWithoutExtension(path);
                if (dname.Length > 0 && fname.Length > 0)
                {
                    string cdg_mp3 = "\\karaoke\\cdg_mp3\\";
                    string cdg_zip = "\\karaoke\\cdg_zip\\";
                    int i = dname.ToLower().IndexOf(cdg_mp3);
                    if (i < 0)
                    {
                        i = dname.ToLower().IndexOf(cdg_zip);
                    }
                    if (i > 0)
                    {
                        rstring = "$\\" + dname.Substring(i + cdg_mp3.Length) + "\\" + fname + ".ext";
                    }
                }
            }
            return rstring;
        }
        private bool HasKey(List<TSCSongListItem> songs, TSCSongListItem sli)
        {
            foreach (TSCSongListItem target in songs)
            {
                if (target.FilePath.Trim().ToLower() == sli.FilePath.Trim().ToLower())
                    return true;
            }
            return false;
        }
    }
}
