using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class Utilisateur : NamedEntity
    {
        public Utilisateur(string pseudo, string motDePasse, string prenom, string tel, Adresse adresse, int id, string nom) : base(id, nom)
        {
            Pseudo = pseudo;
            MotDePasse = motDePasse;
            Prenom = prenom;
            Tel = tel;
            Adresse = adresse;
            GroupeChats = new List<GroupeChat>();
            CoursSuivis = new List<Cours>();
        }

        public string Pseudo { get; set; }
        public string MotDePasse { get; set; }
        public string Prenom { get; set; }
        public string Tel { get; set; }
        public Adresse Adresse { get; set; }
        public List<GroupeChat> GroupeChats { get; set; }
        public List<Cours> CoursSuivis { get; set; }
    }
}
