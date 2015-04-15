using EcoleWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EcoleWeb.Controllers
{
    /// <summary>
    /// Controlleur pour gérer les pages administrateurs
    /// </summary>
    [Authorize(Users = "admin@lacarte.com")]
    public class AdminController : Controller
    {        
        /// <summary>
        /// Retourne la vue Home de l'administarteur
        /// </summary>
        /// <returns>La vue Home</returns>
        public ActionResult Home()
        {
            return View();
        }
        /// <summary>
        /// Retourne la vue FAQ de l'administarteur
        /// </summary>
        /// <returns>La vue FAQ</returns>
        public ActionResult FAQ()
        {
            return View();
        }

        
  
    }
}