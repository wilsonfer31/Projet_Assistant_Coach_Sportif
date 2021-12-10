using CoachSportif.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CoachSportif.Models.FormsModel
{
    public class CreateCoursForm
    {
        public CreateCoursForm()
        {
            Villes = Villes.InitVilles();
            Activites = Activites.InitActivites();
        }

        [Required]
        public int CoachId { get; set; }
        [Required]
        public DateTime DateCours { get; set; } = DateTime.Now;
        [Required]
        public int Heure { get; set; } = DateTime.Now.Hour;
        public IEnumerable<SelectListItem> Hours { get; set; } = InitHours();
        [Required]
        public int Minutes { get; set; }
        public IEnumerable<SelectListItem> Mins { get; set; } = InitMins();
        [Required]
        public int Ville { get; set; }
        public IEnumerable<SelectListItem> Villes { get; set; }
        [Required]
        public int Activite { get; set; }
        public IEnumerable<SelectListItem> Activites { get; set; }

        private static IEnumerable<SelectListItem> InitHours()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            for (int i = 0; i < 24; i++)
            {
                lst.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            return lst;
        }
        private static IEnumerable<SelectListItem> InitMins()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            for (int i = 0; i < 61; i += 5)
            {
                lst.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            return lst;
        }
    }
}
