using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandJoinGame : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("jg", "Присоединиться к игре по номеру", "<game id>"); } }
        public override int ArgsNeed { get { return 1; } }

        public void Join(string gameIdString)
        {
            if (Utils.IsLoggedIn() && Utils.IsNotInGame())
            {
                try
                {
                    int gameId = Convert.ToInt32(gameIdString);
                    var request = new ConnectToGameRequest();
                    request.GameID = gameId;
                    request.NewPlayer = new User { Name = CurrentUser.Name };
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
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Game id should be integer number.");
                }
            }
        }
    }
}
