using System;

namespace Coaching_Models
{
    public class Message : BaseEntity
    {
        public Utilisateur Utilisateur { get; set; }
        public DateTime Date { get; set; }
        public string MessageText { get; set; }
    }
}
