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
    [Authorize(Users = "scott@lacarte.com")]
    public class CoursesController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: Courses
        public ActionResult Index()
        {
            var cours = db.cours.Include(c => c.professeur);
            return View(cours.ToList());
        }

        [AllowAnonymous]
        public ActionResult IndexStudent(string message)
        {
            ViewData["message"] = message;
            var cours = db.cours.Include(c => c.professeur);
            return View(cours.ToList());
        }

        // GET: Courses/Details/5
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

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.idProfesseur = new SelectList(db.professeurs, "idProfesseur", "nom");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Courses/Edit/5
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

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Courses/Delete/5
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
