namespace Protocol
{
    public class MoveVariantsRequest : Request
    {
        public MoveVariantsRequest()
        {
            Command = "moveVariants";
        }

        public string Cell;
    }
}
