﻿using Coaching_Models;
using CoachSportif.DAO;
using CoachSportif.Models;
using CoachSportif.Models.FormsModel;
using CoachSportif.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace CoachSportif.Tools
{
    public static class Extensions
    {
        public static Utilisateur GetUser(this RegisterForm rf, MyContext db)
        {
            Utilisateur u = new Utilisateur
            {
                Pseudo = rf.Pseudo,
                MotDePasse = HashTool.CryptPassword(rf.MotDePasse),
                Mail = rf.Mail,
                Ville = db.Villes.Find(rf.Ville)
            };
            db.Villes.Attach(u.Ville);
            return u;
        }
        public static Utilisateur GetUser(this EditForm ef, MyContext db)
        {
            Utilisateur u = new Utilisateur
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
            db.Villes.Attach(u.Ville);
            return u;
        }
        public static Activite GetObject(this CreateActiviteForm caf, MyContext db)
        {
            return new Activite
            {
                Nom = caf.Nom,
                Descritption = caf.Descritption,
                ImageUrl = Path.GetExtension(caf.ImageUrl.FileName),
                Categorie = db.CategorieActivites.Find(caf.Categorie)
            };
        }
        public static Cours GetObject(this CreateCoursForm ccf, MyContext db)
        {
            Coach co = db.Coaches.Find(ccf.CoachId);
            Activite act = db.Activites.Find(ccf.Activite);
            DateTime dt = ccf.DateCours.AddHours(ccf.Heure).AddMinutes(ccf.Minutes);
            Cours c = new Cours
            {
                Coach = co,
                Activite = act,
                Adresse = db.Villes.Find(ccf.Ville),
                DateCours = dt,
                Chat = new GroupeChat
                {
                    Nom = act.Nom + " - " + dt.ToShortDateString(),
                    Membres = new List<Utilisateur> { co.Utilisateur }
                }
            };
            co.CoursDispenses.Add(c);
            co.Utilisateur.GroupeChats.Add(c.Chat);
            db.Villes.Attach(c.Adresse);
            db.Activites.Attach(c.Activite);
            return c;
        }
        public static Cours UserJoin(this GenericDao<Cours> gd, int coursId, int userId)
        {
            MyContext db = gd.Getcontext();
            Cours c = db.Cours.Find(coursId);
            Utilisateur u = db.Utilisateurs.Find(userId);
            if(c.Coach.Utilisateur != u && !c.Adherents.Contains(u))
            {
                c.Adherents.Add(u);
                c.Chat.Membres.Add(u);
                u.CoursSuivis.Add(c);
                u.GroupeChats.Add(c.Chat);
            }
            return c;
        }
        public static void ChangeAdminStateAsync(this Utilisateur u, MyContext db)
        {
            u.Admin = !u.Admin;
            db.SaveChangesAsync();
        }
        public static IQueryable<SelectListItem> InitVilles(this IEnumerable<SelectListItem> Villes)
        {
            return new MyContext().Villes.Select(V => new SelectListItem { Text = V.Nom + " - " + V.CP, Value = V.Id.ToString() });
        }
        public static IQueryable<SelectListItem> InitActivites(this IEnumerable<SelectListItem> Activites)
        {
            return new MyContext().Activites.Select(V => new SelectListItem { Text = V.Nom, Value = V.Id.ToString() });
        }
        public static IQueryable<SelectListItem> InitCategories(this IEnumerable<SelectListItem> Categories)
        {
            return new MyContext().CategorieActivites.Select(c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() });
        }
        public static void InitVilleViewModel(this ViewModelVille vm, int villeId)
        {
            MyContext db = new MyContext();
            vm.Cours = db.Cours.Where(c => c.Adresse.Id == villeId);
            vm.Coaches = db.Coaches.Where(c => c.Utilisateur.Ville.Id == villeId);
        }
        public static (Utilisateur, Coach) GetCoachOrUser(this GenericDao<Utilisateur> gd, LogForm user)
        {
            MyContext db = gd.Getcontext();
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
            return gd.Getcontext().Utilisateurs.Find(userId).Ville.Id;
        }
        public static IQueryable<Utilisateur> GetUserToAppointCoach(this GenericDao<Coach> gd)
        {
            MyContext db = gd.Getcontext();
            return db.Utilisateurs.Except(db.Coaches.Select(c => c.Utilisateur));
        }
        public static Coach UserToCoach(this GenericDao<Coach> gd, int id)
        {
            MyContext db = gd.Getcontext();
            Coach c = new Coach { Utilisateur = db.Utilisateurs.Find(id) };
            db.Utilisateurs.Attach(c.Utilisateur);
            return c;
        }
        public static Coach DeleteCoach(this GenericDao<Coach> gd, int id)
        {
            MyContext db = gd.Getcontext();
            Coach coach = db.Coaches.Include(c => c.CoursDispenses).SingleOrDefault(c => c.Id == id);
            if (coach.CoursDispenses.Count() > 0)
            {
                coach.CoursDispenses.ForEach(c => db.Cours.Remove(c));
            }
            return coach;
        }
    }
}