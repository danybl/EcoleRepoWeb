using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcoleWeb.Models;
using Microsoft.AspNet.Identity;

namespace EcoleWeb.Controllers
{
    /// <summary>
    /// Controlleur pour gérer les inscriptions
    /// </summary>
    [Authorize(Users = "admin@lacarte.com")]
    public class CoursesEnrollmentsController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: CoursesEnrollments
        /// <summary>
        /// Retourne liste des inscriptions
        /// </summary>
        /// <returns>La vue contenant la liste des insctiptions</returns>
        public ActionResult Index()
        {
            var inscriptioncours = db.inscriptioncours.Include(i => i.cour).Include(i => i.etudiant);
            return View(inscriptioncours.ToList());
        }

        // GET: CoursesEnrollments/Details/5
        /// <summary>
        /// Retourne les détails d'une inscription
        /// </summary>
        /// <param name="id">L'id de l'inscription</param>
        /// <returns>La vue Details</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inscriptioncour inscriptioncour = db.inscriptioncours.Find(id);
            if (inscriptioncour == null)
            {
                return HttpNotFound();
            }
            return View(inscriptioncour);
        }

        // GET: CoursesEnrollments/Create
        /// <summary>
        /// Retourne la vue pour créer une inscription
        /// </summary>
        /// <returns>La vue Create</returns>
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.idcour = new SelectList(db.cours, "idcour", "nom");
            ViewBag.idEtudiant = new SelectList(db.etudiants, "idEtudiant", "nom");
            return View();
        }

        // POST: CoursesEnrollments/Create
        /// <summary>
        /// Crée une inscription
        /// </summary>
        /// <param name="inscriptioncour">L'entité Inscription</param>
        /// <returns>La vue Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idInscription,idcour,idEtudiant,dateInscription,paiments")] inscriptioncour inscriptioncour)
        {
            if (ModelState.IsValid)
            {
                db.inscriptioncours.Add(inscriptioncour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idcour = new SelectList(db.cours, "idcour", "nom", inscriptioncour.idcour);
            ViewBag.idEtudiant = new SelectList(db.etudiants, "idEtudiant", "nom", inscriptioncour.idEtudiant);
            return View(inscriptioncour);
        }

        // GET: CoursesEnrollments/Edit/5
        /// <summary>
        /// Retourne la vue pour éditer une inscription
        /// </summary>
        /// <param name="id">L'id de l'inscription à éditer</param>
        /// <returns>La vue Edit</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inscriptioncour inscriptioncour = db.inscriptioncours.Find(id);
            if (inscriptioncour == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcour = new SelectList(db.cours, "idcour", "nom", inscriptioncour.idcour);
            ViewBag.idEtudiant = new SelectList(db.etudiants, "idEtudiant", "nom", inscriptioncour.idEtudiant);
            return View(inscriptioncour);
        }

        // POST: CoursesEnrollments/Edit/5
        /// <summary>
        /// Édite une inscription
        /// </summary>
        /// <param name="inscriptioncour">L'id de l'inscription à modifier</param>
        /// <returns>La vue Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idInscription,idcour,idEtudiant,dateInscription,paiments")] inscriptioncour inscriptioncour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscriptioncour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcour = new SelectList(db.cours, "idcour", "nom", inscriptioncour.idcour);
            ViewBag.idEtudiant = new SelectList(db.etudiants, "idEtudiant", "nom", inscriptioncour.idEtudiant);
            return View(inscriptioncour);
        }

        // GET: CoursesEnrollments/Delete/5
        /// <summary>
        /// Retourne la vue pour supprimer une inscription
        /// </summary>
        /// <param name="id">L'id de l'inscription à supprimer</param>
        /// <returns>La vue Delete</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inscriptioncour inscriptioncour = db.inscriptioncours.Find(id);
            if (inscriptioncour == null)
            {
                return HttpNotFound();
            }
            return View(inscriptioncour);
        }

        // POST: CoursesEnrollments/Delete
        /// <summary>
        /// Supprime une inscription
        /// </summary>
        /// <param name="id">L'id de l'inscription à supprimer</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            inscriptioncour inscriptioncour = db.inscriptioncours.Find(id);
            db.inscriptioncours.Remove(inscriptioncour);
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
        // GET: CoursesEnrollments/Enroll
        /// <summary>
        /// Permet à un étudiant de s'inscrire
        /// </summary>
        /// <param name="idCour">L'id du cours auquel l'étudiant veut s'inscrire</param>
        /// <returns>La vue IndexStudent</returns>
        [AllowAnonymous]
        public ActionResult Enroll(int? idCour)
        {
            if (idCour == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var etudiants = from e in db.etudiants
                               where e.courriel == User.Identity.Name
                               select e;

                etudiant etudiant = etudiants.First();

                var inscriptions = from i in db.inscriptioncours
                                   where i.idEtudiant == etudiant.idEtudiant && i.idcour == idCour
                                   select i;

                if (inscriptions.Count<inscriptioncour>() == 0)
                {
                    inscriptioncour nouvInscription = new inscriptioncour();
                    nouvInscription.idEtudiant = etudiant.idEtudiant;
                    nouvInscription.idcour = idCour;
                    nouvInscription.paiments = 10.00;
                    nouvInscription.dateInscription = DateTime.Now;
                    db.inscriptioncours.Add(nouvInscription);
                    db.SaveChanges();
                    return RedirectToAction("IndexStudent", "Courses", new { message = "Inscription réussie" });
                }
                else
                {
                    return RedirectToAction("IndexStudent", "Courses", new { message = "Vous êtes déjà inscrit(e) à ce cours" });
                }

            }

            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return View("IndexStudents");
        }

    }
}
