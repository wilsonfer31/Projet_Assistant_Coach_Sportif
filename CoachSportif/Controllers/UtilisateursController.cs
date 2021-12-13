using Coaching_Models;
using CoachSportif.Filters;
using CoachSportif.Models.FormsModel;
using CoachSportif.Models.ViewModel;
using CoachSportif.Tools;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    public class UtilisateursController : BaseController<Utilisateur>
    {

        // GET: Utilisateurs/Create
        public override ActionResult Create()
        {
            Session.Remove("logging");
            return View(new RegisterForm());
        }

        // POST: Utilisateurs/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterForm registerForm)
        {
            if (ModelState.IsValid)
            {
                if (await db.AddAsync(registerForm.GetUser(db.Getcontext())) != default)
                {
                    Session["logging"] = true;
                    return RedirectToAction("Log");
                }
            }
            return View(registerForm);
        }
        // GET: Utilisateurs/Edit/5
        [LoginFilters]
        public override async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = await db.FindByIdAsync(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(new EditForm(utilisateur));
        }

        // POST: Utilisateurs/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginFilters]
        public ActionResult Edit(EditForm editForm)
        {
            if (ModelState.IsValid)
            {
                if (db.UpdateAsync(editForm.GetUser(db.Getcontext())).Result)
                {
                    return RedirectToAction("Details", new { id = editForm.Id });
                }
            }
            return View(editForm);
        }

        public ActionResult Log()
        {
            Session["logging"] = true;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckLogin(LogForm lf)
        {
            string msgErreur = "Echec authentification";

            if (ModelState.IsValid)
            {
                (Utilisateur userDB, Coach coach) = db.GetCoachOrUser(lf);

                if (userDB != null)
                {
                    if (userDB.MotDePasse.Equals(HashTool.CryptPassword(lf.MotDePasse)))
                    {
                        if (coach != null)
                        {
                            Session["coach_id"] = coach.Id;
                        }
                        Session["user_id"] = userDB.Id;
                        Session["user_pseudo"] = userDB.Pseudo;
                        Session["user_profile"] = userDB.ProfilePicture;
                        if (userDB.Admin)
                        {
                            Session["admin"] = userDB.Admin;
                        }
                        Session.Remove("logging");
                        return RedirectToAction("Details", "Ville");
                    }
                    else
                    {
                        ViewBag.Error = msgErreur;
                    }
                }
                else
                {
                    ViewBag.Error = msgErreur;
                }
            }
            else
            {
                ViewBag.Error = msgErreur;
            }

            Session.Remove("logging");
            return RedirectToAction("Log");
        }
        [LoginFilters]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

        [AdminFilters]
        public ActionResult AdminState(int id)
        {
            db.FindByIdAsync(id).Result.ChangeAdminStateAsync(db.Getcontext());
            return RedirectToAction("Index");
        }

        public ActionResult Auth()
        {
            return View();
        }


        public async Task<ActionResult> changePassword()
        {
            
            

            return View(new ViewModelVerificationMotDePasse() {Id = (int)Session["user_id"]});
        }

        [HttpPost]
        public async Task<ActionResult> changePassword(ViewModelVerificationMotDePasse vmdp)
        {
            if (ModelState.IsValid)
            {
                Utilisateur u = await db.FindByIdAsync(vmdp.Id);
                if (u.MotDePasse.Equals(HashTool.CryptPassword(vmdp.AncienMotDePasse)))
                {                    
                    await db.UpdateAsync(vmdp.GetUser(db.Getcontext()));
                }              
            }


            return RedirectToAction("Details", new { id = vmdp.Id });
        }
    }
}
