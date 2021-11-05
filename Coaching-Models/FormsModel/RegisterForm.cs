using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Coaching_Models
{
    public class RegisterForm
    {
        [Required]
        public string Pseudo { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }
        [Required]
        public int Ville { get; set; }
        public IEnumerable<SelectListItem> Villes { get; set; }
    }
}
