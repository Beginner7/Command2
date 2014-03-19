namespace Protocol
{
    public class AttackersRequest : Request
    {
        public AttackersRequest()
        {
            Command = "attackers";
        }

        public int Game;
    }
}