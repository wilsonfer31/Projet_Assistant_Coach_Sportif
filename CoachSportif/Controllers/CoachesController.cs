using Coaching_Models;
using CoachSportif.Filters;
using CoachSportif.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    [LoginFilters]
    public class CoachesController : Controller
    {
        private readonly MyContext db = new MyContext();

        // GET: Coach
        public ActionResult Index()
        {
            return View(db.Coaches.Include(c => c.Utilisateur.Ville).Include(c => c.CoursDispenses.Select(cr => cr.Activite.Categorie)));
        }

        // GET: Coaches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Include(c => c.CoursDispenses.Select(cr => cr.Activite.Categorie)).Include(c => c.Utilisateur).SingleOrDefault(c=>c.Id==id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // GET: Coaches/Create
        [AdminFilters]
        public ActionResult Create()
        {
            return View(db.Utilisateurs.Except(db.Coaches.Include(c => c.Utilisateur).Select(c => c.Utilisateur)).Include(u => u.Ville));
        }

        [AdminFilters]
        public ActionResult CreateCoach(int? id)
        {
            if (id.HasValue)
            {
                db.Coaches.Add(new Coach { Utilisateur = db.Utilisateurs.Find(id.Value) });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Something Went Rong";
            return RedirectToAction("Create");
        }

        // GET: Coaches/Edit/5
        [CoachFilters]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CoachFilters]
        public ActionResult Edit([Bind(Include = "Id")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coach);
        }

        // GET: Coaches/Delete/5
        [CoachFilters]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Include(c => c.Utilisateur).SingleOrDefault(c => c.Id == id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CoachFilters]
        public ActionResult DeleteConfirmed(int id)
        {
            Coach coach = db.Coaches.Include(c => c.CoursDispenses).SingleOrDefault(c => c.Id == id);
            if (coach.CoursDispenses.Count() > 0) coach.CoursDispenses.ForEach(c => db.Cours.Remove(c));
            db.Coaches.Remove(coach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AdminFilters]
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
