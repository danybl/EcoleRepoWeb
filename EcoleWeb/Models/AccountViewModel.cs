using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcoleWeb.Models
{

    // Ce modele va etre utilise dans la Vue Login pour la connexion utilisateur (étudiant ou administrateur)
    public class LoginViewModel
    {
        // [Required] specifie que le champ Email doit être requis pour la connection
        // [Display(Name)] permet de affiche un label pour le email
        // [EmailAddress] permet de valider une addresse courriel
        [Required]
        [Display(Name = "Courrier électronique")]
        [EmailAddress]
        public string Email { get; set; }

        // [DataType(DataType.Password)] specifie l'attribut comme étant de type Password
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Mémoriser le mot de passe ?")]
        public bool RememberMe { get; set; }
    }
}
