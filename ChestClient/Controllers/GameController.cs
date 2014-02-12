using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            Board board = new Board();
            board.InitialPosition();
            return Json(board.ShowBoardToWeb(), JsonRequestBehavior.AllowGet);
        }
    }
}
