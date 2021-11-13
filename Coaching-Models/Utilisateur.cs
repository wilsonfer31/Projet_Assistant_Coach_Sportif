using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace Coaching_Models
{
    public class Utilisateur : NamedEntity
    {

        [Required]
        public string Pseudo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }
        public string Prenom { get; set; }
        public string Tel { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        public string Adresse { get; set; }
        [Required]
        public Ville Ville { get; set; }
        [ScriptIgnore]
        public List<GroupeChat> GroupeChats { get; set; } = new List<GroupeChat>();
        public List<Cours> CoursSuivis { get; set; } = new List<Cours>();
        public bool Admin { get; set; } = false;

        public void UpdateFromForm(EditForm ef)
        {
            Pseudo = ef.Pseudo;
            Nom = ef.Nom;
            Prenom = ef.Prenom;
            Tel = ef.Tel;
            Mail = ef.Mail;
            Adresse = ef.Adresse;
        }
    }
}
