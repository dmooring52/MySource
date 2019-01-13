using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Web;
using System.Web.Mvc;

namespace QueueStation.Controllers
{
    public class EditQueueController : Controller
    {
        // GET: EditQueue
        public ActionResult PageEditQueue(string SingerKey, int QueueRound)
        {
            if (SingerKey != null && SingerKey.Trim().Length > 0)
            {
                Models.MvcList list = Controllers.QueueRoundData.GetRound(SessionBag.Current.RoundXml as XmlDocument, QueueRound);
                foreach (object itm in list)
                {
                    MVCQueue queue = itm as MVCQueue;
                    if (queue != null)
                    {
                        if (queue.SingerKey == SingerKey)
                        {
                            ViewData.Model = queue;
                            return View(queue);
                        } 
                    }
                }
            }
            else
            {
                MVCQueue model = new MVCQueue();
                XmlDocument doc = SessionBag.Current.SingersXml;
                if (doc != null)
                {
                    XmlNodeList nodes = doc.SelectNodes("/Root/Data/SingerKey");
                    foreach (XmlNode node in nodes)
                    {
                        string singer = node.InnerText;
                        if (singer != null && singer.Trim().Length > 0)
                            model.SingerList.Add(singer);
                    }
                }
                model.QueueRound = QueueRound;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult PageAddSaveRevert(SingerRound singerround)
        {
            int round = Utility.GetInt(Request.Form.Get("QueueRound"));
            string command = Request.Form.Get("command");
            singerround.round = round;
            singerround.command = command;
            if (round > 0)
            {
                switch (command.ToLower())
                {
                    case "revert":
                        XmlDocument doc = SessionBag.Current.RoundXml as XmlDocument;
                        XmlDocument docref = SessionBag.Current.RoundXmlReference as XmlDocument;
                        if (doc != null && docref != null)
                        {
                            doc.LoadXml(docref.OuterXml);
                        }
                        break;
                    case "save":
                        QueueGrid model = new QueueGrid(round);
                        model.QueueRound = round;
                        model.queues = QueueRoundData.GetRounds(SessionBag.Current.RoundXml as XmlDocument, round);
                        model.queuesref = QueueRoundData.GetRounds(SessionBag.Current.RoundXmlReference as XmlDocument, round);
                        string result = model.Save();
                        string _RoundXml = "";
                        if (result != null && Utility.IsNumber(result) && int.Parse(result) > 0)
                        {
                            SingingClubClient client = new SingingClubClient();
                            client.Open();
                            string eventKey = SessionBag.Current.EventKey;
                            if (eventKey.Trim().Length > 0)
                            {
                                MVCQueue q = new MVCQueue();
                                q.EventKey = eventKey;
                                q.QueueRound = -1;
                                _RoundXml = client.GeneralStore("TSCQueue", "GET", q.GetDataXml());
                            }
                            client.Close();
                            XmlDocument RoundXml = new XmlDocument();
                            XmlDocument RoundXmlReference = new XmlDocument();

                            RoundXml.LoadXml(_RoundXml);
                            RoundXmlReference.LoadXml(_RoundXml);

                            SessionBag.Current.RoundXml = RoundXml;
                            SessionBag.Current.RoundXmlReference = RoundXmlReference;
                        }
                        break;
                    case "add":
                        break;
                    default:
                        break;
                }
            }
            return View(singerround);
        }

        public ActionResult PageSetRound(MVCQueue model)
        {
            string state = Request.Form["theQueueState"];
            string singerlist = Request.Form["SingerList"];
            int maxorder = 0;
            if (model.QueueRound > 0)
            {
                Models.MvcList sessionround = QueueRoundData.GetRound(SessionBag.Current.RoundXml, model.QueueRound);
                bool found = false;
                foreach (MVCQueue q in sessionround)
                {
                    if (q.QueueOrder > maxorder)
                        maxorder = q.QueueOrder;
                    if (q.SingerKey == model.SingerKey)
                    {
                        found = true;
                        if (MVCQueue.RoundsEqual(q, model) == false)
                        {
                            SetField("QueueSong", model.QueueSong, model.SingerKey, model.QueueRound);
                            SetField("QueueArtist", model.QueueArtist, model.SingerKey, model.QueueRound);
                            SetField("QueueNote", model.QueueNote, model.SingerKey, model.QueueRound);
                            SetField("QueueLink", model.QueueLink, model.SingerKey, model.QueueRound);
                            if (state != null && state.Trim().Length > 0)
                                SetField("QueueState", state, model.SingerKey, model.QueueRound);
                        }
                    }
                }
                if (found == false && singerlist != null && singerlist.Trim().Length > 0)
                {
                    XmlDocument doc = SessionBag.Current.SingersXml as XmlDocument;
                    if (doc != null)
                    {
                        XmlNode node = doc.SelectSingleNode(string.Format("/Root/Data[SingerKey='{0}']/SingerKey", singerlist.Trim()));
                        if (node != null)
                        {
                            doc = SessionBag.Current.RoundXml as XmlDocument;
                            if (doc != null && (SessionBag.Current.EventKey as string) != null && (SessionBag.Current.EventKey as string).Trim().Length > 0)
                            {
                                model.EventKey = (SessionBag.Current.EventKey as string);
                                model.SingerKey = singerlist.Trim();
                                model.QueueOrder = maxorder + 1;
                                if (state != null && state.Trim().Length > 0)
                                    model.QueueState = state;
                                else
                                    model.QueueState = "Pending";
                                model.AppendToDocument(doc);
                            }
                        }
                    }
                }
            }
            return View(model);
        }
        private void SetField(string field, string value, string singerkey, int round)
        {
            XmlDocument doc = SessionBag.Current.RoundXml as XmlDocument;
            XmlNode node = doc.SelectSingleNode(string.Format("/Root/Data[SingerKey='{0}' and QueueRound={1}]", singerkey, round));
            if (node != null)
            {
                XmlNode fieldnode = node.SelectSingleNode(field);
                if (fieldnode != null)
                    fieldnode.InnerText = value;
                else
                {
                    fieldnode = doc.CreateNode(XmlNodeType.Element, field, null);
                    fieldnode.InnerText = value;
                    node.AppendChild(fieldnode);
                }
            }
        }

        public ActionResult PageLaunch(string QueueRound, string QueueLink)
        {
            if (QueueLink != null && QueueLink.Trim().Length > 0)
                new LaunchForm(QueueLink).Launch();
            return View(new QueueGrid(Utility.GetInt(QueueRound)));
        }
    }
}