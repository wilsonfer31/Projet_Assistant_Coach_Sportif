using Coaching_Models;
using CoachSportif.Filters;
using CoachSportif.Models.FormsModel;
using CoachSportif.Tools;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    [LoginFilters]
    public class CoursController : BaseController<Cours>
    {

        // GET: Cours
        [AdminFilters]
        public override async Task<ActionResult> Index()
        {
            return await base.Index();
        }


        // GET: Cours/Create
        [CoachFilters]
        public override ActionResult Create()
        {
            return View(new CreateCoursForm());
        }

        // POST: Cours/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CoachFilters]
        public async Task<ActionResult> Create(CreateCoursForm ccf)
        {
            if (ModelState.IsValid)
            {
                await db.AddAsync(ccf.GetObject(db.Getcontext()));
                return RedirectToAction("Index");
            }
            return View(ccf);
        }
    }
}
