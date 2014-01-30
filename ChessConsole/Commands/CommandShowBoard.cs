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
        public override bool DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
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
            return true;
        }
    }
}
