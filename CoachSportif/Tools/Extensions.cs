using Coaching_Models;
using CoachSportif.DAO;
using CoachSportif.Models;
using CoachSportif.Models.FormsModel;
using CoachSportif.Models.ViewModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace CoachSportif.Tools
{
    public static class Extensions
    {
        private static readonly MyContext db = new MyContext();
        public static Utilisateur GetUser(this RegisterForm rf)
        {
            return new Utilisateur
            {
                Pseudo = rf.Pseudo,
                MotDePasse = rf.MotDePasse,
                Mail = rf.Mail,
                Ville = db.Villes.Find(rf.Ville)
            };
        }
        public static Utilisateur GetUser(this EditForm ef)
        {
            return new Utilisateur
            {
                Id = ef.Id,
                Pseudo = ef.Pseudo,
                Nom = ef.Nom,
                Prenom = ef.Prenom,
                Tel = ef.Tel,
                Mail = ef.Mail,
                Adresse = ef.Adresse,
                Ville = db.Villes.Find(ef.Ville)
            };
        }
        public static Activite GetObject(this CreateActiviteForm caf)
        {
            return new Activite
            {
                Nom = caf.Nom,
                Descritption = caf.Descritption,
                ImageUrl = Path.GetExtension(caf.ImageUrl.FileName),
                Categorie = db.CategorieActivites.Find(caf.Categorie)
            };
        }
        public static Cours GetObject(this CreateCoursForm ccf)
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
            return c;
        }
        public static void ChangeAdminStateAsync(this Utilisateur u)
        {
            u.Admin = !u.Admin;
            db.SaveChangesAsync();
        }
        public static IQueryable<SelectListItem> InitVilles(this IEnumerable<SelectListItem> Villes)
        {
            return db.Villes.Select(V => new SelectListItem { Text = V.Nom + " - " + V.CP, Value = V.Id.ToString() });
        }
        public static IQueryable<SelectListItem> InitActivites(this IEnumerable<SelectListItem> Activites)
        {
            return db.Activites.Select(V => new SelectListItem { Text = V.Nom, Value = V.Id.ToString() });
        }
        public static IQueryable<SelectListItem> InitCategories(this IEnumerable<SelectListItem> Categories)
        {
            return db.CategorieActivites.Select(c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() });
        }
        public static void InitVilleViewModel(this ViewModelVille vm, int villeId)
        {
            vm.Cours = db.Cours.Where(c => c.Adresse.Id == villeId);
            vm.Coaches = db.Coaches.Where(c => c.Utilisateur.Ville.Id == villeId);
        }
        public static (Utilisateur, Coach) GetCoachOrUser(this GenericDao<Utilisateur> gd, LogForm user)
        {
            Utilisateur userDB = null;
            Coach coach = db.Coaches.SingleOrDefault(u => u.Utilisateur.Pseudo.Equals(user.Pseudo));
            if (coach == null)
            {
                userDB = db.Utilisateurs.SingleOrDefault(u => u.Pseudo.Equals(user.Pseudo));
            }
            else
            {
                userDB = coach.Utilisateur;
            }
            return (userDB, coach);
        }
        public static int GetUserVilleId(this GenericDao<Ville> gd, int userId)
        {
            return db.Utilisateurs.Find(userId).Ville.Id;
        }
        public static IQueryable<Utilisateur> GetUserToAppointCoach(this GenericDao<Coach> gd)
        {
            return db.Utilisateurs.Except(db.Coaches.Select(c => c.Utilisateur));
        }
        public static Coach UserToCoach(this GenericDao<Coach> gd, int id)
        {
            return new Coach { Utilisateur = db.Utilisateurs.Find(id) };
        }
        public static Coach DeleteCoach(this GenericDao<Coach> gd, int id)
        {

            Coach coach = db.Coaches.Include(c => c.CoursDispenses).SingleOrDefault(c => c.Id == id);
            if (coach.CoursDispenses.Count() > 0)
            {
                coach.CoursDispenses.ForEach(c => db.Cours.Remove(c));
            }

            return coach;
        }
    }
}