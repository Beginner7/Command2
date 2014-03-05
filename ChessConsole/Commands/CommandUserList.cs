using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandUserList : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("ul", "Список вошедших пользователей"); } }
        public override int ArgsNeed { get { return 0; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                var request = new UserListRequest();
                var response = ServerProvider.MakeRequest<UserListResponse>(request);
                foreach (var element in response.Users)
                {
                    Console.WriteLine(element);
                }
            }
        }
    }
}
