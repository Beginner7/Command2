using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Transport.Messages
{
    public class Message
    {
        public string Text;
        public MessageType Type;

        public Message(string text, MessageType type)
        {
            Text = text;
            Type = type;
        }
    }
}
