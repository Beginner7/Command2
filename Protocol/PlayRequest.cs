namespace Protocol
{
    public class PlayRequest : Request
    {
        public PlayRequest()
        {
            Command = "play";
        }

        public string UserName;
    }
}
