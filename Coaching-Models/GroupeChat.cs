using System.Collections.Generic;

namespace Coaching_Models
{
    public class GroupeChat : NamedEntity
    {
        public virtual List<Utilisateur> Membres { get; set; } = new List<Utilisateur>();
        public virtual List<Message> ChatMessages { get; set; } = new List<Message>();
    }
}
