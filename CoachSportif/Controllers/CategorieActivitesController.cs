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
    public class CategorieActivitesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: CategorieActivites
        public ActionResult Index()
        {
            return View(db.CategorieActivites.ToList());
        }

        // GET: CategorieActivites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActivite categorieActivite = db.CategorieActivites.Find(id);
            if (categorieActivite == null)
            {
                return HttpNotFound();
            }
            return View(categorieActivite);
        }

        // GET: CategorieActivites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategorieActivites/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descritption,Nom")] CategorieActivite categorieActivite)
        {
            if (ModelState.IsValid)
            {
                db.CategorieActivites.Add(categorieActivite);
                db.SaveChanges();
                return RedirectToAction("Create","Activites");
            }

            return View(categorieActivite);
        }

        // GET: CategorieActivites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActivite categorieActivite = db.CategorieActivites.Find(id);
            if (categorieActivite == null)
            {
                return HttpNotFound();
            }
            return View(categorieActivite);
        }

        // POST: CategorieActivites/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descritption,Nom")] CategorieActivite categorieActivite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorieActivite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorieActivite);
        }

        // GET: CategorieActivites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActivite categorieActivite = db.CategorieActivites.Find(id);
            if (categorieActivite == null)
            {
                return HttpNotFound();
            }
            return View(categorieActivite);
        }

        // POST: CategorieActivites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategorieActivite categorieActivite = db.CategorieActivites.Find(id);
            db.CategorieActivites.Remove(categorieActivite);
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
