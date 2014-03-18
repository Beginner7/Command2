using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using ChestClient.Models;
using Protocol.GameObjects;
using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;

namespace ChestClient.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/
        private static readonly Dictionary<int, GameModel> Games = new Dictionary<int, GameModel>();

        public List<Message> Messages = new List<Message>();
   
        public ActionResult List()
        {
            return Json(Games.Values, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Status()
        {
            var board = new Board();
            board.InitialPosition();
            string res;
            var request = new MoveListRequest {Game = int.Parse(Request.Params["gameID"])};
            var response = ServerProvider.MakeRequest<MoveListResponse>(request);
            var request2 = new GameStatRequest {gameID = int.Parse(Request.Params["gameID"])};
            var response2 = ServerProvider.MakeRequest<GameStatResponse>(request2);

            board.ApplyMoves(response.Moves);

            switch (response2.Act)
            {
                case Act.AbandonedByWhite:
                    res = ("Was abandoned by White");
                    break;

                case Act.AbandonedByBlack:
                    res = ("Was abandoned by Black");
                    break;

                case Act.Cancled:
                    res = ("Was cancled");
                    break;

                case Act.WaitingOpponent:
                    res = ("Waiting for 2nd player");
                    break;

                case Act.Pat:
                    res = ("Finished with pat");
                    break;

                case Act.WhiteWon:
                    res = ("Won by White");
                    break;

                case Act.BlackWon:
                    res = ("Won by Black");
                    break;

                case Act.InProgress:
                    res = ("Now in progress");
                    break;

                default:
                    res = ("Unexpected act");
                    break;
            }
            
            return Json(new {DataBoard = board.ShowBoardToWeb(), DataMoves = response.Moves, DataStatus = response2.Act, DataTextStatus = res,
                DataWhitePlayer = response2.PlayerWhite, DataBlackPlayer = response2.PlayerBlack, DataTurn = response2.Turn,
                EatedWhites = response2.EatedWhites, EatedBlacks = response2.EatedBlacks}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MoveVariants()
        {
            var board = new Board();
            board.InitialPosition();

            var request = new MoveVariantsRequest { Cell = Request.Params["cell"], GameID = int.Parse(Request.Params["gameID"]) };
            var response = ServerProvider.MakeRequest<MoveVariantsResponse>(request);
                        
            //request.Params["cell"];
            return Json(response.Cells, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Free()
        {
            return View();
        }

        public ActionResult StartFree()
        {
            User newPlayer = null;
            if (Request.IsAuthenticated)
            {
                newPlayer = new User { Name = User.Identity.Name };
            }
            var requestCreateGame = new CreateGameRequest { NewPlayer = newPlayer };
            var responseCreateGame = ServerProvider.MakeRequest<CreateGameResponse>(requestCreateGame);
            
            int? gameId = null;
            if (responseCreateGame.Status == Statuses.Ok)
            {

                gameId = responseCreateGame.ID;
                FormsAuthentication.SetAuthCookie(responseCreateGame.FirstPlayer.Name, false);
                var requestJoinGame = new JoinGameRequest
                {
                    GameID = gameId.Value,
                    NewPlayer = responseCreateGame.FirstPlayer
                };
                var responseJoinGame = ServerProvider.MakeRequest(requestJoinGame);
                if (responseJoinGame.Status != Statuses.Ok)
                {
                    gameId = null;
                }
            }

            return Json(gameId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DoMove()
        {
            var command = new MoveRequest
            {
                From = Request.Params["From"],
                To = Request.Params["To"],
                Player = User.Identity.Name,
                GameId = int.Parse(Request.Params["GameID"]),
                InWhom = Request.Params["InWhom"],
            };
            var response = ServerProvider.MakeRequest(command);
            string ret;
            switch (response.Status)
            {
                case Statuses.Ok:
                    ret = "";
                    break;
                case Statuses.NeedPawnPromotion:
                    ret = "";
                    break;
                case Statuses.NoUser:
                    ret = "No opponent yet."; 
                    break;
                case Statuses.NotAuthorized:
                    ret = "You not authorized."; 
                    break;
                case Statuses.OpponentTurn:
                    ret = "Now is opponent turn.";
                    break;
                case Statuses.WrongMove:
                    ret = "Wrong move.";
                    break;
                case Statuses.WrongMoveNotation:
                    ret = "Wrong move notation.";
                    break;
                default:
                    ret = "Wrong status.";
                    break;
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
