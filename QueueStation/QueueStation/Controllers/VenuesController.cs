using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using QueueStation.Models;

namespace QueueStation.Controllers
{
    public class VenuesController : Controller
    {
        // GET: Venues
        public ActionResult VenuesView()
        {
            VenuesGrid venues = new VenuesGrid();
            if (SessionBag.Current.VenuesXml != null && SessionBag.Current.VenuesXml is XmlDocument &&
                SessionBag.Current.VenuesXmlReference != null && SessionBag.Current.VenuesXmlReference is XmlDocument)
            {
                XmlDocument doc = SessionBag.Current.VenuesXml as XmlDocument;
                XmlDocument docref = SessionBag.Current.VenuesXmlReference as XmlDocument;
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    venues.venues.Add(new MVCVenues(node));
                }
                nodes = docref.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    venues.venuesref.Add(new MVCVenues(node));
                }

                //venues.venues.Sort();
                venues.Bind();
                ViewData.Model = venues;
            }

            return View();
        }

        public ActionResult PageEditVenue(string VenueKey, string Command = "")
        {
            MVCVenues tscvenue = null;
            if (VenueKey != null && VenueKey.Trim().Length > 0)
            {
                VenueData venueData = new VenueData(Command, VenueKey, SessionBag.Current.VenuesXml as XmlDocument);
                tscvenue = venueData.GetVenue();
            }
            else
            {
                tscvenue = new MVCVenues();
                tscvenue.command = Command;
            }
            return View(tscvenue);
        }

        public ActionResult PageSetVenue(MVCVenues model)
        {
            string venuekey = Request.Form["VenueKey"];
            string venuename = Request.Form["VenueName"];
            string venueemail = Request.Form["VenueEmail"];
            string venueaddress = Request.Form["VenueAddress"];
            string venuecontact = Request.Form["VenueContact"];
            string venuephone = Request.Form["VenuePhone"];

            string command = Request.Form["command"];
            if (command == null)
                command = "";
            else
                command = command.Trim().ToLower();
            if (command == "remove" && venuekey != null && venuekey.Trim().Length > 0)
            {
                MVCVenues venue = new MVCVenues();
                venue.VenueKey = venuekey;
                SingingClubClient client = new SingingClubClient();
                client.Open();
                string result = client.GeneralStore("TSCVenues", "DELETE", venue.GetDataXml());
                if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                {
                    string _VenuesXml = client.GeneralStore("TSCVenues", "GET", (new MVCVenues()).GetDataXml());
                    client.Close();
                    XmlDocument VenuesXml = new XmlDocument();
                    XmlDocument VenuesXmlReference = new XmlDocument();

                    VenuesXml.LoadXml(_VenuesXml);
                    VenuesXmlReference.LoadXml(_VenuesXml);

                    SessionBag.Current.VenuesXml = VenuesXml;
                    SessionBag.Current.VenuesXmlReference = VenuesXmlReference;
                }
            }

            VenuesGrid venues = new VenuesGrid();
            if (SessionBag.Current.VenuesXml != null && SessionBag.Current.VenuesXml is XmlDocument &&
                SessionBag.Current.VenuesXmlReference != null && SessionBag.Current.VenuesXmlReference is XmlDocument &&
                venuekey != null && venuekey.Trim().Length > 0)
            {
                XmlDocument doc = SessionBag.Current.VenuesXml as XmlDocument;
                XmlDocument docref = SessionBag.Current.VenuesXmlReference as XmlDocument;
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                bool found = false;
                foreach (XmlNode node in nodes)
                {
                    MVCVenues add = new MVCVenues(node);
                    if (add.VenueKey == venuekey)
                    {
                        SetText(node, "VenueName", venuename);
                        SetText(node, "VenueEmail", venueemail);
                        SetText(node, "VenueAddress", venueaddress);
                        SetText(node, "VenueContact", venuecontact);
                        SetText(node, "VenuePhone", venuephone);
                        add = new MVCVenues(node);
                        found = true;
                    }
                    venues.venues.Add(add);
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
                            SetText(data, "VenueName", venuename);
                            SetText(data, "VenueEmail", venueemail);
                            SetText(data, "VenueAddress", venueaddress);
                            SetText(data, "VenueContact", venuecontact);
                            SetText(data, "VenuePhone", venuephone);
                            MVCVenues add = new MVCVenues(data);
                            venues.venues.Add(add);
                        }
                    }
                }
                nodes = docref.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    venues.venuesref.Add(new MVCVenues(node));
                }
                //venues.venues.Sort();
                venues.Bind();
                ViewData.Model = venues;
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

        public ActionResult PageAddSaveRevert()
        {
            VenuesGrid venues = new VenuesGrid();
            if (Request.Form["Command"] != null && Request.Form["Command"].Trim().Length > 0)
            {
                string command = Request.Form["Command"].ToLower().Trim();
                venues.command = command;
                switch (command)
                {
                    case "save":
                        if (SessionBag.Current.VenuesXml != null && SessionBag.Current.VenuesXml is XmlDocument &&
                            SessionBag.Current.VenuesXmlReference != null && SessionBag.Current.VenuesXmlReference is XmlDocument)
                        {
                            XmlDocument doc = SessionBag.Current.VenuesXml as XmlDocument;
                            XmlDocument docref = SessionBag.Current.VenuesXmlReference as XmlDocument;
                            XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                            foreach (XmlNode node in nodes)
                            {
                                venues.venues.Add(new MVCVenues(node));
                            }
                            nodes = docref.SelectNodes("/Root/Data");
                            foreach (XmlNode node in nodes)
                            {
                                venues.venuesref.Add(new MVCVenues(node));
                            }
                        }
                        if (command == "save")
                        {
                            string result = venues.Save();
                            if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                            {
                                SingingClubClient client = new SingingClubClient();
                                client.Open();
                                string _VenuesXml = client.GeneralStore("TSCVenues", "GET", (new MVCVenues()).GetDataXml());
                                client.Close();
                                XmlDocument VenuesXml = new XmlDocument();
                                XmlDocument VenuesXmlReference = new XmlDocument();

                                VenuesXml.LoadXml(_VenuesXml);
                                VenuesXmlReference.LoadXml(_VenuesXml);

                                SessionBag.Current.VenuesXml = VenuesXml;
                                SessionBag.Current.VenuesXmlReference = VenuesXmlReference;
                            }
                        }
                        break;
                    case "add":
                        break;
                    case "revert":
                        VenuesGrid.Revert();
                        break;
                }
            }
            return View(venues);
        }
    }
}