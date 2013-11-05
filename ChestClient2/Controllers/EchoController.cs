using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChestClient2.Controllers
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
