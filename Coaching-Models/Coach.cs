using System.Collections.Generic;

namespace Coaching_Models
{
    public class Coach : BaseEntity
    {
        public List<Cours> CoursDispenses { get; set; } = new List<Cours>();
        public Utilisateur Utilisateur { get; set; }
    }
}
