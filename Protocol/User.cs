using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class User
    {
        public string Name;
        public int lostbeats = 0;

        public List<Protocol.Transport.Message> Messages = new List<Protocol.Transport.Message>();
    }
}
