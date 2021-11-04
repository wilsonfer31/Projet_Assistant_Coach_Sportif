using CoachSportif.Models;
using System.Web.Mvc;
using System.Data.Entity;

namespace CoachSportif.Controllers
{
    public class CoachController : Controller
    {
        MyContext db = new MyContext();
        // GET: Coach
        public ActionResult Index()
        {
            return View(db.Coaches.Include(c=>c.Utilisateur));
        }
    }
}