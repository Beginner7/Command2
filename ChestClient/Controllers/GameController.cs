using System.Collections.Generic;
using System.Web.Mvc;
using ChestClient.Models;
using Protocol.GameObjects;

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
            return Json(board.ShowBoardToWeb(), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult MoveVariants()
        //{
        //    Board board = new Board();
        //    board.InitialPosition();

        //    //var request = new UserListRequest();
        //    //var response = ServerProvider.MakeRequest<UserListResponse>(request);

        //    //Request.Params["cell"];
        //}
    }
}
