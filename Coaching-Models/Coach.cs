using System.Collections.Generic;

namespace Coaching_Models
{
    public class Coach : BaseEntity
    {
        public virtual List<Cours> CoursDispenses { get; set; } = new List<Cours>();
        public virtual Utilisateur Utilisateur { get; set; }
    }
}
