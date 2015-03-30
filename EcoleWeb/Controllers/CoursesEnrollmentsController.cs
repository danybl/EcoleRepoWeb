﻿using System;
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

        // POST: CoursesEnrollments/Delete/5
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
    }
}