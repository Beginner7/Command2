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
            var workResponse = new GameStatResponse {ID = workRequest.gameID, Act = Server.Games[workRequest.gameID].Act,
            EatedWhites = Server.Games[workRequest.gameID].EatedWhites, EatedBlacks = Server.Games[workRequest.gameID].EatedBlacks};
            if (Server.Games[workRequest.gameID].PlayerBlack != null)
            {
                workResponse.PlayerBlack = Server.Games[workRequest.gameID].PlayerBlack.Name;
            }
            if (Server.Games[workRequest.gameID].PlayerWhite != null)
            {
                workResponse.PlayerWhite = Server.Games[workRequest.gameID].PlayerWhite.Name;
            }
            workResponse.Turn = Server.Games[workRequest.gameID].Turn;
            workResponse.Status = Statuses.Ok;
            return workResponse;
        }
    }
}
