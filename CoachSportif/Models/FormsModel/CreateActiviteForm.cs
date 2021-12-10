using Coaching_Models;
using CoachSportif.Tools;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace CoachSportif.Models.FormsModel
{
    public class CreateActiviteForm : DescribedEntity
    {
        public CreateActiviteForm()
        {
            Categories = Categories.InitCategories();
        }
        public int Categorie { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public HttpPostedFileBase ImageUrl { get; set; }
    }
}
