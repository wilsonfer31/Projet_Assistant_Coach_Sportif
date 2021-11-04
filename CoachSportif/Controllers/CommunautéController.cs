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
            var intSession = int.Parse(Session["user_id"].ToString());
            Utilisateur userinfo = myContext.Utilisateurs.Find(intSession);



            ViewBag.userName = userinfo.Pseudo;
            ViewBag.GroupeChats = myContext.GroupeChats.Include(gc => gc.Membres);

            ViewBag.Messages = myContext.Messages.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Index(Message message)
        {
            var intSession = int.Parse(Session["user_id"].ToString());
            Utilisateur myUser = myContext.Utilisateurs.Find(intSession);



            if (ModelState.IsValid)
            {
                GroupeChat groupeChat = new GroupeChat();
                Message message1 = new Message();

                message1.Utilisateur = myUser;
                message1.Date = new System.DateTime();
                message1.MessageText = message.MessageText;

                
                groupeChat.Membres.Add(myUser);
                groupeChat.ChatMessages.Add(message1);


                myContext.GroupeChats.Attach(groupeChat);
                myContext.Entry(groupeChat).State = EntityState.Modified;
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
           

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