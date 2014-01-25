using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Transport.Messages
{
    public enum MessageType
    {
        OpponentLostConnection = 1,
        OpponentAbandonedGame = 2,
        OpponentJoinedGame = 3,
        ChatMessage = 4,
        OpponentMove = 5
    }
}
