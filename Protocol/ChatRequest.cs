namespace Protocol
{
    public class ChatRequest : Request
    {
        public ChatRequest()
        {
            Command = "chat";
        }

        public string SayString;
        public string From;
        public int GameID;
    }
}
