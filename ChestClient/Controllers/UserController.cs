using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using ChestClient.Models;

namespace ChestClient.Controllers
{
    public class UserController : Controller

    {
        private UserContext _db = new UserContext();
        //
        // GET: /User /

        public ActionResult List()
        {
            var allUserModels = _db.UserModels.ToList<UserModel>();
            ViewBag.UserModels = allUserModels;
            return Json (ViewBag.UserModels, JsonRequestBehavior.AllowGet);
        }

    }
}
