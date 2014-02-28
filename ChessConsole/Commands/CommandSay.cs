using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandSay : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("say", "Отправить сообщение оппоненту", "<message>"); } }
        public override int ArgsNeed { get { return -1; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsInGame())
                {
                    var request = new ChatRequest
                    {
                        SayString = args.StrJoin(' '),
                        From = CurrentUser.Name,
                        GameID = CurrentUser.CurrentGame.Value
                    };
                    var response = ServerProvider.MakeRequest<ChatResponse>(request);
                    if (response.Status != Statuses.Ok)
                    {
                        Console.WriteLine("Bad status.");
                    }
                }
            }
        }
    }
}
