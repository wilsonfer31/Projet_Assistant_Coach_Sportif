using Coaching_Models;
using CoachSportif.Filters;
using CoachSportif.Models;
using CoachSportif.Models.ViewModel;
using CoachSportif.Tools;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{


    [LoginFilters]
    public class CommunautéController : Controller
    {
        readonly MyContext myContext = new MyContext();
        // GET: Communauté
        public ActionResult Index(int? id)
        {
            var intSession = int.Parse(Session["user_id"].ToString());
            Utilisateur userinfo = myContext.Utilisateurs.Find(intSession);
            if (id.HasValue)
            {
                ViewBag.CurrentChat = id.Value;
            }
            else if (userinfo.GroupeChats.Count() > 0)
            {
                ViewBag.CurrentChat = userinfo.GroupeChats.ElementAt(0).Id;
            }
            return View(userinfo.GroupeChats);
        }

        [HttpPost]
        public ActionResult Chat(ViewModelChatGroupMessages viewModelChatGroupMessages)
        {
            var intSession = int.Parse(Session["user_id"].ToString());
            Utilisateur myUser = myContext.Utilisateurs.Find(intSession);

            GroupeChat gc = myContext.GroupeChats.Include(g => g.ChatMessages.Select(m => m.Utilisateur)).SingleOrDefault(g => g.Id == viewModelChatGroupMessages.CurrentChat);

            if (ModelState.IsValid)
            {
                Message message1 = new Message
                {
                    Utilisateur = myUser,
                    Date = DateTime.Now,
                    MessageText = viewModelChatGroupMessages.Messages.MessageText
                };
                gc.ChatMessages.Add(message1);
                myContext.SaveChanges();
                return View(gc);
            }
            return View(gc);
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
                GroupeChat groupeChatSender = new GroupeChat
                {
                    Nom = groupeChat.Nom
                };
                groupeChatSender.Membres.Add(myUser);
                myUser.GroupeChats.Add(groupeChatSender);

                myContext.GroupeChats.Add(groupeChatSender);
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupeChat);
        }



        public ActionResult JoinAchat()
        {
            ViewBag.GroupeChats = myContext.GroupeChats.Include(gc => gc.Membres);
            return View();
        }

        [HttpPost]
        public ActionResult JoinAchat(ViewModelChatGroupMessages ViewModelChatGroupMessages)
        {

            var intSession = int.Parse(Session["user_id"].ToString());
            ViewBag.GroupeChats = myContext.GroupeChats.Include(gc => gc.Membres);
            Utilisateur myUser = myContext.Utilisateurs.Find(intSession);

            if (ModelState.IsValid)
            {

                GroupeChat groupeChat1 = myContext.GroupeChats.Find(ViewModelChatGroupMessages.CurrentChat);
                groupeChat1.Membres.Add(myUser);
                myUser.GroupeChats.Add(groupeChat1);

                myContext.SaveChanges();
                return RedirectToAction("Index", new { groupeChat1.Id });
            }
            return View(ViewModelChatGroupMessages.CurrentChat);
        }

        public ActionResult Groupes()
        {
            return View(myContext.GroupeChats.Include(g => g.Membres));
        }

        public ActionResult Groupe(int? id)
        {
            return View(myContext.GroupeChats.Include(g => g.Membres).Include(g => g.ChatMessages.Select(m => m.Utilisateur)).SingleOrDefault(g => g.Id == id.Value));
        }



        public ActionResult Details(int? id, int? opt)
        {
            ViewBag.GroupeNom = myContext.GroupeChats.Find(id.Value).Nom;
            return View(myContext.GroupeChats.Include(g => g.ChatMessages.Select(m => m.Utilisateur)).FirstOrDefault(g => g.Id == id.Value).ChatMessages.Where(m => m.Utilisateur.Id == opt.Value));
        }
    }
}