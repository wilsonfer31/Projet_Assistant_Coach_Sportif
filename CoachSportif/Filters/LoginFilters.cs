using System.Web.Mvc;

namespace CoachSportif.Filters
{
    public class LoginFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user_id"] == null)
            {
                filterContext.HttpContext.Response.Redirect("~/Utilisateurs/Log");

            }

        }

    }
}