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
    class DisconnectProvider
    {
        public bool Disconnect()
        {
            var command = new DisconnectRequest();
            command.User = CurrentUser.Name;
            command.GameID = CurrentUser.CurrentGame.Value;
            var response = ServerProvider.MakeRequest(command);
            Console.WriteLine(response.Status.ToString());
            return response.Status == Statuses.OK;
        }
    }
}
