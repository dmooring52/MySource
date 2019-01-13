using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using QueueStation.Models;

namespace QueueStation.Controllers
{
    public class SongsController : Controller
    {
        // GET: Songs
        private void GetSongs()
        {
            if (SessionBag.Current.SongsPageOffset == null)
                SessionBag.Current.SongsPageOffset = 0;
            InitData d = new InitData();
            SingingClubClient client = new SingingClubClient();
            try
            {
                client.Open();
                TSCSongs songs = new TSCSongs();
                songs.PageOrder = SessionBag.Current.SongsPageOrder;
                songs.PageByDisk = SessionBag.Current.SongsPageByDisk;
                songs.PageOffset = SessionBag.Current.SongsPageOffset;
                songs.PageReturn = SessionBag.Current.SongsPageReturn;
                //songs.PageSearchString = SessionBag.Current.SongsSearchString;
                string whereclause = Utility.SqlString(SessionBag.Current.SongsPageSearchString);
                if (whereclause != null && whereclause.Length > 0)
                {
                    string srch = "title";
                    if (songs.PageByDisk == true)
                        srch = "disk";
                    else if (songs.PageOrder.ToLower().Contains("artist"))
                        srch = "artist";
                    if (whereclause.Length > 0)
                    {
                        if (whereclause.Length == 1)
                            songs.WhereClause = string.Format("{0} > '{1}'", srch, whereclause);
                        else
                            songs.WhereClause = string.Format("{0} like '%{1}%'", srch, whereclause);
                    }
                }
                if (songs.WhereClause.Trim().Length > 0)
                    songs.WhereClause += " and IsHelper = 'No'";
                else
                    songs.WhereClause = "IsHelper = 'No'";
                d.SongsXml = client.GeneralStore("TSCSongList_Main", "GET", songs.GetDataXml());
                XmlDocument SongsXml = new XmlDocument();
                XmlDocument SongsXmlReference = new XmlDocument();
                if (d.SongsXml != null && d.SongsXml.Trim().Length > 0)
                {
                    SongsXml.LoadXml(d.SongsXml);
                    XmlNodeList nodes = SongsXml.SelectNodes("/Root/Data");
                    if (nodes.Count > 0)
                    {
                        SongsXmlReference.LoadXml(d.SongsXml);
                        SetSongsPageRef();
                        SessionBag.Current.SongsXml = SongsXml;
                        SessionBag.Current.SongsXmlReference = SongsXmlReference;
                    }
                    else
                    {
                        SessionBag.Current.SongsPageOffset = SessionBag.Current.SongsPageOffsetRef;
                    }
                }
                else
                {
                    SessionBag.Current.SongsPageOffset = SessionBag.Current.SongsPageOffsetRef;
                }
                client.Close();
            }
            catch {}
            finally
            {
                if (client != null)
                    client.Close();
            }
        }
        private void SetSongsPageRef()
        {
            SessionBag.Current.SongsPageOffsetRef = SessionBag.Current.SongsPageOffset;
            SessionBag.Current.SongsPageOrderRef = SessionBag.Current.SongsPageOrder;
            SessionBag.Current.SongsPageByDiskRef = SessionBag.Current.SongsPageByDisk;
            SessionBag.Current.SongsPageReturnRef = SessionBag.Current.SongsPageReturn;
            SessionBag.Current.SongsPageSearchStringRef = SessionBag.Current.SongsPageSearchString;
        }
        private bool SetSongsPageDirty()
        {
            if (
                (SessionBag.Current.SongsPageOffset == null) ||
                (SessionBag.Current.SongsPageOrder == null) ||
                (SessionBag.Current.SongsPageByDisk == null) ||
                (SessionBag.Current.SongsPageReturn == null) ||
                (SessionBag.Current.SongsPageSearchString) == null
                )
            {
                SessionBag.Current.SongsPageOffset = 0;
                SessionBag.Current.SongsPageOrder = "Title";
                SessionBag.Current.SongsPageByDisk = false;
                SessionBag.Current.SongsPageReturn = 50;
                SessionBag.Current.SongsPageSearchString = "";
                return true;
            }

            if (SessionBag.Current.SongsPageOffsetRef != SessionBag.Current.SongsPageOffset)
                return true;
            if (SessionBag.Current.SongsPageOrderRef != SessionBag.Current.SongsPageOrder)
                return true;
            if (SessionBag.Current.SongsPageByDiskRef != SessionBag.Current.SongsPageByDisk)
                return true;
            if (SessionBag.Current.SongsPageReturnRef != SessionBag.Current.SongsPageReturn)
                return true;
            if (SessionBag.Current.SongsPageSearchStringRef != SessionBag.Current.SongsPageSearchString)
                return true;
            return false;
        }
        private void IncrementOffset()
        {
            if (
                (SessionBag.Current.SongsPageOffset != null) &&
                (SessionBag.Current.SongsPageReturn != null)
                )
            {
                SessionBag.Current.SongsPageOffset += SessionBag.Current.SongsPageReturn;
            }
        }
        private void DecrementOffset()
        {
            if (
                (SessionBag.Current.SongsPageOffset != null) &&
                (SessionBag.Current.SongsPageReturn != null)
                )
            {
                SessionBag.Current.SongsPageOffset -= SessionBag.Current.SongsPageReturn;
                if (SessionBag.Current.SongsPageOffset < 0)
                    SessionBag.Current.SongsPageOffset = 0;
            }
        }
        public ActionResult SongsView(int offset = 0)
        {
            if (offset > 0)
                IncrementOffset();
            else if (offset < 0)
                DecrementOffset();
            if (SetSongsPageDirty() == true)
                GetSongs();
            SongsGrid songs = new SongsGrid();
            songs.sortby = SessionBag.Current.SongsPageOrder;
            songs.bydisk = SessionBag.Current.SongsPageByDisk;
            songs.searchstring = SessionBag.Current.SongsPageSearchString;
            if (SessionBag.Current.SongsXml != null && SessionBag.Current.SongsXml is XmlDocument &&
                SessionBag.Current.SongsXmlReference != null && SessionBag.Current.SongsXmlReference is XmlDocument)
            {
                XmlDocument doc = SessionBag.Current.SongsXml as XmlDocument;
                XmlDocument docref = SessionBag.Current.SongsXmlReference as XmlDocument;
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                    songs.songs.Add(new MVCSongs(node));
                nodes = docref.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                    songs.songsref.Add(new MVCSongs(node));
                //songs.songs.Sort();
                songs.offset += 50;
                songs.Bind();
                ViewData.Model = songs;
            }

            return View();
        }
        public ActionResult PageNavigate()
        {
            SongsGrid songs = new SongsGrid();
            ViewData.Model = songs;

            int offset = 0;
            if (int.TryParse(Request.Form["txtoffset"], out offset) == true)
                songs.offset = offset;

            string sortby = Utility.NotNullString(Request.Form["sortby"]);
            bool bydisk = Utility.NotNullBool(Request.Form["bydisk"]);
            if (sortby == "Title" || sortby == "Artist")
            {
                if (sortby != SessionBag.Current.SongsPageOrderRef || bydisk != SessionBag.Current.SongsPageByDiskRef)
                    songs.offset = SessionBag.Current.SongsPageOffset = SessionBag.Current.SongsPageOffsetRef = 0;
                SessionBag.Current.SongsPageOrder = sortby;
                SessionBag.Current.SongsPageByDisk = bydisk;
            }

            string txtsrch = Utility.NotNullString(Request.Form["searchstring"]);
            if (txtsrch.Trim().Length > 0)
            {
                if (txtsrch != Utility.NotNullString(SessionBag.Current.SongsPageSearchStringRef))
                    songs.offset = SessionBag.Current.SongsPageOffset = SessionBag.Current.SongsPageOffsetRef = 0;
                SessionBag.Current.SongsPageSearchString = txtsrch;
            }
            else
            {
                if (txtsrch != Utility.NotNullString(SessionBag.Current.SongsPageSearchStringRef))
                    songs.offset = SessionBag.Current.SongsPageOffset = SessionBag.Current.SongsPageOffsetRef = 0;
                SessionBag.Current.SongsPageSearchString = "";
            }

            return View();
        }
        public ActionResult SongLaunch(string title, string artist, string disk)
        {
            TSCSongs song = new TSCSongs();
            if (SessionBag.Current.SongsXml != null)
            {
                XmlDocument doc = SessionBag.Current.SongsXml as XmlDocument;
                XmlNode node = doc.SelectSingleNode(string.Format("/Root/Data[Title=\"{0}\" and Artist=\"{1}\" and Disk=\"{2}\"]", title, artist, disk));
                if (node != null)
                    song = new TSCSongs(node);
            }
            return View(song);
        }
        public ActionResult SongTag(string title, string artist, string disk)
        {
            SingersGrid singers = new SingersGrid();
            singers.song = new TSCSongs();
            if (SessionBag.Current.SongsXml != null)
            {
                XmlDocument doc = SessionBag.Current.SongsXml as XmlDocument;
                XmlNode node = doc.SelectSingleNode(string.Format("/Root/Data[Title=\"{0}\" and Artist=\"{1}\" and Disk=\"{2}\"]", title, artist, disk));
                if (node != null)
                {
                    singers.song = new TSCSongs(node);
                }
            }

            Models.MvcList list = Controllers.QueueRoundData.GetRound(SessionBag.Current.RoundXml as XmlDocument, 1);
            foreach (object itm in list)
            {
                MVCQueue queue = itm as MVCQueue;
                if (queue != null)
                {
                    if (queue.SingerKey != null && queue.SingerKey.Trim().Length > 0)
                    {
                        MVCSingers singer = new MVCSingers();
                        singer.SingerKey = queue.SingerKey;
                        singers.singers.Add(singer);
                        singers.singersref.Add(singer);
                    }
                }
            }
            singers.Bind();
            ViewData.Model = singers;
            return View();
        }
        public ActionResult SongTagEdit(string singerkey, string title, string artist, string disk)
        {

            SingingClubClient client = new SingingClubClient();
            TSCSongs songs = null;
            if (SessionBag.Current.SongsXml != null && client != null &&
                title != null && artist != null && disk != null && singerkey != null &&
                title.Trim().Length > 0 && artist.Trim().Length > 0 && disk.Trim().Length > 0 && singerkey.Trim().Length > 0)
            {
                XmlDocument doc = SessionBag.Current.SongsXml as XmlDocument;
                XmlNode node = doc.SelectSingleNode(string.Format("/Root/Data[Title=\"{0}\" and Artist=\"{1}\" and Disk=\"{2}\"]", title, artist, disk));
                if (node != null)
                {
                    songs = new TSCSongs(node);
                    TSCSingersSongs singerssongs = new TSCSingersSongs(singerkey, songs.Title, songs.Artist);
                    string xml = client.GeneralStore("TSCSingersSongs", "GET", singerssongs.GetDataXml());
                    if (xml != null && xml.Trim().Length == 0)
                    {
                        singerssongs.OneDriveZip = songs.OneDriveZip;
                        xml = client.GeneralStore("TSCSingersSongs", "INSERT", singerssongs.GetDataXml());
                    }
                }
            }

            return View();
        }
    }
}