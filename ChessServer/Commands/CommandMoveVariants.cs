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

            Game game = games[moveVariantsRequest.GameID];
            
            var map = new AttackMap(game.Moves);
            if (map.SourceBoard[moveVariantsRequest.Cell].Side == game.Turn)
            {
                moveVariantsResponse.Cells = map.MoveVariants(moveVariantsRequest.Cell);
            }
            else
            {
                moveVariantsResponse.Cells = new List<string>();
            }
            moveVariantsResponse.Status = Statuses.OK;
            return moveVariantsResponse;
        }
    }
}
