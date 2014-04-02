using Protocol;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandGameStat : CommandBase
    {
        public override string Name { get { return "gamestat"; } }
        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<GameStatRequest>(request);
            GameObject game = Server.Games[workRequest.gameID];
            var workResponse = new GameStatResponse {ID = workRequest.gameID, Act = game.Act,
            EatedWhites = game.EatedWhites, EatedBlacks = game.EatedBlacks};
            if (game.PlayerBlack != null)
            {
                workResponse.PlayerBlack = game.PlayerBlack.Name;
            }
            if (game.PlayerWhite != null)
            {
                workResponse.PlayerWhite = game.PlayerWhite.Name;
            }
            workResponse.Turn = game.Turn;
            workResponse.Status = Statuses.Ok;
            return workResponse;
        }
    }
}
