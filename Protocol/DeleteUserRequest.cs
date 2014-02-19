namespace Protocol
{
    public class DeleteUserRequest : Request
    {
        public DeleteUserRequest()
        {
            Command = "deleteuser";
        }
        public string UserName;
    }
}
