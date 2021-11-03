using CoachSportif.Models;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(MailForm form)
        {
            if (ModelState.IsValid)
            {
                MailMessage msg = new MailMessage(form.Mail, "tahmi.ma222@gmail.com", form.Sujet, form.Contenu);
                msg.CC.Add("tahmi.ma222@gmail.com");

                //Instanciation du client: On se connecte au serveur SMTP gmail
                //Provider Gmail
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                //On indique au client d'utiliser les informations qu'on va lui fournir
                client.UseDefaultCredentials = true;

                //Ajout des informations de connexion
                //On initialise une nouvelle instance NetworkCredential avec le nom de l'utilisateur et le MDP
                client.Credentials = new NetworkCredential("claudeaziz8@gmail.com", "Dawan1234");

                //On active le protocole SSL
                client.EnableSsl = true;
                client.Send(msg);

                ViewBag.Message = "Votre message a bien été envoyer";
                ModelState.Clear();
            }
            return View(new MailForm());
        }
    }
}