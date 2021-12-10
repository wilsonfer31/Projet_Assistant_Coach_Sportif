using Coaching_Models;
using CoachSportif.Filters;
using CoachSportif.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoachSportif.Controllers
{
    [AdminFilters]
    public class CategorieActivitesController : BaseController<CategorieActivite>
    {
    }
}
