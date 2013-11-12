using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using ChestClient.Models;

namespace ChestClient.Controllers
{
    public class UserController : Controller

    {

        private static readonly Dictionary<string, UserModel> Users = new Dictionary<string, UserModel>();
        public ActionResult List()
        {
            /*var allUserModels = _db.UserModels.ToList<UserModel>();
            ViewBag.UserModels = allUserModels;
            */
            return Json (Users.Values, JsonRequestBehavior.AllowGet);
        }

    }
}
