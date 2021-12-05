using System.Web.Mvc;

namespace PSS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = Utils.GetHomeModel();
            return View("Index", model);
        }
    }
}