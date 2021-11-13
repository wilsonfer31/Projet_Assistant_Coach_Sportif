using System.Web.Mvc;

namespace CoachSportif.Filters
{
    public class AdminFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["admin"] == null)
            {
                filterContext.HttpContext.Response.Redirect("~/Utilisateurs/Auth");
            }
        }
    }
}