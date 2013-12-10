using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole
{
    public class MoveListProvider
    {
        public List<Move> GetList()
        {
            var request = new MoveListRequest();
            request.Game = CurrentUser.CurrentGame.Value;
            var response = ServerProvider.MakeRequest<MoveListResponse>(request);
            return response.Moves;
        }
    }
}
