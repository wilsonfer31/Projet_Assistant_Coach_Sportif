﻿using Coaching_Models;
using CoachSportif.Filters;
using CoachSportif.Tools;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
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
        public async Task<ActionResult> DetailsCours(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach u = await db.FindByIdAsync(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u.CoursDispenses);
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
