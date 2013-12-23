using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class ChatRequest : Request
    {
        public ChatRequest()
        {
            Command = "chat";
        }

        public string ChatString;
        public string From;
        public int? GameID;
    }
}
