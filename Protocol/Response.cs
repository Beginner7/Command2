using System.Collections.Generic;
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