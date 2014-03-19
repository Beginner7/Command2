namespace Protocol
{
    public class StopRequest : Request
    {
        public StopRequest()
        {
            Command = "stop";
        }

        public string UserName;
    }
}
