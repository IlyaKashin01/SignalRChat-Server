namespace SignalRChat.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Avatar {  get; set; } = string.Empty;
        public List<GroupChatRoom> Groups { get; set; } = new List<GroupChatRoom>();
        public List<PersonalMessage> PersonalMessages{ get; set; } = new List<PersonalMessage>();
        public DateTime RegistrationDate { get; set; }
    }
}
