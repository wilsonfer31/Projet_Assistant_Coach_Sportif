using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class NamedEntity : BaseEntity
    {
        public NamedEntity(int id, string nom) : base(id)
        {
            Nom = nom;
        }

        public string Nom { get; set; }
    }
}
