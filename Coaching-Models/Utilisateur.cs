using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class Utilisateur : NamedEntity
    {
        public string Pseudo { get; set; }
        public string MotDePasse { get; set; }
        public string Prenom { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }
        public string Adresse { get; set; }
        public Ville Ville { get; set; }
        public List<GroupeChat> GroupeChats { get; set; }
        public List<Cours> CoursSuivis { get; set; }
    }
}
