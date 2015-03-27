using EcoleWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoleWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Classes()
        {
            ViewBag.Message = "La liste des cours offerts";

            ClassModels loClassModels = new ClassModels();
            List<ClassModels> listeCours = new List<ClassModels>();

            loClassModels.ID = 1;
            loClassModels.nom = "Anglais";
            loClassModels.dureeTotale = 60;
            listeCours.Add(loClassModels);

            loClassModels = new ClassModels();
            loClassModels.ID = 2;
            loClassModels.nom = "Math";
            loClassModels.dureeTotale = 45;
            listeCours.Add(loClassModels);

            loClassModels = new ClassModels();
            loClassModels.ID = 3;
            loClassModels.nom = "Educ";
            loClassModels.dureeTotale = 45;
            listeCours.Add(loClassModels);

            loClassModels = new ClassModels();
            loClassModels.ID = 4;
            loClassModels.nom = "Histoire";
            loClassModels.dureeTotale = 60;
            listeCours.Add(loClassModels);


            var donnee = listeCours;
            return View(donnee);
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}