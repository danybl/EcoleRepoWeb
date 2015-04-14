using EcoleWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EcoleWeb.Controllers
{
    [Authorize(Users = "scott@lacarte.com")]
    public class AdminController : Controller
    {        
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        
  
    }
}