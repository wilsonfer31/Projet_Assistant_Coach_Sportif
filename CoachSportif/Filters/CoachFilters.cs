using System.Web.Mvc;

namespace CoachSportif.Filters
{
    public class CoachFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["coach_id"] == null && filterContext.HttpContext.Session["admin"] == null)
            {
                filterContext.HttpContext.Response.Redirect("~/Utilisateurs/Auth");
            }
        }
    }
}