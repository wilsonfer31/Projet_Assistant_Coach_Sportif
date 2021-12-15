using Coaching_Models;
using CoachSportif.Tools;
using System.Collections.Generic;

namespace CoachSportif.Models.ViewModel
{
    public class ViewModelVille
    {
        public ViewModelVille()
        {
        }
        public ViewModelVille(int villeID)
        {
            this.InitVilleViewModel(villeID);
        }
        public ViewModelVille(Ville v)
        {
            Ville = v;
            this.InitVilleViewModel(v.Id);
        }
        public Ville Ville { get; set; }
        public IEnumerable<Cours> Cours { get; set; }
        public IEnumerable<Coach> Coaches { get; set; }
    }
}
