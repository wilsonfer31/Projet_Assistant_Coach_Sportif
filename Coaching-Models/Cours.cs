using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Coaching_Models
{
    public class Cours : BaseEntity
    {
        public virtual Activite Activite { get; set; }
        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Coach Coach { get; set; }
        public virtual Ville Adresse { get; set; }
        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual List<Utilisateur> Adherents { get; set; } = new List<Utilisateur>();
        public DateTime DateCours { get; set; }
    }
}
