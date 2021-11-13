using Coaching_Models;
using Coaching_Models.ViewModelChatGroupMessages;
using CoachSportif.Filters;
using CoachSportif.Models;
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
            Utilisateur userinfo = myContext.Utilisateurs.Include(u => u.GroupeChats).SingleOrDefault(u => u.Id == intSession);

            ViewBag.Messages = myContext.Messages.ToList();
            ViewBag.userName = userinfo.Pseudo;
            ViewBag.GroupeChats = myContext.GroupeChats.Include(gc => gc.Membres);

            if (id.HasValue)
            {


                ViewBag.Groupe = myContext.GroupeChats.Find(id.Value);
            }
            else if (userinfo.GroupeChats.Count() > 0)
            {
                ViewBag.Groupe = myContext.GroupeChats.Find(userinfo.GroupeChats.ElementAt(0).Id);

            }




            return View();
        }

        [HttpPost]
        public ActionResult Chat(ViewModelChatGroupMessages viewModelChatGroupMessages)
        {
            var intSession = int.Parse(Session["user_id"].ToString());
            Utilisateur myUser = myContext.Utilisateurs.Find(intSession);

            GroupeChat gc = myContext.GroupeChats.Include(g => g.ChatMessages.Select(m => m.Utilisateur)).SingleOrDefault(g => g.Id == viewModelChatGroupMessages.GroupeChats);

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

                GroupeChat groupeChat1 = myContext.GroupeChats.Find(ViewModelChatGroupMessages.GroupeChats);
                groupeChat1.Membres.Add(myUser);
                myUser.GroupeChats.Add(groupeChat1);

                myContext.SaveChanges();
                return RedirectToAction("Index", new { groupeChat1.Id });
            }
            return View(ViewModelChatGroupMessages.GroupeChats);
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