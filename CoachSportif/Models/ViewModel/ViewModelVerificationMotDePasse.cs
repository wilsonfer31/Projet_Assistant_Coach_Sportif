using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoachSportif.Models.ViewModel
{
    public class ViewModelVerificationMotDePasse
    {
        public int Id { get; set; }
        public string AncienMotDePasse { get; set; }

        public string NouveauMotDePasse { get; set; }
    }
}