using System.Linq;
using Protocol;

namespace ChessServer.Commands
{
    public class CommandGamelist : CommandBase
    {
        public override string Name { get { return "gamelist"; } }
        public override Response DoWork(string request)
        {
            var workResponse = new GameListResponse {Games = Server.Games.Keys.ToArray(), Status = Statuses.Ok};
            return workResponse;
        }
    }
}
