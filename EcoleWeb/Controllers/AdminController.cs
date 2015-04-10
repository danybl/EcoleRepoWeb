using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoleWeb.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminHome()
        {
            return View();
        }

        public ActionResult AdminFAQ()
        {
            return View();
        }
    }
}