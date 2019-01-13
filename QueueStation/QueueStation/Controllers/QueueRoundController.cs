using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QueueStation.Controllers
{
    public class QueueRoundController : Controller
    {
        // GET: QueueRound
        public ActionResult RoundView(int QueueRound=0)
        {
            QueueGrid queueGrid = new QueueGrid(QueueRound);
            queueGrid.queues = QueueRoundData.GetRounds(SessionBag.Current.RoundXml as XmlDocument, QueueRound);
            queueGrid.queuesref = QueueRoundData.GetRounds(SessionBag.Current.RoundXmlReference as XmlDocument, QueueRound);
            queueGrid.queues.Sort();
            queueGrid.Bind();
            ViewData.Model = queueGrid;
            return View();
        }
    }
}