using System.Web.Caching;
using System.Web.Mvc;

namespace ChestClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "";

            return RedirectToAction("Free","Game");
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
