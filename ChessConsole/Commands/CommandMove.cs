using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandMove : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("move", "Сделать ход", "<start cell> <target cell>"); } }
        public override int ArgsNeed { get { return 2; } }
        public override bool DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsInGame())
                {
                    var command = new MoveRequest
                    {
                        From = args.ToArray()[0],
                        To = args.ToArray()[1],
                        Player = new User {Name = CurrentUser.Name},
                        GameID = CurrentUser.CurrentGame.Value
                    };
                    var response = ServerProvider.MakeRequest(command);
                    switch (response.Status)
                    {
                        case Statuses.OK:
                            Console.WriteLine("Move done.");
                            break;
                        case Statuses.NoUser:
                            Console.WriteLine("No opponent yet.");
                            break;
                        case Statuses.OpponentTurn:
                            Console.WriteLine("Now is opponent turn.");
                            break;
                        case Statuses.WrongMove:
                            Console.WriteLine("Wrong move.");
                            break;
                        case Statuses.WrongMoveNotation:
                            Console.WriteLine("Wrong move notation.");
                            break;
                        default:
                            Console.WriteLine("Wrong status.");
                            break;
                    }
                }
            }
            return true;
        }
    }
}
