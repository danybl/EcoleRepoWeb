﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoleWeb.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        
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