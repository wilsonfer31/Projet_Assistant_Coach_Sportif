using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Coaching_Models.ViewModel
{
    public class ViewModelVille
    {
        public IEnumerable<Cours> Cours { get; set; }
        public IEnumerable<Coach> Coaches { get; set; }
    }
}
