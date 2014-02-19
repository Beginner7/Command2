namespace Protocol
{
    public class DisconnectRequest: Request
    {
        public DisconnectRequest()
        {
            Command = "disconnect";
        }
        public string User;
        public int GameID;
    }
}
