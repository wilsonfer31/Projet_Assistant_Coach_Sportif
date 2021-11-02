using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class Adresse : BaseEntity
    {

        public Adresse(int numRue, string rue, string ville, int cP, int id) : base(id)
        {
            NumRue = numRue;
            Rue = rue;
            Ville = ville;
            CP = cP;
        }

        public int NumRue { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        public int CP { get; set; }
    }
}
