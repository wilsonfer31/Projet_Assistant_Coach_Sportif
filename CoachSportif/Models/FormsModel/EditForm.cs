using Coaching_Models;
using CoachSportif.Tools;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace CoachSportif.Models.FormsModel
{
    public class EditForm : NamedEntity
    {
        public EditForm() 
        {
            Villes = Villes.InitVilles();
        }
        [Required]
        public string Pseudo { get; set; }
        public string Prenom { get; set; }
        public string Tel { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        public string Adresse { get; set; }
        [Required]
        public int Ville { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }

        public string Image { get; set; }
        public IEnumerable<SelectListItem> Villes { get; set; }

        public EditForm(Utilisateur utilisateur)
        {
            Id = utilisateur.Id;
            Pseudo = utilisateur.Pseudo;
            Nom = utilisateur.Nom;
            Prenom = utilisateur.Prenom;
            Tel = utilisateur.Tel;
            Mail = utilisateur.Mail;
            Adresse = utilisateur.Adresse;
            Image = utilisateur.ProfilePicture;
            Ville = utilisateur.Ville.Id;
            Villes = Villes.InitVilles();
        }
    }
}
