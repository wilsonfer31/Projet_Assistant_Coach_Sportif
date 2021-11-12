using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Coaching_Models
{
    public class Cours : BaseEntity
    {
        public Activite Activite { get; set; }
        [ScriptIgnore]
        public Coach Coach { get; set; }
        public Ville Adresse { get; set; }
        public List<Utilisateur> Adherents { get; set; } = new List<Utilisateur>();
        public DateTime DateCours { get; set; }
    }
}
