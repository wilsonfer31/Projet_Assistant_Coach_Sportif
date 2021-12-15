using Coaching_Models;
using CoachSportif.Filters;
using CoachSportif.Models.ViewModel;
using CoachSportif.Tools;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    public class VilleController : BaseController<Ville>
    {
        // GET: Activites
        public override async Task<ActionResult> Index()
        {
            var l = (await db.GetAllAsync()).Select(v => new ViewModelVille(v));
            return View(l);
        }
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

        [AdminFilters]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ville o)
        {
            if (ModelState.IsValid)
            {
                await db.AddAsync(o);
                return RedirectToAction("Index");
            }
            return View(o);
        }
    }
}
