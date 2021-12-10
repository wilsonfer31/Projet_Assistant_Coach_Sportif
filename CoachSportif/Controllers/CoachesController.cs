using Coaching_Models;
using CoachSportif.Filters;
using CoachSportif.Models;
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
    public class CoachesController : BaseController<Coach>
    {
        // GET: Coaches/Create
        [AdminFilters]
        public override ActionResult Create()
        {
            return View(db.GetUserToAppointCoach());
        }

        [AdminFilters]
        public async Task<ActionResult> CreateCoach(int? id)
        {
            if (id.HasValue)
            {
                await db.AddAsync(db.UserToCoach(id.Value));
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Something Went Rong";
            return RedirectToAction("Create");
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CoachFilters]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await db.RemoveAsync(db.DeleteCoach(id));
            return RedirectToAction("Index");
        }
    }
}
