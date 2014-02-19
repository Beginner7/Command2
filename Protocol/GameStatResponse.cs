using Protocol.Transport;

namespace Protocol
{
    public class GameStatResponse : Response
    {
        public int ID;
        public string PlayerWhite;
        public string PlayerBlack;
        public Side Turn;
        public Act Act;
    }
}
