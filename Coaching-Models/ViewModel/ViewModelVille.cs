using System.Collections.Generic;

namespace Coaching_Models.ViewModel
{
    public class ViewModelVille
    {
        public IEnumerable<Cours> Cours { get; set; }
        public IEnumerable<Coach> Coaches { get; set; }
    }
}
