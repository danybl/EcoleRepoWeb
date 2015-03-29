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
    public class TeachersController : Controller
    {
        private ecoleEntities db = new ecoleEntities();

        // GET: Teachers
        public ActionResult Index()
        {
            return View(db.professeurs.ToList());
        }

        // GET: Teachers/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
