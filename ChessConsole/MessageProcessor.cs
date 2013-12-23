using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Protocol;
using Protocol.Transport;

namespace ChessConsole
{
    public static class MessageProcessor
    {
        public static void Process(Message Message)
        {
            Console.WriteLine("Empty message?!");
        }

        public static void Process(MessageChat Message)
        {
            Console.WriteLine(Message.ChatString);
        }
    }
}
