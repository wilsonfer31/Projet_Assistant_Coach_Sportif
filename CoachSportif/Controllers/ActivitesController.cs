using Coaching_Models;
using CoachSportif.Filters;
using CoachSportif.Models.FormsModel;
using CoachSportif.Tools;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    [AdminFilters]
    public class ActivitesController : BaseController<Activite>
    {
        // GET: Activites/Create
        public override ActionResult Create()
        {
            return View(new CreateActiviteForm());
        }

        // POST: Activites/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateActiviteForm activite)
        {
            if (ModelState.IsValid)
            {
                Activite act = await db.AddAsync(activite.GetObject(db.Getcontext()));
                if (act != default && activite.ImageUrl != null)
                {
                    activite.ImageUrl.SaveAs(Server.MapPath("~/Content/images/Activites/") + act.Id + act.ImageUrl);
                }
                return RedirectToAction("Index");
            }
            return View(activite);
        }
    }
}
