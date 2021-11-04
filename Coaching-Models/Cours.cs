using System;
using System.Collections.Generic;

namespace Coaching_Models
{
    public class Cours : BaseEntity
    {
        public Activite Activite { get; set; }
        public Coach Coach { get; set; }
        public Ville Adresse { get; set; }
        public List<Utilisateur> Adherents { get; set; } = new List<Utilisateur>();
        public DateTime DateCours { get; set; }
    }
}
