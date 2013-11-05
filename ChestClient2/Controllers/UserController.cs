using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChestClient2.Models;

namespace ChestClient2.Controllers
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
