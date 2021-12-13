using Coaching_Models;
using CoachSportif.Filters;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    [AdminFilters]
    public class CategorieActivitesController : BaseController<CategorieActivite>
    {

        // POST: CategorieActivites/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(CategorieActivite o)
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
