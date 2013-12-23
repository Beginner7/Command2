using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Protocol;
using Protocol.Transport;

namespace ChessConsole
{
    public class ChatProvider
    {
        public void Say(string InputString)
        {
            var command = new ChatRequest();
            command.ChatString = InputString;
            command.From = CurrentUser.Name;
            command.GameID = CurrentUser.CurrentGame;
            var response = ServerProvider.MakeRequest<ChatResponse>(command);
            if (response.Status != Statuses.OK)
            {
                Console.WriteLine("Error.");
            }
        }
    }
}
