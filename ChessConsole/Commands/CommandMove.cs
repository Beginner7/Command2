using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.GameObjects;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandMove : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("move", "Сделать ход", "<start cell> <target cell>"); } }
        public override int ArgsNeed { get { return 2; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsInGame())
                {
                    var request = new MoveRequest
                    {
                        From = args.ToArray()[0],
                        To = args.ToArray()[1],
                        InWhom = null,
                        Player = CurrentUser.Name,
                        GameId = CurrentUser.CurrentGame.Value
                    };
                    var response = ServerProvider.MakeRequest(request);
                    switch (response.Status)
                    {
                        case Statuses.Ok:
                            Console.WriteLine("Move done.");
                            CurrentUser.LastMove = new Move
                            {
                                From = request.From,
                                InWhom = null,
                                Player = CurrentUser.Name,
                                To = request.To
                            };
                            break;
                        case Statuses.NeedPawnPromotion:
                            Console.WriteLine("This pawn need promotion!");
                            CurrentUser.LastMove = new Move
                            {
                                From = request.From,
                                InWhom = null,
                                Player = CurrentUser.Name,
                                To = request.To
                            };
                            CurrentUser.NeedPawnPromotion = true;
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
        }
    }
}
