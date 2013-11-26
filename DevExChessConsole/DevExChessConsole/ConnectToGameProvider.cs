using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using ChessConsole.Transport;

namespace ChessConsole
{
    public class ConnectToGameProvider
    {
        public IReadOnlyCollection<string> GetList()
        {
            var request = new ConnectToGameRequest();
            var response = ServerProvider.MakeRequest<ConnectToGameResponse>(request);
            return response.Games;
        }
    }
}
