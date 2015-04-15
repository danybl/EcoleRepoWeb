using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcoleWeb.Models;

namespace EcoleWeb.Controllers
{
    /// <summary>
    /// Controlleur qui permet de gerer les professeurs
    /// </summary>
    [Authorize(Users = "admin@lacarte.com")] // N'autorise que les usagers qui respectent les contraintes spécifiées.
    public class TeachersController : Controller
    {
        private ecoleEntities db = new ecoleEntities(); // Le contexte de la base de données

        // GET: Teachers
        /// <summary>
        /// Methode qui retourne la vue index avec une liste des professeurs.
        /// </summary>
        public ActionResult Index()
        {
            return View(db.professeurs.ToList());
        }

        // GET: Teachers/Details/5
        /// <summary>
        /// Methode qui affiche les détails d'un professeur spécifique
        /// </summary>
        /// <param name="id">Le id du professeur</param>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            professeur professeur = db.professeurs.Find(id);
            if (professeur == null)
            {
                return HttpNotFound();
            }
            return View(professeur);
        }

        // GET: Teachers/Create
        /// <summary>
        /// Retourne la vue qui permet de créer un professeur
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Methode qui crée un nouveau professeur dans la base de données
        /// </summary>
        /// <param name="professeur">Le professeur a créer</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProfesseur,nom,prenom,adresse,telephone,courriel,dateEmbauche,idcourPossible")] professeur professeur)
        {
            if (ModelState.IsValid)
            {
                db.professeurs.Add(professeur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(professeur);
        }

        // GET: Teachers/Edit/5
        /// <summary>
        /// Retourne la vue pour modifier les données d'un professeur
        /// </summary>
        /// <param name="id">Le id du professeur à modifier</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            professeur professeur = db.professeurs.Find(id);
            if (professeur == null)
            {
                return HttpNotFound();
            }
            return View(professeur);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Méthode qui permet de modifier les données d'un professeur
        /// </summary>
        /// <param name="professeur">Le professeur à modifier</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProfesseur,nom,prenom,adresse,telephone,courriel,dateEmbauche,idcourPossible")] professeur professeur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professeur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(professeur);
        }

        // GET: Teachers/Delete/5
        /// <summary>
        /// Retourne la vue pour supprimer un professeur
        /// </summary>
        /// <param name="id">Le id du professeur à supprimer</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            professeur professeur = db.professeurs.Find(id);
            if (professeur == null)
            {
                return HttpNotFound();
            }
            return View(professeur);
        }

        // POST: Teachers/Delete/5
        /// <summary>
        /// Méthode qui permet de supprimer un professeur
        /// </summary>
        /// <param name="id">Le id du professeur à supprimer</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            professeur professeur = db.professeurs.Find(id);
            db.professeurs.Remove(professeur);
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
    }
}
