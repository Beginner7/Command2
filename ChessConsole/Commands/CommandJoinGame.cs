using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandJoinGame : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("jg", "Присоединиться к игре по номеру", "<game id>"); } }
        public override int ArgsNeed { get { return 1; } }
        public override bool DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsLoggedIn() && Utils.IsNotInGame())
                {
                    try
                    {
                        int gameId = Convert.ToInt32(args.ToArray()[0]);
                        var request = new JoinGameRequest
                        {
                            GameID = gameId,
                            NewPlayer = new User {Name = CurrentUser.Name}
                        };
                        var response = ServerProvider.MakeRequest(request);
                        if (response.Status == Statuses.OK)
                        {
                            Console.WriteLine("You joined game. ID: " + gameId);
                            CurrentUser.CurrentGame = gameId;
                        }
                        else
                        {
                            if (response.Status == Statuses.GameIsRunning)
                            {
                                Console.WriteLine("Game is already running.");
                            }
                            if (response.Status == Statuses.GameNotFound)
                            {
                                Console.WriteLine("Game does not not exist.");
                            }
                            if (response.Status == Statuses.GameCancled)
                            {
                                Console.WriteLine("Game was cancled by initiator.");
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Game id should be integer number.");
                    }
                }
            }
            return true;
        }
    }
}
