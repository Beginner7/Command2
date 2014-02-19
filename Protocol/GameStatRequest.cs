namespace Protocol
{
    public class GameStatRequest : Request
    {
        public GameStatRequest()
        {
            Command = "gamestat";
        }

        public int gameID;
    }
}
