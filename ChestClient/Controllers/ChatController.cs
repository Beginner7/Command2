using System.Web.Mvc;
using Newtonsoft.Json;
using Protocol;
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

        public ActionResult SendMessage()
        {
            return Json(Request.Params["Message"] + " received", JsonRequestBehavior.AllowGet);
        }

        public string Name
        {
            get { return "chat"; }
        }

        public void DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<ChatRequest>(request);
            var workResponse = new ChatResponse();
            if (ChessServer.Server.Games.ContainsKey(workRequest.GameID))
            {
                if (ChessServer.Server.Games[workRequest.GameID].PlayerWhite.Name == workRequest.From)
                {
                    User geted;
                    if (
                        ChessServer.Server.Users.TryGetValue(
                            ChessServer.Server.Games[workRequest.GameID].PlayerBlack.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.ChatMessage(workRequest.From, workRequest.SayString));
                    }
                }
                else
                {
                    if (ChessServer.Server.Games[workRequest.GameID].PlayerBlack.Name == workRequest.From)
                    {
                        User geted;
                        if (
                            ChessServer.Server.Users.TryGetValue(
                                ChessServer.Server.Games[workRequest.GameID].PlayerWhite.Name, out geted))
                        {
                            geted.Messages.Add(MessageSender.ChatMessage(workRequest.From, workRequest.SayString));
                        }
                    }
                }
            }
        }
    }
}