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
    public class UtilisateursController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Utilisateurs
        public ActionResult Index()
        {
            return View(db.Utilisateurs.ToList());
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
            List<SelectListItem> VilleSelect = new List<SelectListItem>();
            foreach (Ville V in new List<Ville> 
            {
                new Ville {Id= 0, Nom = "Paris", CP = 75000},
                new Ville {Id= 1, Nom = "Le Mans", CP = 72000},
                new Ville {Id= 2, Nom = "Nantes", CP = 44000},
                new Ville {Id= 3, Nom = "Toulouse", CP = 31000},
            })
            {
                VilleSelect.Add(new SelectListItem { Selected = false, Text = V.Nom + " - " + V.CP, Value = V.Id.ToString() });
            }
            ViewBag.SelectVille = VilleSelect;
            return View();
        }

        // POST: Utilisateurs/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Pseudo,MotDePasse,Prenom,Tel,Mail,Adresse,Nom,Ville")] Utilisateur utilisateur, [Bind(Include = "Ville")] string ville)
        {
            List<Ville> lv = new List<Ville>
            {
                new Ville {Id= 0, Nom = "Paris", CP = 75000},
                new Ville {Id= 1, Nom = "Le Mans", CP = 72000},
                new Ville {Id= 2, Nom = "Nantes", CP = 44000},
                new Ville {Id= 3, Nom = "Toulouse", CP = 31000},
            };
            List<SelectListItem> VilleSelect = new List<SelectListItem>();
            foreach (Ville V in lv)
            {
                VilleSelect.Add(new SelectListItem { Selected = false, Text = V.Nom + " - " + V.CP, Value = V.Id.ToString() });
                if (V.Id.ToString().Equals(ville)) utilisateur.Ville = V;
            }
            ViewBag.SelectVille = VilleSelect;

            if (ModelState.IsValid)
            {
                db.Utilisateurs.Add(utilisateur);
                db.SaveChanges();
                Session["logging"] = true;
                return RedirectToAction("Log");
            }
            return View(utilisateur);
        }

        // GET: Utilisateurs/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Utilisateurs/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pseudo,MotDePasse,Prenom,Tel,Mail,Adresse,Nom")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utilisateur);
        }

        // GET: Utilisateurs/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            db.Utilisateurs.Remove(utilisateur);
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
                Coach coach = db.Coaches.SingleOrDefault(u => u.Utilisateur.Pseudo.Equals(user.Pseudo));
                if(coach == null)
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
                        /*
                         * Session est un objet (dictionnaire crée côté serveur), accessible par l'ensemble des contrôleurs
                         * (idem vues) disponible tant que l'application est en cours d'execution
                         * Une session possède une durée par defaut limitée à 20min
                         */
                        if (coach != null)
                        {
                            Session["coach_id"] = coach.Id;  //Enregistrement de userDB.Admin dans la session
                        }
                        Session["user_id"] = userDB.Id;

                        //Session.Timeout = 1; //Permet de limiter la durée de la session à 1 min*
                        Session.Remove("logging");
                        return RedirectToAction("Index","Ville");
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

        public ActionResult Logout()
        {
            //Vider le contenu de la clé user_id définie dans la session
            Session.Remove("user_id");

            Session.Remove("coach_id");

            //Pour vider tout le conteni de la session 
            //Session.RemoveAll();

            return RedirectToAction("Index","Home");
        }
    }
}
