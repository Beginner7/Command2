namespace Protocol
{
    public class PeaceRequest : Request
    {
        public PeaceRequest()
        {
            Command = "peace";
        }

        public string From;
        public int GameID;
    }
}
