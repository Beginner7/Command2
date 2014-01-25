using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Transport.Messages
{
    public static class MessageSander
    {
        public static Message OpponentLostConnection()
        {
            return new Message(null, MessageType.OpponentLostConnection);
        }

        public static Message OpponentAbandonedGame()
        {
            return new Message(null, MessageType.OpponentAbandonedGame);
        }

        public static Message OpponentJoinedGame()
        {
            return new Message(null, MessageType.OpponentJoinedGame);
        }

        public static Message ChatMessage(string from, string message)
        {
            return new Message(from + " said: " + message, MessageType.ChatMessage);
        }

        public static Message OpponentMove(string from, string to)
        {
            return new Message(from + '-' + to, MessageType.OpponentMove);
        }
    }
}
