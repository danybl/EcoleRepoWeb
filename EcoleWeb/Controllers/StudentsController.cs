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
    [Authorize(Users = "admin@lacarte.com")]
    public class StudentsController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: Students
        /// <summary>
        /// Retourne la vue qui affiche les étudiants
        /// </summary>
        public ActionResult Index()
        {
            return View(db.etudiants.ToList());
        }

        // GET: Students/Details/5
        /// <summary>
        /// Retourne la vue qui affiche les details d'un étudiant spécifique
        /// </summary>
        /// <param name="id">Le id de l'étudiant</param>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            etudiant etudiant = db.etudiants.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // GET: Students/Create
        /// <summary>
        /// Retourne la vue pour créer un nouveau étudiant
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Méthode qui permet de créer un étudiant dans la base de données
        /// </summary>
        /// <param name="etudiant"> L'étudiant à créer </param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                db.etudiants.Add(etudiant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(etudiant);
        }

        // GET: Students/Edit/5
        /// <summary>
        /// Retourne la vue pour editer un étudiant
        /// </summary>
        /// <param name="id"> Le id de l'étudiant à modifier</param>
        /// <returns>La vue Edit</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            etudiant etudiant = db.etudiants.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Methode qui permet de modifier les données d'un étudiant spécifique
        /// </summary>
        /// <param name="etudiant"> L'étudiant à modifier</param>
        /// <returns>La vue Edit</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEtudiant,nom,prenom,adresse,telephone,courriel,dateInscription,motDePasse")] etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(etudiant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(etudiant);
        }

        // GET: Students/Delete/5
        /// <summary>
        /// Retourne la vue pour supprimer un étudiant
        /// </summary>
        /// <param name="id"> Le id de l'étudiant à modifier</param>
        /// <returns>La vue Delete</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            etudiant etudiant = db.etudiants.Find(id);
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // POST: Students/Delete/5
        /// <summary>
        /// Méthode qui permet de supprimer un étudiant
        /// </summary>
        /// <param name="id">Le id de l'étudiant à supprimer</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            etudiant etudiant = db.etudiants.Find(id);
            db.etudiants.Remove(etudiant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //
        // GET: /Account/Register
        /// <summary>
        /// Retorne la vue pour inscrire un étudiant
        /// </summary>
        /// <returns>La vue Register</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        /// <summary>
        /// Méthode qui permet d'inscrire un étudiant dans la base de données
        /// </summary>
        /// <param name="model">Le modele à utiliser pour faire l'inscription</param>
        /// <returns>La vue Register</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var etudiants = from e in db.etudiants
                                where e.courriel.Equals(model.Email)
                                select e;

                if (etudiants.Count<etudiant>() == 0)
                {
                    etudiant nouvEtudiant = new etudiant();
                    nouvEtudiant.courriel = model.Email;
                    nouvEtudiant.prenom = model.FirstName;
                    nouvEtudiant.nom = model.LastName;
                    nouvEtudiant.adresse = model.Address;
                    nouvEtudiant.telephone = model.Phone;
                    nouvEtudiant.motDePasse = model.Password;
                    nouvEtudiant.dateInscription = DateTime.Now;

                    db.etudiants.Add(nouvEtudiant);
                    db.SaveChanges();
                }

                return RedirectToAction("Login", "Account");

            }

            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return View(model);
        }

        
    }
}
