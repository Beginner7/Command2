using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;
using System.Text.RegularExpressions;

namespace ChessConsole
{
    class ConnectToGameProvider
    {
        public bool Connect(int gameid, string userName)
        {
            var command = new ConnectToGameRequest();
            command.GameID = gameid;
            command.PlayerTwo = new User { Name = userName };
            var response = ServerProvider.MakeRequest(command);
            Console.WriteLine(response.Status.ToString());
            return response.Status == Statuses.OK;
        }
    }
}
