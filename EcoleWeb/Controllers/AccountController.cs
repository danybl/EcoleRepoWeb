using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcoleWeb.Models;
using System.Threading.Tasks;
using System.Web.Security;

namespace EcoleWeb.Controllers
{
    public class AccountController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            if (IsValidAdmin(model.Email, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Email, true);
                return RedirectToAction("Home", "Admin");
            }
            else if (IsValidUser(model.Email, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Email, true);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Le courriel ou le mot de passe est incorrect");
            }


            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private bool IsValidAdmin(string email, string password)
        {
            bool IsValid = false;

            var admins = from a in db.admins
                            where a.courriel.Equals(email) && a.motDePasse.Equals(password)
                            select a;
            if (admins.Count<admin>() == 1 )
            {
                admin connexionAdmin = admins.First();
                if (connexionAdmin.motDePasse == password)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }

        private bool IsValidUser(string email, string password)
        {
            bool IsValid = false;

            var etudiants = from e in db.etudiants
                            where e.courriel.Equals(email) && e.motDePasse.Equals(password)
                            select e;

            if (etudiants.Count<etudiant>() == 1)
            {
                etudiant connexionEtudiant = etudiants.First();
                if (connexionEtudiant.motDePasse == password)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}