using System.Web.Mvc;

namespace ChestClient.Controllers
{
    public class EchoController : Controller
    {
        //
        // GET: /Echo/

        public ActionResult Index(string inStr)
        {
            return Json(inStr, JsonRequestBehavior.AllowGet);
        }

    }
}