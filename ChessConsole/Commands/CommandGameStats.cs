using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandGameStats : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("gs", "Отобразить состояния игры"); } }
        public override int ArgsNeed { get { return 0; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.IsInGame())
            {
                if (Utils.CheckArgs(ArgsNeed, args.Count()))
                {
                    var reques = new GameStatRequest {gameID = CurrentUser.CurrentGame.Value};
                    var response = ServerProvider.MakeRequest<GameStatResponse>(reques);
                    Console.WriteLine("Game \"" + response.ID + "\" stats:");
                    switch (response.Act)
                    {
                        case Act.AbandonedByWhite:
                            Console.WriteLine("Was abandoned by White");
                            break;

                        case Act.AbandonedByBlack:
                            Console.WriteLine("Was abandoned by Black");
                            break;

                        case Act.Cancled:
                            Console.WriteLine("Was cancled");
                            break;

                        case Act.WaitingOpponent:
                            Console.WriteLine("Waiting for 2nd player");
                            break;

                        case Act.Pat:
                            Console.WriteLine("Finished with pat");
                            break;

                        case Act.WhiteWon:
                            Console.WriteLine("Won by White");
                            break;

                        case Act.BlackWon:
                            Console.WriteLine("Won by Black");
                            break;

                        case Act.InProgress:
                            Console.WriteLine("Now in progress");
                            break;

                        default:
                            Console.WriteLine("Unexpected act");
                            break;
                    }
                    Console.WriteLine("White player: " + response.PlayerWhite);
                    Console.WriteLine("Black player: " + response.PlayerBlack);
                    Console.WriteLine("Now " + ((response.Turn == Side.BLACK) ? "black's" : "white's") + " turn");
                }
            }
        }
    }
}
