using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class GroupeChat : NamedEntity
    {
        public List<Utilisateur> Membres { get; set; }
        public List<Message> ChatMessages { get; set; }
    }
}
