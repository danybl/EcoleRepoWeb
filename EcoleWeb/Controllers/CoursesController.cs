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
    /// Controlleur pour gérer les cours
    /// </summary>
    [Authorize(Users = "admin@lacarte.com")]
    public class CoursesController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        /// <summary>
        /// Retourne la liste des cours avec toutes les informations pour l'administrateur
        /// </summary>
        /// <returns>La vue contenant la liste des cours</returns>
        public ActionResult Index()
        {
            var cours = db.cours.Include(c => c.professeur);
            return View(cours.ToList());
        }

        /// GET
        /// <summary>
        /// Retourne la liste des cours avec certaines informations pour l'étudiant
        /// </summary>
        /// <param name="message">Message à afficher au-dessus de la liste (confirmation ou  d'ins)</param>
        /// <returns>La vue contenant la liste des cours</returns>
        [AllowAnonymous]
        public ActionResult IndexStudent(string message)
        {
            ViewData["message"] = message;
            var cours = db.cours.Include(c => c.professeur);
            return View(cours.ToList());
        }

        /// GET
        /// <summary>
        /// Retourne les détails d'un cours
        /// </summary>
        /// <param name="id">L'id du cours à afficher</param>
        /// <returns>La vue contenant les détails du cours</returns>
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cour cour = db.cours.Find(id);
            if (cour == null)
            {
                return HttpNotFound();
            }
            return View(cour);
        }

        ///GET
        /// <summary>
        /// Retourne la vue pour créer un cours
        /// </summary>
        /// <returns>La vue Create</returns>
        public ActionResult Create()
        {
            ViewBag.idProfesseur = new SelectList(db.professeurs, "idProfesseur", "nom");
            return View();
        }
        
        /// POST
        /// <summary>
        /// Crée un nouveau cours
        /// </summary>
        /// <param name="cour">L'entité Cours à créer</param>
        /// <returns>La vue du nouveau cours créé</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idcour,nom,idProfesseur,dureeTotale,dureeParJour,prix,jour,heures,dateLimite,nbInscriptionMin,nbInscriptionMax,dateDebut,dateFin,etatcour")] cour cour)
        {
            if (ModelState.IsValid)
            {
                db.cours.Add(cour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProfesseur = new SelectList(db.professeurs, "idProfesseur", "nom", cour.idProfesseur);
            return View(cour);
        }

        /// GET
        /// <summary>
        /// Retourne la vue pour éditer un cours
        /// </summary>
        /// <param name="id">L'id du cours à modifier</param>
        /// <returns>La vue du Edit</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cour cour = db.cours.Find(id);
            if (cour == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProfesseur = new SelectList(db.professeurs, "idProfesseur", "nom", cour.idProfesseur);
            return View(cour);
        }

        /// POST
        /// <summary>
        /// Édite un cours
        /// </summary>
        /// <param name="id">L'id du cours à éditer</param>
        /// <returns>Retourne la vue du cours dont les informations ont été modifiée</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idcour,nom,idProfesseur,dureeTotale,dureeParJour,prix,jour,heures,dateLimite,nbInscriptionMin,nbInscriptionMax,dateDebut,dateFin,etatcour")] cour cour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProfesseur = new SelectList(db.professeurs, "idProfesseur", "nom", cour.idProfesseur);
            return View(cour);
        }

        /// GET: Courses/Delete/5
        /// <summary>
        /// Retourne la vue pour supprimer un cours 
        /// </summary>
        /// <param name="id">L'id du cours à supprimer</param>
        /// <returns>La vue Delete</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cour cour = db.cours.Find(id);
            if (cour == null)
            {
                return HttpNotFound();
            }
            return View(cour);
        }

        // POST: Courses/Delete/5
        /// <summary>
        /// Supprime un cours
        /// </summary>
        /// <param name="id">L'id du cours à supprimer</param>
        /// <returns>La vue Index</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cour cour = db.cours.Find(id);
            db.cours.Remove(cour);
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
