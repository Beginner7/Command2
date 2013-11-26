using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using ChestClient.Models;
using Protocol;
using Protocol.Transport;

namespace ChestClient.Controllers
{
    public class UserController : Controller
    {
        public static readonly Dictionary<string, UserModel> Users = new Dictionary<string, UserModel>();

        

        public ActionResult List()
        {
            /*var allUserModels = _db.UserModels.ToList<UserModel>();
            ViewBag.UserModels = allUserModels;
            */
            return Json (Users.Values, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View("User", new UserModel());
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            Users[model.UserName] = model;
            return RedirectToAction("Index");

        }
        
        public ActionResult Index()
        {
            var request = new UserListRequest();
            var response = ServerProvider.MakeRequest<UserListResponse>(request);
            return View("Index", response.Users);
        }
    }
}
