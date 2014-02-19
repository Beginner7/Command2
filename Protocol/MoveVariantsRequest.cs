namespace Protocol
{
    public class MoveVariantsRequest : Request
    {
        public const string command = "moveVariants";

        public MoveVariantsRequest()
        {
            Command = command;
        }

        public string Cell;
        public int GameID;
    }
}
