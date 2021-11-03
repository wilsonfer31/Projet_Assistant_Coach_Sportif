using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
