using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QueueStation.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult EventsView()
        {
            EventsGrid events = new EventsGrid();
            if (SessionBag.Current.EventsXml != null && SessionBag.Current.EventsXml is XmlDocument &&
                SessionBag.Current.EventsXmlReference != null && SessionBag.Current.EventsXmlReference is XmlDocument)
            {
                XmlDocument doc = SessionBag.Current.EventsXml as XmlDocument;
                XmlDocument docref = SessionBag.Current.EventsXmlReference as XmlDocument;
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    events.events.Add(new MVCEvents(node));
                }
                nodes = docref.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    events.eventsref.Add(new MVCEvents(node));
                }

                events.events.Sort();
                events.events.Reverse();
                events.Bind();
                ViewData.Model = events;
            }

            return View();
        }
        public ActionResult PageEditEvent(string VenueKey, string EventKey)
        {
            MVCEvents tscevent = null;
            if (VenueKey != null && EventKey != null && VenueKey.Trim().Length > 0 && EventKey.Trim().Length > 0)
            {
                EventData eventData = new EventData(VenueKey, EventKey, SessionBag.Current.EventsXml as XmlDocument);
                tscevent = eventData.GetEvent();
            }
            else
            {
                tscevent = new MVCEvents();
                XmlDocument vdoc = SessionBag.Current.VenuesXml as XmlDocument;
                if (vdoc != null)
                {
                    XmlNodeList nodes = vdoc.SelectNodes("Root/Data/VenueKey");
                    if (nodes != null)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            string venue = node.InnerText;
                            if (venue != null && venue == "IAG")
                            {
                                tscevent.VenueList.Add(venue);
                                break;
                            }
                        }
                        foreach (XmlNode node in nodes)
                        {
                            string venue = node.InnerText;
                            if (venue != null && venue.Trim().Length > 0 && venue != "IAG")
                                tscevent.VenueList.Add(venue);
                        }
                    }
                }
            }
            return View(tscevent);
        }
        public ActionResult PageSetEvent(MVCEvents model)
        {
            string venuekey = Request.Form["VenueList"];
            if (venuekey == null || venuekey.Trim().Length == 0)
                venuekey = Request.Form["VenueKey"];
            string eventkey = Request.Form["EventKey"];
            string eventname = Request.Form["EventName"];
            string eventdate = Request.Form["EventDate"];
            string eventaddress = Request.Form["EventAddress"];
            string eventemail = Request.Form["EventEmail"];

            string command = Request.Form["command"];
            if (command == null)
                command = "";
            else
                command = command.Trim().ToLower();
            if (command == "remove" && venuekey != null && venuekey.Trim().Length > 0 && eventkey != null && eventkey.Trim().Length > 0)
            {
                MVCEvents theevent = new MVCEvents();
                theevent.VenueKey = venuekey;
                theevent.EventKey = eventkey;
                SingingClubClient client = new SingingClubClient();
                client.Open();
                string result = client.GeneralStore("TSCEvents", "DELETE", theevent.GetDataXml());
                if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                {
                    string _EventsXml = client.GeneralStore("TSCEvents", "GET", (new MVCEvents()).GetDataXml());
                    client.Close();
                    XmlDocument EventsXml = new XmlDocument();
                    XmlDocument EventsXmlReference = new XmlDocument();

                    EventsXml.LoadXml(_EventsXml);
                    EventsXmlReference.LoadXml(_EventsXml);

                    SessionBag.Current.EventsXml = EventsXml;
                    SessionBag.Current.EventsXmlReference = EventsXmlReference;
                }
            }

            EventsGrid events = new EventsGrid();
            if (SessionBag.Current.EventsXml != null && SessionBag.Current.EventsXml is XmlDocument && 
                SessionBag.Current.EventsXmlReference != null && SessionBag.Current.EventsXmlReference is XmlDocument &&
                venuekey != null && eventkey != null && venuekey.Trim().Length > 0 && eventkey.Trim().Length > 0)
            {
                XmlDocument doc = SessionBag.Current.EventsXml as XmlDocument;
                XmlDocument docref = SessionBag.Current.EventsXmlReference as XmlDocument;
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                bool found = false;
                foreach (XmlNode node in nodes)
                {
                    MVCEvents add = new MVCEvents(node);
                    if (add.VenueKey == venuekey && add.EventKey == eventkey)
                    {
                        SetText(node, "EventName", eventname);
                        SetDate(node, "EventDate", eventdate);
                        SetText(node, "EventAddress", eventaddress);
                        SetText(node, "EventEmail", eventemail);
                        add = new MVCEvents(node);
                        found = true;
                    }
                    events.events.Add(add);
                }
                if (found == false && command != "remove")
                {
                    XmlNode root = doc.SelectSingleNode("/Root");
                    if (root != null)
                    {
                        XmlNode data = doc.CreateNode(XmlNodeType.Element, "Data", null);
                        if (data != null)
                        {
                            root.AppendChild(data);
                            SetText(data, "VenueKey", venuekey);
                            SetText(data, "EventKey", eventkey);
                            SetText(data, "EventName", eventname);
                            SetDate(data, "EventDate", eventdate);
                            SetText(data, "EventAddress", eventaddress);
                            SetText(data, "EventEmail", eventemail);
                            MVCEvents add = new MVCEvents(data);
                            events.events.Add(add);
                        }
                    }
                }
                nodes = docref.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    events.eventsref.Add(new MVCEvents(node));
                }
                events.events.Sort();
                events.events.Reverse();
                events.Bind();
                ViewData.Model = events;
            }

            return View();
        }
        private void SetText(XmlNode node, string name, string value)
        {
            if (name != null && name.Trim().Length > 0)
            {
                XmlNode child = node[name];
                if (value == null || value.Trim().Length == 0)
                {
                    if (child != null)
                        child.InnerText = "";
                }
                else
                {
                    if (child == null)
                    {
                        child = node.OwnerDocument.CreateNode(XmlNodeType.Element, name, null);
                        child.InnerText = value;
                        node.AppendChild(child);
                    }
                    else
                    {
                        child.InnerText = value;
                    }
                }
            }
        }
        private void SetDate(XmlNode node, string name, string value)
        {
            if (name != null && name.Trim().Length > 0)
            {
                XmlNode child = node[name];
                if (value == null || value.Trim().Length == 0)
                {
                    if (child != null)
                        child.InnerText = "";
                }
                else
                {
                    if (child == null)
                    {
                        DateTime parsed = DateTime.MinValue;
                        DateTime.TryParse(value, out parsed);
                        child = node.OwnerDocument.CreateNode(XmlNodeType.Element, name, null);
                        child.InnerText = parsed.ToString();
                        node.AppendChild(child);
                    }
                    else
                    {
                        DateTime parsed = DateTime.MinValue;
                        DateTime.TryParse(value, out parsed);
                        child.InnerText = parsed.ToString();
                    }
                }
            }
        }
        public ActionResult PageAddSaveRevert()
        {
            EventsGrid events = new EventsGrid();
            if (Request.Form["Command"] != null && Request.Form["Command"].Trim().Length > 0)
            {
                string command = Request.Form["Command"].ToLower().Trim();
                events.command = command;
                switch (command)
                {
                    case "save":
                        if (SessionBag.Current.EventsXml != null && SessionBag.Current.EventsXml is XmlDocument &&
                            SessionBag.Current.EventsXmlReference != null && SessionBag.Current.EventsXmlReference is XmlDocument)
                        {
                            XmlDocument doc = SessionBag.Current.EventsXml as XmlDocument;
                            XmlDocument docref = SessionBag.Current.EventsXmlReference as XmlDocument;
                            XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                            foreach (XmlNode node in nodes)
                            {
                                events.events.Add(new MVCEvents(node));
                            }
                            nodes = docref.SelectNodes("/Root/Data");
                            foreach (XmlNode node in nodes)
                            {
                                events.eventsref.Add(new MVCEvents(node));
                            }
                        }
                        if (command == "save")
                        {
                            string result = events.Save();

                            if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                            {
                                SingingClubClient client = new SingingClubClient();
                                client.Open();
                                string _EventsXml = client.GeneralStore("TSCEvents", "GET", (new MVCEvents()).GetDataXml());
                                client.Close();
                                XmlDocument EventsXml = new XmlDocument();
                                XmlDocument EventsXmlReference = new XmlDocument();

                                EventsXml.LoadXml(_EventsXml);
                                EventsXmlReference.LoadXml(_EventsXml);

                                SessionBag.Current.EventsXml = EventsXml;
                                SessionBag.Current.EventsXmlReference = EventsXmlReference;
                            }
                        }
                        break;
                    case "add":
                        break;
                    case "revert":
                        EventsGrid.Revert();
                        break;
                }
            }
            return View(events);
        }
    }
}