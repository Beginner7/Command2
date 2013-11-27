using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole
{
    public class GameListProvider
    {
        public IReadOnlyCollection<int> GetList()
        {
            var request = new GameListRequest();
            var response = ServerProvider.MakeRequest<GameListResponse>(request);
            return response.Games;
        }
    }
}
