using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.GameObjects;

namespace ChessConsole.Commands
{
    public static class CommandShowBoard
    {
        public static int ArgsNeed = 0;

        public static void ShowBoard()
        {
            if (Utils.IsInGame())
            {
                var gameboard = new Board();
                gameboard.InitialPosition();
                gameboard.ApplyMoves(CommandMoveList.GetList());
                gameboard.ShowBoard();
            }
        }
    }
}
