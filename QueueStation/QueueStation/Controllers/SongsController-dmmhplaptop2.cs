using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QueueStation.Controllers
{
    public class SongsController : Controller
    {
        // GET: Songs
        public ActionResult SongsView()
        {
            SongsGrid songs = new SongsGrid();
            if (SessionBag.Current.SongsXml != null && SessionBag.Current.SongsXml is XmlDocument &&
                SessionBag.Current.SongsXmlReference != null && SessionBag.Current.SongsXmlReference is XmlDocument)
            {
                XmlDocument doc = SessionBag.Current.SongsXml as XmlDocument;
                XmlDocument docref = SessionBag.Current.SongsXmlReference as XmlDocument;
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    songs.songs.Add(new MVCSongs(node));
                }
                nodes = docref.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    songs.songsref.Add(new MVCSongs(node));
                }

                //songs.songs.Sort();
                songs.Bind();
                ViewData.Model = songs;
            }

            return View();
        }
    }
}