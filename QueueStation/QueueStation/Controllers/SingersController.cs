using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QueueStation.Controllers
{
    public class SingersController : Controller
    {
        // GET: Singers
        public ActionResult SingersView()
        {
            SingersGrid singers = new SingersGrid();
            if (SessionBag.Current.SingersXml != null && SessionBag.Current.SingersXml is XmlDocument &&
                SessionBag.Current.SingersXmlReference != null && SessionBag.Current.SingersXmlReference is XmlDocument)
            {
                XmlDocument doc = SessionBag.Current.SingersXml as XmlDocument;
                XmlDocument docref = SessionBag.Current.SingersXmlReference as XmlDocument;
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    singers.singers.Add(new MVCSingers(node));
                }
                nodes = docref.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    singers.singersref.Add(new MVCSingers(node));
                }

                //singers.singers.Sort();
                singers.Bind();
                ViewData.Model = singers;
            }

            return View();
        }

        public ActionResult PageEditSinger(string SingerKey, string Command="")
        {
            MVCSingers tscsinger = null;
            if (SingerKey != null && SingerKey.Trim().Length > 0)
            {
                SingerData singerData = new SingerData(Command, SingerKey, SessionBag.Current.SingersXml as XmlDocument);
                tscsinger = singerData.GetSinger();
            }
            else
            {
                tscsinger = new MVCSingers();
                tscsinger.command = Command;
            }
            return View(tscsinger);
        }
        public ActionResult PageSetSinger(MVCSingers model)
        {
            string singerkey = Request.Form["SingerKey"];
            string singername = Request.Form["SingerName"];
            string singeremail = Request.Form["SingerEmail"];

            string command = Request.Form["command"];
            if (command == null)
                command = "";
            else
                command = command.Trim().ToLower();
            if (command == "remove" && singerkey != null && singerkey.Trim().Length > 0)
            {
                MVCSingers singer = new MVCSingers();
                singer.SingerKey = singerkey;
                SingingClubClient client = new SingingClubClient();
                string result = client.GeneralStore("TSCSingers", "DELETE", singer.GetDataXml());
                if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                {
                    string _SingersXml = client.GeneralStore("TSCSingers", "GET", (new MVCSingers()).GetDataXml());
                    client.Close();
                    XmlDocument SingersXml = new XmlDocument();
                    XmlDocument SingersXmlReference = new XmlDocument();

                    SingersXml.LoadXml(_SingersXml);
                    SingersXmlReference.LoadXml(_SingersXml);

                    SessionBag.Current.SingersXml = SingersXml;
                    SessionBag.Current.SingersXmlReference = SingersXmlReference;
                }
            }

            SingersGrid singers = new SingersGrid();
            if (SessionBag.Current.SingersXml != null && SessionBag.Current.SingersXml is XmlDocument &&
                SessionBag.Current.SingersXmlReference != null && SessionBag.Current.SingersXmlReference is XmlDocument &&
                singerkey != null && singerkey.Trim().Length > 0)
            {
                XmlDocument doc = SessionBag.Current.SingersXml as XmlDocument;
                XmlDocument docref = SessionBag.Current.SingersXmlReference as XmlDocument;
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                bool found = false;
                foreach (XmlNode node in nodes)
                {
                    MVCSingers add = new MVCSingers(node);
                    if (add.SingerKey == singerkey)
                    {
                        SetText(node, "SingerName", singername);
                        SetText(node, "SingerEmail", singeremail);
                        add = new MVCSingers(node);
                        found = true;
                    }
                    singers.singers.Add(add);
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
                            SetText(data, "SingerKey", singerkey);
                            SetText(data, "SingerName", singername);
                            SetText(data, "SingerEmail", singeremail);
                            MVCSingers add = new MVCSingers(data);
                            singers.singers.Add(add);
                        }
                    }
                }
                nodes = docref.SelectNodes("/Root/Data");
                foreach (XmlNode node in nodes)
                {
                    singers.singersref.Add(new MVCSingers(node));
                }
                //singers.singers.Sort();
                singers.Bind();
                ViewData.Model = singers;
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
            SingersGrid singers = new SingersGrid();
            if (Request.Form["Command"] != null && Request.Form["Command"].Trim().Length > 0)
            {
                string command = Request.Form["Command"].ToLower().Trim();
                singers.command = command;
                switch (command)
                {
                    case "save":
                        if (SessionBag.Current.SingersXml != null && SessionBag.Current.SingersXml is XmlDocument &&
                            SessionBag.Current.SingersXmlReference != null && SessionBag.Current.SingersXmlReference is XmlDocument)
                        {
                            XmlDocument doc = SessionBag.Current.SingersXml as XmlDocument;
                            XmlDocument docref = SessionBag.Current.SingersXmlReference as XmlDocument;
                            XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                            foreach (XmlNode node in nodes)
                            {
                                singers.singers.Add(new MVCSingers(node));
                            }
                            nodes = docref.SelectNodes("/Root/Data");
                            foreach (XmlNode node in nodes)
                            {
                                singers.singersref.Add(new MVCSingers(node));
                            }
                        }
                        if (command == "save")
                        {
                            string result = singers.Save();
                            if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                            {
                                SingingClubClient client = new SingingClubClient();
                                client.Open();
                                string _SingersXml = client.GeneralStore("TSCSingers", "GET", (new MVCSingers()).GetDataXml());
                                client.Close();
                                XmlDocument SingersXml = new XmlDocument();
                                XmlDocument SingersXmlReference = new XmlDocument();

                                SingersXml.LoadXml(_SingersXml);
                                SingersXmlReference.LoadXml(_SingersXml);

                                SessionBag.Current.SingersXml = SingersXml;
                                SessionBag.Current.SingersXmlReference = SingersXmlReference;
                            }
                        }
                        break;
                    case "add":
                        break;
                    case "revert":
                        SingersGrid.Revert();
                        break;
                }
            }
            return View(singers);
        }
    }
}