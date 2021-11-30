using Coaching_Models;
using Coaching_Models.ViewModel;
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
    public class VilleController : Controller
    {
        private readonly MyContext db = new MyContext();

        // GET: Ville
        public ActionResult Index()
        {
            int userId = (int)Session["user_id"];
            int VilleId = db.Utilisateurs.AsNoTracking().Include(u => u.Ville).SingleOrDefault(u => u.Id == userId).Ville.Id;
            return View(new ViewModelVille { Cours = db.Cours.Include(c => c.Adresse).Where(c => c.Adresse.Id == VilleId).Include(c => c.Activite.Categorie).Include(c => c.Coach.Utilisateur), Coaches = db.Coaches.Include(c => c.Utilisateur.Ville).Where(c => c.Utilisateur.Ville.Id == VilleId) });
        }

        // GET: Ville/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ville ville = db.Villes.Find(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(ville);
        }

        // GET: Ville/Create
        [AdminFilters]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ville/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminFilters]
        public ActionResult Create([Bind(Include = "Id,CP,Nom")] Ville ville)
        {
            if (ModelState.IsValid)
            {
                db.Villes.Add(ville);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ville);
        }

        // GET: Ville/Edit/5
        [AdminFilters]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ville ville = db.Villes.Find(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(ville);
        }

        // POST: Ville/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminFilters]
        public ActionResult Edit([Bind(Include = "Id,CP,Nom")] Ville ville)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ville).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ville);
        }

        // GET: Ville/Delete/5
        [AdminFilters]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ville ville = db.Villes.Find(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(ville);
        }

        // POST: Ville/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AdminFilters]
        public ActionResult DeleteConfirmed(int id)
        {
            Ville ville = db.Villes.Find(id);
            db.Villes.Remove(ville);
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

        // GET: Utilisateurs
        [AdminFilters]
        public ActionResult ListeVille()
        {
            return View(db.Villes);
        }


    }
}
