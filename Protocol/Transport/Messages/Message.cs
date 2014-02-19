namespace Protocol.Transport.Messages
{
    public class Message
    {
        public string Text;
        public MessageType Type;

        public Message(string text, MessageType type)
        {
            Text = text;
            Type = type;
        }
    }
}
