using CoachSportif.Tools;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace CoachSportif.Models.FormsModel
{
    public class RegisterForm
    {
        public RegisterForm()
        {
            Villes = Villes.InitVilles();
        }

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
        public HttpPostedFileBase ProfilePicture { get; set; }
    }
}
