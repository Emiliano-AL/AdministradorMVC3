using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace adoProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Vieje en autobuses ADO";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
