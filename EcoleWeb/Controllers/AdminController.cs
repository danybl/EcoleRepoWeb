using EcoleWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EcoleWeb.Controllers
{
    public class AdminController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

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

        [HttpPost]
        public ActionResult Login(Models.admin unAdmin)
        {
            if (!ModelState.IsValid)
            {
                return View(unAdmin);
            }
            if (IsValid(unAdmin.courriel, unAdmin.motDePasse))
            {
                FormsAuthentication.SetAuthCookie(unAdmin.nom, false);
                return RedirectToAction("Home", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Le courriel ou le mot de passe est incorrect.");
            }
            return View(unAdmin);
        }

        private bool IsValid(string email, string password)
        {
            bool IsValid = false;

                var user = db.admins.FirstOrDefault(u => u.courriel == email);
                if (user != null)
                {
                    if (user.motDePasse == password)
                    {
                        IsValid = true;
                    }
                }
            return IsValid;
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }
  
    }
}