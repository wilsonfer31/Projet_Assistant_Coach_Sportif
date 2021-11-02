using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class Activite : IllustratedEntity
    {
        public Activite(int id, string nom, string descritption, string imageUrl) : base(id, nom, descritption, imageUrl)
        {
        }
    }
}
