namespace Protocol
{
    public class JoinGameRequest: Request
    {
        public JoinGameRequest()
        {
            Command = "joingame";
        }
        public int GameID;
        public User NewPlayer;
    }
}
