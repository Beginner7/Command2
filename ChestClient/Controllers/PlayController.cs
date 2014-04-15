using System;
using System.Web.Mvc;
using Protocol;
using Protocol.Transport;

namespace ChestClient.Controllers
{
    public class PlayController : Controller
    {
        //
        // GET: /Game/

        public ActionResult PulseRequest()
        {
            var request = new PulseRequest
            {
                From = User.Identity.Name,
            };
            var response = ServerProvider.MakeRequest<PulseResponse>(request);
            string ret;
            switch (response.Status)
            {
                case Statuses.Ok:
                    ret = "";
                    break;
                case Statuses.NoUser:
                    ret = "User no found.";
                    break;
                case Statuses.DuplicateUser:
                    ret = "You allready in que";
                    break;
                default:
                    ret = "Wrong status.";
                    break;
            }
            return Json(new
            {
                Ret = ret,
                Messages = response.Messages,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PlayRequest()
        {
            var request = new PlayRequest
            {
                UserName = User.Identity.Name,
            };
            var response = ServerProvider.MakeRequest<PlayResponse>(request);
            string ret;
            switch (response.Status)
            {
                case Statuses.Ok:
                    ret = "";
                    break;
                case Statuses.NoUser:
                    ret = "User no found."; 
                    break;
                case Statuses.DuplicateUser:
                    ret = "You allready in que";
                    break;
                default:
                    ret = "Wrong status.";
                    break;
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StopRequest()
        {
            var request = new StopRequest
            {
                UserName = User.Identity.Name
            };
            var response = ServerProvider.MakeRequest<StopResponse>(request);
            string ret;
            switch (response.Status)
            {
                case Statuses.Ok:
                    ret = "";
                    break;
                case Statuses.NoUser:
                    ret = "User no found.";
                    break;
                default:
                    ret = "Wrong status.";
                    break;
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SurrenderRequest(string gameId)
        {
            var request = new SurrenderRequest {From = User.Identity.Name};
            try
            {
                request.GameID = Int32.Parse(gameId);
            }
            catch (Exception)
            {
                request.GameID = 0;
            }
            var response = ServerProvider.MakeRequest<SurrenderResponse>(request);
            string ret;
            switch (response.Status)
            {
                case Statuses.Ok:
                    ret = "";
                    break;
                default:
                    ret = "Wrong status.";
                    break;
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PeaceRequest(string gameId)
        {
            var request = new PeaceRequest { From = User.Identity.Name };
            try
            {
                request.GameID = Int32.Parse(gameId);
            }
            catch (Exception)
            {
                request.GameID = 0;
            }
            var response = ServerProvider.MakeRequest<PeaceResponse>(request);
            string ret;
            switch (response.Status)
            {
                case Statuses.Ok:
                    ret = "";
                    break;
                default:
                    ret = "Wrong status.";
                    break;
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeclinePeaceRequest(string gameId)
        {
            var request = new DeclinePeaceRequest { From = User.Identity.Name };
            try
            {
                request.GameID = Int32.Parse(gameId);
            }
            catch (Exception)
            {
                request.GameID = 0;
            }
            var response = ServerProvider.MakeRequest<DeclinePeaceResponse>(request);
            string ret;
            switch (response.Status)
            {
                case Statuses.Ok:
                    ret = "";
                    break;
                default:
                    ret = "Wrong status.";
                    break;
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AcceptPeaceRequest(string gameId)
        {
            var request = new AcceptPeaceRequest { From = User.Identity.Name };
            try
            {
                request.GameID = Int32.Parse(gameId);
            }
            catch (Exception)
            {
                request.GameID = 0;
            }
            var response = ServerProvider.MakeRequest<AcceptPeaceResponse>(request);
            string ret;
            switch (response.Status)
            {
                case Statuses.Ok:
                    ret = "";
                    break;
                default:
                    ret = "Wrong status.";
                    break;
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
    }
}
