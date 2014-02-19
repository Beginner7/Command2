namespace Protocol
{
    public class CreateGameRequest: Request
    {
        public CreateGameRequest()
        {
            Command = "creategame";
        }
        public User NewPlayer;
    }
}
