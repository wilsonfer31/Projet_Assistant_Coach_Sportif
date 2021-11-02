using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class IllustratedEntity : DescribedEntity
    {
        public IllustratedEntity(int id, string nom, string descritption, string imageUrl) : base(id, nom, descritption)
        {
            ImageUrl = imageUrl;
        }

        public string ImageUrl { get; set; }
    }
}
