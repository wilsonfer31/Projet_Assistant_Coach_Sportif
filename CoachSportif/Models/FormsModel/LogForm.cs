using System.ComponentModel.DataAnnotations;

namespace CoachSportif.Models.FormsModel
{
    public class LogForm
    {
        [Required]
        public string Pseudo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }
    }
}
