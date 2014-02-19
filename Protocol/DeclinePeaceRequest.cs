namespace Protocol
{
    public class DeclinePeaceRequest : Request
    {
        public DeclinePeaceRequest()
        {
            Command = "declinepeace";
        }

        public string From;
        public int GameID;
    }
}
