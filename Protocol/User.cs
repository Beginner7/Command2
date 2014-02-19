using System.Collections.Generic;
using Protocol.Transport.Messages;

namespace Protocol
{
    public class User
    {
        public string Name;
        public int Lostbeats = 0;

        public List<Message> Messages = new List<Message>();
    }
}
