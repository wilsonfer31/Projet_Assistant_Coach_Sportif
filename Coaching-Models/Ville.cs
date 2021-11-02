using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class Ville : NamedEntity
    {

        public Ville(string nom, int cP, int id) : base(id,nom)
        {
            CP = cP;
        }
        public int CP { get; set; }
    }
}
