using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoleWeb.Models
{
    public class ClassModels
    {
        public Int32 ID { get; set; }
        public string nom { get; set; }
        public Int32 dureeTotale { get; set; }
        public Int32 dureeParJour { get; set; }
        public Double prix { get; set; }
        public string jour { get; set; }
        public string heures { get; set; }
        public DateTime dateLimite { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public string etatCours { get; set; }
    }


    public class CourseRegistrationViewModel
    {
        public string nom { get; set; }
        public Int32 dureeTotale { get; set; }
        public Int32 dureeParJour { get; set; }
        public Double prix { get; set; }
        public string jour { get; set; }
        public string heures { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public string etatCours { get; set; }
        public string nomEtudiant { get; set; }
        public Double depot{ get; set; }
        public Double soldeRestant { get; set; }
    }
}