using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public static class CommandJoinGame
    {
        public static int ArgsNeed = 1;

        public static void Join(string gameIdString)
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
