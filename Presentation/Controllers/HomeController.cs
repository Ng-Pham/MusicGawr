using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _PartialFooter()
        {
            return PartialView();
        }
        public ActionResult _PartialMenu()
        {
            return PartialView();
        }
        public ActionResult _PartialPlayer()
        {
            return PartialView();
        }
        public ActionResult _PartialHeader()
        {
            return PartialView();
        }
        public ActionResult RecentPlay()
        {
            return PartialView();
        }
        public ActionResult WeekTop()
        {
            return PartialView();
        }
        public ActionResult Release()
        {
            return PartialView();
        }
        public ActionResult TopGenres()
        {
            return PartialView();
        }
    }
}