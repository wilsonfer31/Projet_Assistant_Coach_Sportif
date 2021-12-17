using CoachSportif.DAO;
using CoachSportif.Filters;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    public abstract class BaseController<T> : Controller
    {
        protected readonly GenericDao<T> db = new GenericDao<T>();

        public virtual async Task<ActionResult> Index()
        {
            return View(await db.GetAllAsync());
        } //GET
        public virtual async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T obj = await db.FindByIdAsync(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        } //GET
        public virtual ActionResult Create()
        {
            return View();
        } //GET
        public virtual async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T obj = await db.FindByIdAsync(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        } //GET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(T o)
        {
            if (ModelState.IsValid)
            {
                await db.UpdateAsync(o);
                return RedirectToAction("Index");
            }
            return View(o);
        } //GET
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T obj = await db.FindByIdAsync(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [LoginFilters]
        public async Task<ActionResult> DeleteConfirmedAsync(int id)
        {
            await db.RemoveAsync(await db.FindByIdAsync(id));
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
