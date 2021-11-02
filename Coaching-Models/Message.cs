using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class Message : BaseEntity
    {
        public Utilisateur Utilisateur { get; set; }
        public DateTime Date { get; set; }
        public string MessageText { get; set; }
    }
}
