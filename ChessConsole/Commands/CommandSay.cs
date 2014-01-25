using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public static class CommandSay
    {
        public static int ArgsNeed = -1;

        public static void Say(string sayString)
        {
            if (Utils.IsInGame())
            {
                var request = new ChatRequest();
                request.SayString = sayString;
                request.From = CurrentUser.Name;
                request.GameID = CurrentUser.CurrentGame.Value;
                var response = ServerProvider.MakeRequest<ChatResponse>(request);
                if (response.Status != Statuses.OK)
                {
                    Console.WriteLine("Bad status.");
                }
            }
        }
    }
}
