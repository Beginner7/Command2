namespace Protocol
{
    public class AcceptPeaceRequest : Request
    {
        public AcceptPeaceRequest()
        {
            Command = "acceptpeace";
        }

        public string From;
        public int GameID;
    }
}
