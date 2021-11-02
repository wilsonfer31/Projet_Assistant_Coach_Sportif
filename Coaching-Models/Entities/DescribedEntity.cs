using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class DescribedEntity : NamedEntity
    {
        public DescribedEntity(int id, string nom, string descritption) : base(id, nom)
        {
            Descritption = descritption;
        }

        public string Descritption { get; set; }
    }
}
