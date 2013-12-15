using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Protocol
{
    public class UserListResponse : Response
    {
        public string[] Users;
    }
}
