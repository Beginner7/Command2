using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class AddUserRequest : Request
    {
        public AddUserRequest()
        {
            Command = "adduser";
        }
        public string UserName;
    }
}
