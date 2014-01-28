using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.GameObjects;

namespace ChessConsole.Commands
{
    public class CommandShowBoard : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("sb", "Отобразить доску"); } }
        public override int ArgsNeed { get { return 0; } }

        public void ShowBoard()
        {
            if (Utils.IsInGame())
            {
                var gameboard = new Board();
                gameboard.InitialPosition();
                var commandMoveList = new CommandMoveList();
                gameboard.ApplyMoves(commandMoveList.GetList());
                gameboard.ShowBoard();
            }
        }
    }
}
