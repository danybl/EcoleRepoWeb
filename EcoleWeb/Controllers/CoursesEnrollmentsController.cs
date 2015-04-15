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
    [Authorize(Users = "scott@lacarte.com")]
    public class CoursesEnrollmentsController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: CoursesEnrollments
        public ActionResult Index()
        {
            var inscriptioncours = db.inscriptioncours.Include(i => i.cour).Include(i => i.etudiant);
            return View(inscriptioncours.ToList());
        }

        // GET: CoursesEnrollments/Details/5
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
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.idcour = new SelectList(db.cours, "idcour", "nom");
            ViewBag.idEtudiant = new SelectList(db.etudiants, "idEtudiant", "nom");
            return View();
        }

        // POST: CoursesEnrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        //
        // POST: /CoursesEnrollments/Confirm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(etudiant model, int? idCours)
        {
            if (ModelState.IsValid)
            {
                if(idCours == null){
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var inscriptions = from e in db.inscriptioncours
                                where e.idEtudiant == model.idEtudiant && e.idcour == idCours
                                select e;

                if (inscriptions.Count<inscriptioncour>() == 0)
                {
                    inscriptioncour nouvInscription = new inscriptioncour();
                    nouvInscription.idEtudiant = model.idEtudiant;
                    nouvInscription.idcour = idCours;
                    nouvInscription.paiments = 10.00;
                    nouvInscription.dateInscription = DateTime.Now;
                    db.inscriptioncours.Add(nouvInscription);
                    return RedirectToAction("IndexStudents", "Courses", new { message = "Inscription réussie" });
                }
                else
                {
                    return RedirectToAction("IndexStudents", "Courses", new { message = "Inscription annulée, vous êtes déjà inscrit(e) à ce cours" });
                }

            }

            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return View(model);
        }
    }
}
