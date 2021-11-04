using Coaching_Models;
using CoachSportif.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Linq;
using System.Data.Entity;

namespace CoachSportif.Controllers
{
    public class CommunautéController : Controller
    {

        MyContext myContext = new MyContext();
        // GET: Communauté
        public ActionResult Index()
        {

            ViewBag.GroupeChats = myContext.GroupeChats.Include(gc => gc.Membres);
            return View();
        }


        public ActionResult CreateNewChat()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewChat(GroupeChat groupeChat)
        {

            var intSession = int.Parse(Session["user_id"].ToString());


            Utilisateur myUser = myContext.Utilisateurs.Find(intSession);

            if (ModelState.IsValid)
            {
                GroupeChat groupeChatSender = new GroupeChat();
                groupeChatSender.Nom = groupeChat.Nom;
                groupeChatSender.Membres.Add(myUser);
                myUser.GroupeChats.Add(groupeChatSender);
                
                myContext.GroupeChats.Add(groupeChatSender);
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupeChat);
        }
    }
}