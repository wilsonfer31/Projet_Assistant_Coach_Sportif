using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public List<GroupeChat> GroupeChats { get; set; }
        public List<Cours> CoursSuivis { get; set; }
    }
}
