namespace Protocol
{
    public class SurrenderRequest : Request
    {
        public SurrenderRequest()
        {
            Command = "surrender";
        }

        public string From;
        public int GameID;
    }
}
