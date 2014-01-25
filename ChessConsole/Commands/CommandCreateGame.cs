using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public static class CommandCreateGame
    {
        public static int ArgsNeed = 0;

        public static void Create()
        {
            if (Utils.IsLoggedIn() && Utils.IsNotInGame())
            {
                var request = new CreateGameRequest();
                request.NewPlayer = new User { Name = CurrentUser.Name };
                var response = ServerProvider.MakeRequest<CreateGameResponse>(request);
                if (response.Status == Statuses.OK)
                {
                    Console.WriteLine("You create game. ID: " + response.ID);
                    CurrentUser.CurrentGame = (int?)response.ID;
                }
                else
                {
                    Console.WriteLine("Bad status");
                }
            }
        }
    }
}
