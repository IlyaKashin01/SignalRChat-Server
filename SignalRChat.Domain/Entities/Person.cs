namespace SignalRChat.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string ConnectionId { get; set; } = string.Empty;
        public List<PersonJoinToGroup> Groups { get; set; } = new List<PersonJoinToGroup>();
        public List<PersonalMessage> SentMessages{ get; set; } = new List<PersonalMessage>();
        public List<PersonalMessage> RecivedMessages{ get; set; } = new List<PersonalMessage>();
    }
}
