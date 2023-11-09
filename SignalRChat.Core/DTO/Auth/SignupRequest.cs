namespace SignalRChat.Core.Dto.Auth
{
    public class SignupRequest
    {
        public string Login { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        //public IFormFile? Avatar { get; set; }
    }
}
