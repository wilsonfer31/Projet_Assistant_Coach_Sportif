using Coaching_Models;
using CoachSportif.DAO;
using CoachSportif.Filters;
using CoachSportif.Models;
using CoachSportif.Models.ViewModel;
using CoachSportif.Tools;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    [LoginFilters]
    public class VilleController : BaseController<Ville>
    {
        // GET: Ville/Details/5
        public override async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View(new ViewModelVille(db.GetUserVilleId((int)Session["user_id"])));
            }
            Ville ville = await db.FindByIdAsync(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(new ViewModelVille(ville.Id));
        }
    }
}
