using System.ComponentModel.DataAnnotations;

namespace Coaching_Models
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
