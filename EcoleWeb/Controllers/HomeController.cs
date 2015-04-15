using EcoleWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoleWeb.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Redirige vers la vue Index principale (Accueil)
        /// </summary>
        /// <returns>La vue Index</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Redirige vers la vue Contact
        /// </summary>
        /// <returns>La vue Contact</returns>
        public ActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// Redirige vers la vue FAQ
        /// </summary>
        /// <returns>La vue FAQ</returns>
        public ActionResult FAQ()
        {
            return View();
        }
    }
}