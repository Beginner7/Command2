using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class EchoRequest : Request
    {
        public EchoRequest()
        {
            Command = "echo";
        }

        public string EchoString;
    }
}
