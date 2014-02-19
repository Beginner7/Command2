using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandGameList : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("gl", "Список игр"); } }
        public override int ArgsNeed { get { return 0; } }
        public override bool DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                Console.WriteLine("Active games:");
                var request = new GameListRequest();
                var response = ServerProvider.MakeRequest<GameListResponse>(request);
                foreach (int element in response.Games)
                {
                    Console.WriteLine(element);
                }
            }
            return true;
        }
    }
}
