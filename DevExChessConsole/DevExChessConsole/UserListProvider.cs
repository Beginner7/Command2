using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole
{
    public class UserListProvider
    {
        public IReadOnlyCollection<string> GetList()
        {
            var request = new UserListRequest();
            var response = ServerProvider.MakeRequest<UserListResponse>(request);
            return response.Users;
        }
    }
}