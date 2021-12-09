using Coaching_Models;
using CoachSportif.Filters;
using CoachSportif.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    public class UtilisateursController : Controller
    {
        private readonly MyContext db = new MyContext();

        // GET: Utilisateurs
        [AdminFilters]
        public ActionResult Index()
        {
            return View(db.Utilisateurs.Include(u => u.Ville).ToList());
        }

        // GET: Utilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // GET: Utilisateurs/Create
        public ActionResult Create()
        {
            Session.Remove("logging");
            return View(new RegisterForm { Villes = db.Villes.Select(V => new SelectListItem { Text = V.Nom + " - " + V.CP, Value = V.Id.ToString() }) });

        }

        // POST: Utilisateurs/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterForm registerForm)
        {
            if (ModelState.IsValid)
            {
                string fileName = registerForm.Pseudo + Path.GetExtension(registerForm.ProfilePicture.FileName);
                registerForm.ProfilePicture.SaveAs(Server.MapPath("~/Content/images/") + fileName);
                Utilisateur utilisateur = new Utilisateur
                {
                    Pseudo = registerForm.Pseudo,
                    MotDePasse = registerForm.MotDePasse,
                    Mail = registerForm.Mail,
                    Ville = db.Villes.Find(registerForm.Ville),
                    ProfilePicture = fileName
                };
                db.Utilisateurs.Add(utilisateur);
                db.SaveChanges();
                Session["logging"] = true;
                return RedirectToAction("Log");
            }
            return View(registerForm);
        }

        // GET: Utilisateurs/Edit/5
        [LoginFilters]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Include(u => u.Ville).SingleOrDefault(u => u.Id == id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(new EditForm(utilisateur, db.Villes.Select(V => new SelectListItem { Text = V.Nom + " - " + V.CP, Value = V.Id.ToString() })));
        }

        // POST: Utilisateurs/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginFilters]
        public ActionResult Edit(EditForm editForm)
        {
            List<SelectListItem> VilleSelect = new List<SelectListItem>();
            Utilisateur utilisateur = db.Utilisateurs.Include(u => u.Ville).SingleOrDefault(u => u.Id == editForm.Id);
            foreach (Ville V in db.Villes)
            {
                VilleSelect.Add(new SelectListItem { Text = V.Nom + " - " + V.CP, Value = V.Id.ToString() });
                if (V.Id.ToString().Equals(editForm.Ville))
                {
                    utilisateur.Ville = V;
                }
            }
            if (ModelState.IsValid)
            {
                utilisateur.UpdateFromForm(editForm);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = utilisateur.Id });
            }
            ViewBag.SelectVille = VilleSelect;
            return View(editForm);

        }

        // GET: Utilisateurs/Delete/5
        [LoginFilters]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [LoginFilters]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            db.Utilisateurs.Remove(utilisateur);
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

        public ActionResult Log()
        {
            Session["logging"] = true;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckLogin(LogForm user)
        {
            string msgErreur = "Echec authentification";

            if (ModelState.IsValid)
            {
                Utilisateur userDB = null;
                Coach coach = db.Coaches.Include(c => c.Utilisateur).SingleOrDefault(u => u.Utilisateur.Pseudo.Equals(user.Pseudo));
                if (coach == null)
                {
                    userDB = db.Utilisateurs.SingleOrDefault(u => u.Pseudo.Equals(user.Pseudo));
                }
                else
                {
                    userDB = coach.Utilisateur;
                }

                if (userDB != null)
                {
                    if (userDB.MotDePasse.Equals(user.MotDePasse))
                    {
                        if (coach != null)
                        {
                            Session["coach_id"] = coach.Id;
                        }

                        Session["user_id"] = userDB.Id;
                        Session["user_pseudo"] = userDB.Pseudo;
                        Session["user_profile"] = userDB.ProfilePicture;
                        if (userDB.Admin)
                        {
                            Session["admin"] = userDB.Admin;
                        }

                        Session.Remove("logging");
                        return RedirectToAction("Index", "Ville");
                    }
                    else
                    {
                        ViewBag.Error = msgErreur;
                    }
                }
                else
                {
                    ViewBag.Error = msgErreur;
                }
            }
            else
            {
                ViewBag.Error = msgErreur;
            }

            Session.Remove("logging");
            return RedirectToAction("Log");
        }
        [LoginFilters]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

        [AdminFilters]
        public ActionResult AdminState(int id)
        {
            Utilisateur utilisateur = db.Utilisateurs.Include(u => u.Ville).SingleOrDefault(u => u.Id == id);
            utilisateur.Admin = !utilisateur.Admin;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Auth()
        {
            return View();
        }
    }
}
