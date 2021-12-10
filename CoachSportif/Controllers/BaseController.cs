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

        // GET: Activites
        public virtual async Task<ActionResult> Index()
        {
            return View(await db.GetAllAsync());
        }

        // GET: Activites/Details/5
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
        }
        // GET: CategorieActivites/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: CategorieActivites/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(T o)
        {
            if (ModelState.IsValid)
            {
                await db.AddAsync(o);
                return RedirectToAction("Index");
            }
            return View(o);
        }

        // GET: Activites/Edit/5
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
        }

        // POST: CategorieActivites/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
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
        }

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

        // POST: Utilisateurs/Delete/5
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
