using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;

namespace ChestClient.Controllers
{
    public class ChatController : Controller
    {
        //
        // GET: /Chat/

        public ActionResult Index()
        {
            return View();
        }

        public string Name
        {
            get { return "chat"; }
        }

        public ActionResult SendMessage()
        {
            int gameId;
            if (!int.TryParse(Request.Params["GameID"], out gameId))
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var request = new ChatRequest
            {
                SayString = Request.Params["Message"],
                From = User.Identity.Name,
                GameID = gameId
            };
            var response = ServerProvider.MakeRequest<ChatResponse>(request);
            if (response.Status != Statuses.Ok)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}