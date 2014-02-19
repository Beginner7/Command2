namespace Protocol
{
    public class EchoRequest : Request
    {
        public EchoRequest()
        {
            Command = "echo";
        }

        public string EchoString;
    }
}
