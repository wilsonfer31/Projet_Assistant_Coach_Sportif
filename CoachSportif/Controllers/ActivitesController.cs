using Coaching_Models;
using CoachSportif.Filters;
using CoachSportif.Models;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    [AdminFilters]
    public class ActivitesController : Controller
    {
        private readonly MyContext db = new MyContext();

        // GET: Activites
        public ActionResult Index()
        {
            return View(db.Activites.ToList());
        }

        // GET: Activites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activite activite = db.Activites.Find(id);
            if (activite == null)
            {
                return HttpNotFound();
            }
            return View(activite);
        }

        // GET: Activites/Create
        public ActionResult Create()
        {
            ViewBag.Categories = db.CategorieActivites.Select(c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() });
            return View();
        }

        // POST: Activites/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateActiviteForm activite)
        {
            if (ModelState.IsValid)
            {
                string fileName = activite.Nom + Path.GetExtension(activite.ImageUrl.FileName);
                activite.ImageUrl.SaveAs(Server.MapPath("~/Content/images/") + fileName);
                db.Activites.Add(new Activite() 
                {
                    Nom = activite.Nom,
                    Descritption = activite.Descritption,
                    ImageUrl = fileName,
                    Categorie = db.CategorieActivites.Find(activite.Categorie)
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.CategorieActivites.Select(c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() });
            return View(activite);
        }

        // GET: Activites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activite activite = db.Activites.Find(id);
            if (activite == null)
            {
                return HttpNotFound();
            }
            return View(activite);
        }

        // POST: Activites/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImageUrl,Descritption,Nom")] Activite activite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activite);
        }

        // GET: Activites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activite activite = db.Activites.Find(id);
            if (activite == null)
            {
                return HttpNotFound();
            }
            return View(activite);
        }

        // POST: Activites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activite activite = db.Activites.Find(id);
            db.Activites.Remove(activite);
            db.SaveChanges();
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
