using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport.Messages;

namespace Protocol
{
    public class Response
    {
        public string RequestCommand;
        public Statuses Status;
        public List<Message> Messages = new List<Message>();
    }
}