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
    /// <summary>
    /// Controlleur pour gérer les connexions aux comptes étudiants et admins
    /// </summary>
    public class AccountController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retourne la vue précédant la connexion
        /// </summary>
        /// <param name="returnUrl">L'url la vue précédent le Login</param>
        /// <returns>La vue précédent le Login</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Permet à un utilisateur existant, admin ou étudiant, de se connecter au système
        /// </summary>
        /// <param name="model">Le model LoginViewModel</param>
        /// <param name="returnUrl">L'url la vue précédent le Login</param>
        /// <returns>La vue précédent le Login</returns>
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

        /// <summary>
        /// Redirige un utilisateur à l'adresse précédent l'action
        /// </summary>
        /// <param name="returnUrl">L'url précédant l'action</param>
        /// <returns>La page d'accueil</returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Vérifie si l'administrateur existe avant la connexion
        /// </summary>
        /// <param name="email">L'email de l'administrateur</param>
        /// <param name="password">Le mot de passe de l'administrateur</param>
        /// <returns>La confirmation ou l'infirmation de validité</returns>
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

        /// <summary>
        /// Vérifie si l'étudiant existe avant la connexion
        /// </summary>
        /// <param name="email">L'email de l'administrateur</param>
        /// <param name="password">Le mot de passe de l'administrateur</param>
        /// <returns>La confirmation ou l'infirmation de validité</returns>
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

        /// <summary>
        /// Déconnecte un utilisateur du système. Invalide son cookie de connexion. 
        /// </summary>
        /// <returns>La page d'accueil</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}