using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoachSportif.Models;
using Coaching_Models;

namespace CoachSportif.Controllers
{
    public class CoursController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Cours
        public ActionResult Index()
        {
            return View(db.Cours.Include(C => C.Adresse).Include(C => C.Activite).Include(C =>  C.Coach.Utilisateur));
        }

        // GET: Cours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Cours.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }


        // GET: Cours/Create
        public ActionResult Create()
        {
            CreateCoursForm ccf = new CreateCoursForm
            {
                Villes = db.Villes.Select(V => new SelectListItem { Text = V.Nom + " - " + V.CP, Value = V.Id.ToString() }),
                Activites = db.Activites.Select(A => new SelectListItem { Text = A.Nom, Value = A.Id.ToString() })
            };
            return View(ccf);
        }

        // POST: Cours/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCoursForm ccf)
        {
            if (ModelState.IsValid)
            {
                Coach co = db.Coaches.Find(ccf.CoachId);
                Cours c = new Cours
                {
                    Coach = co,
                    Activite = db.Activites.Find(ccf.Activite),
                    Adresse = db.Villes.Find(ccf.Ville),
                    DateCours = ccf.DateCours.AddHours(ccf.Heure).AddMinutes(ccf.Minutes)
                };
                co.CoursDispenses.Add(c);
                db.Cours.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ccf);
        }

        // GET: Cours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Cours.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Cours/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateCours")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cours);
        }

        // GET: Cours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Cours.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Cours.Find(id);
            db.Cours.Remove(cours);
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
