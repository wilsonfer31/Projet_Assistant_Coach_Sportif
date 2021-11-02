using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class Coach : BaseEntity
    {

        public Coach(Adherent adherent, Cours cours, int id) : base(id)
        {
            Adherent = adherent;
            CoursDispenses = new List<Cours>{cours};
        }

        public List<Cours> CoursDispenses { get; set; }
        public Adherent Adherent { get; set; }
    }
}
