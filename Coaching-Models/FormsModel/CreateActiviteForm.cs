using System.Web;

namespace Coaching_Models
{
    public class CreateActiviteForm : DescribedEntity
    {
        public int Categorie { get; set; }

        public HttpPostedFileBase ImageUrl { get; set; }
    }
}
