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
        public async Task<ActionResult> CreateCoursAsync(CreateCoursForm ccf)
        {
            if (ModelState.IsValid)
            {
                await db.AddAsync(ccf.GetObject(db.Getcontext()));
                return RedirectToAction("Details","Utilisateurs", new {id = (int)Session["user_id"]});
            }
            return View("Create",ccf);
        }
        public async Task<ActionResult> Join(int? id)
        {
            if (id.HasValue)
            {
                Cours c = db.UserJoinCours(id.Value, (int)Session["user_id"]);
                if (await db.UpdateAsync(c))
                {
                    return RedirectToAction("Index", "Communauté", c.Chat.Id);
                }
            }
            return RedirectToAction("Details", "Cours", id);
        }
        public async Task<ActionResult> Leave(int? id)
        {
            if (id.HasValue)
            {
                Cours c = db.UserLeftCours(id.Value, (int)Session["user_id"]);
                if (await db.UpdateAsync(c))
                {
                    return RedirectToAction("Index", "Communauté", c.Chat.Id);
                }
            }
            return RedirectToAction("Details", "Cours", id);
        }
        public async Task<ActionResult> CoachLeave(int? id)
        {
            if (id.HasValue)
            {
                Cours c = db.CoachRemoveCours(id.Value);
                if (await db.RemoveAsync(c))
                {
                    return RedirectToAction("DetailsCours", "Coaches", new { id = (int)Session["coach_id"] });
                }
            }
            return RedirectToAction("DetailsCours", "Coaches", new { id = (int)Session["coach_id"] });
        }
    }
}
