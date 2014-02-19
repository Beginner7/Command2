using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using Protocol.GameObjects;
using ChessServer.GameLogic;
using Protocol.Transport;

namespace ChessServer.Commands
{
    public class CommandMoveVariants: CommandBase
    {
        public override string Name
        {
            get
            {
                return MoveVariantsRequest.command;
            }
        }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var moveVariantsRequest = JsonConvert.DeserializeObject<MoveVariantsRequest>(request);
            var moveVariantsResponse = new MoveVariantsResponse();

            Board board = new Board();
            var moveListRequest = new MoveListRequest();
            var moveListResponse = ServerProvider.MakeRequest<MoveListResponse>(moveListRequest);
            board.ApplyMoves(moveListResponse.Moves);
            AttackMap map = new AttackMap(moveListResponse.Moves);
            moveVariantsResponse.Cells = map.MoveVariants(moveVariantsRequest.Cell);
            moveVariantsResponse.Status = Statuses.OK;
            return moveVariantsResponse;
        }
    }
}
