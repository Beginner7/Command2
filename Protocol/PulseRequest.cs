namespace Protocol
{
    public class PulseRequest : Request
    {
        public PulseRequest()
        {
            Command = "pulse";
        }

        public string From;
    }
}
