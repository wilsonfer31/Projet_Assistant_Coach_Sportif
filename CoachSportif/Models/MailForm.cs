using System.ComponentModel.DataAnnotations;

namespace CoachSportif.Models
{
    public class MailForm
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        [Required]

        public string Sujet { get; set; }


        [Required]
        [DataType(DataType.MultilineText)]
        public string Contenu { get; set; }
    }
}