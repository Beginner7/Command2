namespace Protocol.Transport.Messages
{
    public static class MessageSender
    {
        public static Message YouWin()
        {
            return new Message(null, MessageType.YouWin);
        }

        public static Message YouLoose()
        {
            return new Message(null, MessageType.YouLoose);
        }

        public static Message Pat()
        {
            return new Message(null, MessageType.Pat);
        }

        public static Message CheckToOpponent()
        {
            return new Message(null, MessageType.CheckToOpponent);
        }

        public static Message CheckToYou()
        {
            return new Message(null, MessageType.CheckToYou);
        }

        public static Message OpponentSurrendered()
        {
            return new Message(null, MessageType.OpponentSurrendered);
        }

        public static Message OpponentAcceptedPeace()
        {
            return new Message(null, MessageType.OpponentAcceptedPeace);
        }

        public static Message OpponentDeclinedPeace()
        {
            return new Message(null, MessageType.OpponentDeclinedPeace);
        }

        public static Message OpponentRequestPeace()
        {
            return new Message(null, MessageType.OpponentRequestPeace);
        }

        public static Message GameDraw()
        {
            return new Message(null, MessageType.GameDraw);
        }

        public static Message OpponentLostConnection()
        {
            return new Message(null, MessageType.OpponentLostConnection);
        }

        public static Message OpponentAbandonedGame()
        {
            return new Message(null, MessageType.OpponentAbandonedGame);
        }

        public static Message OpponentJoinedGame()
        {
            return new Message(null, MessageType.OpponentJoinedGame);
        }

        public static Message ChatMessage(string from, string message)
        {
            return new Message(from + " said: " + message, MessageType.ChatMessage);
        }

        public static Message OpponentMove(string from, string to)
        {
            return new Message(from + '-' + to, MessageType.OpponentMove);
        }
    }
}
