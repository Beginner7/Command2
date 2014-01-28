using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandUserList : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("ul", "Список вошедших пользователей"); } }
        public override int ArgsNeed { get { return 0; } }

        public void Show()
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
