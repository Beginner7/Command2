using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public static class CommandDisconnect
    {
        public static int ArgsNeed = 0;

        public static void Disconnect()
        {
            if (Utils.IsInGame())
            {
                var request = new DisconnectRequest();
                request.User = CurrentUser.Name;
                request.GameID = CurrentUser.CurrentGame.Value;
                var response = ServerProvider.MakeRequest(request);
                if (response.Status == Statuses.OK)
                {
                    Console.WriteLine("You abandoned the game.");
                    CurrentUser.CurrentGame = null;
                }
                else
                {
                    Console.WriteLine(response.Status.ToString());
                }
            }
        }
    }
}
