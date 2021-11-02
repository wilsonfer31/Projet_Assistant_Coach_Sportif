using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class Coach : BaseEntity
    {
        public List<Cours> CoursDispenses { get; set; }
        public Utilisateur Utilisateur { get; set; }
    }
}
