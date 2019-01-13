using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QueueStation.Controllers
{
    public class CredPageController : Controller
    {
        // GET: CredPage
        public ActionResult CredPage()
        {
            return PartialView();
        }
        public ActionResult Credentials()
        {
            SessionBag.Current.HasCredentials = false;
            SessionBag.Current.IsKj = false;
            clash c = new clash(Request.Form["password"]);
            SessionBag.Current.HasCredentials = c.HasCredentials;
            SessionBag.Current.IsKj = c.IsKj;
            return View();
        }
    }
}