using Protocol;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandEcho : CommandBase
    {
        public override string Name { get { return "echo"; } }
        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<EchoRequest>(request);
            var workResponse = new EchoResponse {EchoString = workRequest.EchoString, Status = Statuses.Ok};
            return workResponse;
        }
    }
}
