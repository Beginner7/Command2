using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public static class CommandGameList
    {
        public static int ArgsNeed = 0;
 
        public static void Show()
        {
            Console.WriteLine("Active games:");
            var request = new GameListRequest();
            var response = ServerProvider.MakeRequest<GameListResponse>(request);
            foreach (int element in response.Games)
            {
                Console.WriteLine(element);
            }
        }
    }
}
