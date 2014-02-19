using System.Collections.Generic;
using System.Web.Mvc;
using ChestClient.Models;
using Protocol.GameObjects;
using Protocol;
using Protocol.Transport;

namespace ChestClient.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/
        private static readonly Dictionary<int, GameModel> Games = new Dictionary<int, GameModel>();
   
        public ActionResult List()
        {
            return Json(Games.Values, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Status()
        {
            var board = new Board();
            board.InitialPosition();
            var request = new MoveListRequest();
            request.Game = int.Parse(Request.Params["gameID"]);
            var response = ServerProvider.MakeRequest<MoveListResponse>(request);

            board.ApplyMoves(response.Moves);
            
            return Json(board.ShowBoardToWeb(), JsonRequestBehavior.AllowGet);
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
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return View("NoAccess");
            }
        }

        public ActionResult StartFree()
        {
            var requestCreateGame = new CreateGameRequest();
            requestCreateGame.NewPlayer = new User { Name = User.Identity.Name };
            var responseCreateGame = ServerProvider.MakeRequest<CreateGameResponse>(requestCreateGame);
            
            int? gameId = null;
            if (responseCreateGame.Status == Statuses.OK)
            {

                gameId = responseCreateGame.ID;
                var requestJoinGame = new JoinGameRequest();
                requestJoinGame.GameID = gameId.Value;
                requestJoinGame.NewPlayer = requestCreateGame.NewPlayer;
                var responseJoinGame = ServerProvider.MakeRequest(requestJoinGame);
                if (responseJoinGame.Status != Statuses.OK)
                {
                    gameId = null;
                }
            }

            return Json(gameId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DoMove()
        {
            var command = new MoveRequest();
            command.From = Request.Params["From"];
            command.To = Request.Params["To"];
            command.Player = new User { Name =User.Identity.Name };
            command.GameID = int.Parse(Request.Params["GameID"]);
            var response = ServerProvider.MakeRequest(command);
            string ret;
            switch (response.Status)
            {
                case Statuses.OK:
                    ret = "";
                    break;
                case Statuses.NoUser:
                    ret = "No opponent yet."; 
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
    }
}
