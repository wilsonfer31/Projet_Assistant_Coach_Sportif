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
        public virtual Ville Ville { get; set; }
        [ScriptIgnore(ApplyToOverrides=true)]
        public virtual List<GroupeChat> GroupeChats { get; set; } = new List<GroupeChat>();
        [ScriptIgnore(ApplyToOverrides=true)]
        public virtual List<Cours> CoursSuivis { get; set; } = new List<Cours>();
        public bool Admin { get; set; } = false;
        [Display(Name = "UserPhoto")]
        public string ProfilePicture { get; set; }

    }
}
