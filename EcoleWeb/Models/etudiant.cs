//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace EcoleWeb.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class etudiant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public etudiant()
        {
            this.inscriptioncours = new HashSet<inscriptioncour>();
        }

        [Display(Name = "Id �tudiant")]
        public int idEtudiant { get; set; }
        [Display(Name = "Nom")]
        public string nom { get; set; }
        [Display(Name = "Prenom")]
        public string prenom { get; set; }
        [Display(Name = "Adresse")]
        public string adresse { get; set; }
        [Display(Name = "T�l�phone")]
        public string telephone { get; set; }
        [Display(Name = "Courriel")]
        public string courriel { get; set; }
        [Display(Name = "Date d'inscription")]
        public System.DateTime dateInscription { get; set; }
        [Display(Name = "Mot de passe")]
        public string motDePasse { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inscriptioncour> inscriptioncours { get; set; }
    }
}
