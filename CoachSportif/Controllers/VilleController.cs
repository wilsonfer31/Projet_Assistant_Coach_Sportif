using Coaching_Models.ViewModel;
using CoachSportif.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace CoachSportif.Controllers
{
    public class VilleController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Ville
        public ActionResult Index()
        {
            int userId = (int)Session["user_id"];
            int VilleId = db.Utilisateurs.AsNoTracking().Include(u => u.Ville).SingleOrDefault(u => u.Id == userId).Ville.Id;
            return View(new ViewModelVille { Cours =  db.Cours.Include(c => c.Adresse).Where(c => c.Adresse.Id == VilleId).Include(c => c.Activite.Categorie).Include(c => c.Coach.Utilisateur), Coaches = db.Coaches.Include(c => c.Utilisateur.Ville).Where(c => c.Utilisateur.Ville.Id == VilleId)});
        }
    }
}