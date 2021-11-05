using Coaching_Models;
using CoachSportif.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Linq;
using System.Data.Entity;
using Coaching_Models.ViewModelChatGroupMessages;
using System;

namespace CoachSportif.Controllers
{
    public class CommunautéController : Controller
    {

        MyContext myContext = new MyContext();
        // GET: Communauté
        public ActionResult Index(int? id)
        {
            var intSession = 5;
           // var intSession = int.Parse(Session["user_id"].ToString());
            Utilisateur userinfo = myContext.Utilisateurs.Find(intSession);

            ViewBag.Messages = myContext.Messages.ToList();
            ViewBag.userName = userinfo.Pseudo;
            ViewBag.GroupeChats = myContext.GroupeChats.Include(gc => gc.Membres);

                if(id.HasValue)
            {


                ViewBag.Groupe = myContext.GroupeChats.Find(id.Value);
            };
           
            

            return View();
        }

        [HttpPost]
        public ActionResult Index(ViewModelChatGroupMessages viewModelChatGroupMessages)
        {
            var intSession = 5;
            // var intSession = int.Parse(Session["user_id"].ToString());
            Utilisateur myUser = myContext.Utilisateurs.Find(intSession);
            ViewBag.GroupeChats = myContext.GroupeChats.Include(gc => gc.Membres);


            if (ModelState.IsValid)
            {

               
              
                Message message1 = new Message();

                message1.Utilisateur = myUser;
                message1.Date =DateTime.Now;
                message1.MessageText = viewModelChatGroupMessages.messages.MessageText;

                myContext.GroupeChats.Find(viewModelChatGroupMessages.groupeChats).ChatMessages.Add(message1);
             
             
                


                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModelChatGroupMessages);
           

        }


        public ActionResult CreateNewChat()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewChat(GroupeChat groupeChat)
        {
            var intSession = 5;
            //var intSession = int.Parse(Session["user_id"].ToString());


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