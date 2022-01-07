namespace Challenger.Models
{
    public class GetUserResult
    {
        public UserModel User { get; set; }
        public bool Success { get; set; }
        public MessageType Message { get; set; }
    }
}