using System.Collections.Generic;
using System.Web.Mvc;
using ChestClient.Models;

namespace ChestClient.Controllers
{
    public class UserController : Controller

    {
        private static readonly Dictionary<string, UserModels> Users = new Dictionary<string, UserModels>();
        //
        // GET: /User /

        public ActionResult List()
        {
            return Json (Users, JsonRequestBehavior.AllowGet);
        }

    }
}
