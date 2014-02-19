namespace Protocol
{
    public class MoveListRequest: Request
    {
        public MoveListRequest()
        {
            Command = "movelist";
        }

        public int Game;
    }
}
