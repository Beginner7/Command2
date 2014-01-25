using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public static class CommandUserList
    {
        public static int ArgsNeed = 0;

        public static void Show()
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
