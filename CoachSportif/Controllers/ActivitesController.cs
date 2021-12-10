using Coaching_Models;
using CoachSportif.DAO;
using CoachSportif.Filters;
using CoachSportif.Models;
using CoachSportif.Models.FormsModel;
using CoachSportif.Tools;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
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
                Activite act = await db.AddAsync(activite.GetObject());
                if (act != default)
                {
                    activite.ImageUrl.SaveAs(Server.MapPath("~/Content/images/Activites/") + act.Id + act.ImageUrl);
                }
                return RedirectToAction("Index");
            }
            return View(activite);
        }
    }
}
