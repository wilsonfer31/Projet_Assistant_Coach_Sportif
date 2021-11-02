using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class Cours : BaseEntity
    {
        public Activite Activite { get; set; }
        public Coach Coach { get; set; }
        public Adresse Adresse { get; set; }
        public List<Adherent> Adherents { get; set; }
        public DateTime DateCours { get; set; }
    }
}
