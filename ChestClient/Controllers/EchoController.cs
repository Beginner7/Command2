using System.Web.Mvc;

namespace ChestClient.Controllers
{
    public class EchoController : Controller
    {
        //
        // GET: /Echo/

        public ActionResult Index(string in_str)
        {
            return Json("ECHO: "+in_str, JsonRequestBehavior.AllowGet);
        }

    }
}