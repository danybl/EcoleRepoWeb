using EcoleWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoleWeb.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            ViewBag.Message = "La liste des cours offerts";

            CourseViewModel loClassModels = new CourseViewModel();
            List<CourseViewModel> listeCours = new List<CourseViewModel>();

            loClassModels.ID = 1;
            loClassModels.nom = "Anglais";
            loClassModels.dureeTotale = 60;
            listeCours.Add(loClassModels);

            loClassModels = new CourseViewModel();
            loClassModels.ID = 2;
            loClassModels.nom = "Math";
            loClassModels.dureeTotale = 45;
            listeCours.Add(loClassModels);

            loClassModels = new CourseViewModel();
            loClassModels.ID = 3;
            loClassModels.nom = "Educ";
            loClassModels.dureeTotale = 45;
            listeCours.Add(loClassModels);

            loClassModels = new CourseViewModel();
            loClassModels.ID = 4;
            loClassModels.nom = "Histoire";
            loClassModels.dureeTotale = 60;
            listeCours.Add(loClassModels);


            var donnee = listeCours;
            return View(donnee);
        }

        public ActionResult Course()
        {
            ViewBag.Message = "La liste des cours offerts";

            CourseRegistrationViewModel loCourseRegistrationViewModel = new CourseRegistrationViewModel();
            List<CourseViewModel> listeCours = new List<CourseViewModel>();

            loCourseRegistrationViewModel.nom = "Anglais";
            loCourseRegistrationViewModel.dureeTotale = 60;

            var donnee = listeCours;
            return View(donnee);
        }
    }
}