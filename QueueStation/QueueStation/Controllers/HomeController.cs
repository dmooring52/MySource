using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QueueStation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string venuekey = SessionBag.Current.VenueKey as string;
            string eventkey = SessionBag.Current.EventKey as string;
            RootData rd = new RootData(venuekey, eventkey);
            if (venuekey != null && venuekey.Trim().Length > 0)
            {
                XmlDocument doc = SessionBag.Current.VenuesXml as XmlDocument;
                XmlNode node = doc.SelectSingleNode(string.Format("/Root/Data[VenueKey='{0}']", venuekey));
                if (node != null)
                {
                    rd.VenueName = Utility.GetXmlString(node, "VenueName");
                }
            }
            if (eventkey != null && eventkey.Trim().Length > 0)
            {
                XmlDocument doc = SessionBag.Current.EventsXml as XmlDocument;
                XmlNode node = doc.SelectSingleNode(string.Format("/Root/Data[EventKey='{0}']", eventkey));
                if (node != null)
                {
                    rd.EventName = Utility.GetXmlString(node, "EventName");
                }
                List<SelectListItem> list = new List<SelectListItem>();
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                if (nodes != null)
                {
                    foreach (XmlNode n in nodes)
                    {
                        string vkey = Utility.GetXmlString(n, "VenueKey");
                        string ekey = Utility.GetXmlString(n, "EventKey");
                        DateTime dt = Utility.GetXmlDateTime(n, "EventDate");
                        if (ekey.Length > 0)
                        {
                            SelectListItem si = new SelectListItem { Text = ekey, Value = Utility.SortableDate(dt) + "\t" + ekey + "\t" + vkey };
                            list.Add(si);
                            if (ekey == eventkey)
                                si.Selected = true;
                        }
                    }
                }
                list.Sort((a, b) => a.Value.CompareTo(b.Value));
                list.Reverse();

                doc = SessionBag.Current.RoundXml as XmlDocument;
                if (doc != null)
                {
                    List<OnDeck> ondeck = new List<OnDeck>();
                    nodes = doc.SelectNodes("/Root/Data");
                    foreach (XmlNode n in nodes)
                    {
                        string sste = Utility.GetXmlString(n, "QueueState");
                        if (sste == "" || sste.Trim().ToLower() == "pending")
                        {
                            ondeck.Add(new OnDeck(Utility.GetXmlString(n, "SingerKey"), Utility.GetXmlInteger(n, "QueueRound"), Utility.GetXmlInteger(n, "QueueOrder")));
                        }
                    }
                    ondeck.Sort();
                    ondeck.Reverse();
                    List<SelectListItem> lod = new List<SelectListItem>();
                    int iondeck = 5;
                    if (ondeck.Count < iondeck)
                        iondeck = ondeck.Count();
                    for (int i = 0; i < iondeck; i++)
                    {
                        OnDeck od = ondeck[i];
                        SelectListItem item = new SelectListItem();
                        item.Text = string.Format("{0} - (Round {1})", od.SingerKey, od.QueueRound);
                        item.Value = od.SingerKey + "\t" + od.QueueRound.ToString();
                        lod.Add(item);
                    }
                    rd.OnDeckList = lod;
                }
                rd.EventList = list;
            }
            return View(rd);
        }
        public ActionResult EventChange(RootData root)
        {
            if (Request.Form["command"] != null && Request.Form["command"].ToLower() == "launch")
            {
                if (root.OnDeckSelection != null && root.OnDeckSelection.Trim().Length > 0 && root.OnDeckSelection.Contains('\t'))
                {
                    string[] ods = root.OnDeckSelection.Split('\t');
                    string singer = ods[0].Trim();
                    int round = Utility.GetInt(ods[1]);
                    if (singer.Length > 0 && round > 0)
                    {
                        XmlDocument doc = SessionBag.Current.RoundXml as XmlDocument;
                        if (doc != null)
                        {
                            XmlNode n = doc.SelectSingleNode(string.Format("/Root/Data[SingerKey='{0}' and QueueRound='{1}']", singer, round));
                            if (n != null)
                            {
                                string path = Utility.GetXmlString(n, "QueueLink");
                                if (path.Length > 0)
                                    new LaunchForm(path).Launch();
                            }
                        }
                    }
                }
            }
            else
            {
                string eventvenuekey = "";
                if (root.EventSelection != null && root.EventSelection.Length > 0 && root.EventSelection.IndexOf('\t') > 0)
                    eventvenuekey = root.EventSelection.Substring(root.EventSelection.IndexOf('\t') + 1);
                if (eventvenuekey.Length > 0)
                {
                    InitData d = new InitData();
                    InitHelper h = new InitHelper();
                    XmlDocument doc = SessionBag.Current.EventsXml as XmlDocument;
                    if (doc != null)
                        d.EventsXml = doc.OuterXml;
                    doc = SessionBag.Current.VenuesXml as XmlDocument;
                    if (doc != null)
                        d.VenuesXml = doc.OuterXml;
                    doc = SessionBag.Current.SingersXml as XmlDocument;
                    if (doc != null)
                        d.SingersXml = doc.OuterXml;
                    h.InitialLoad(d, eventvenuekey);
                }
            }
            return View(root);
        }
    }
}