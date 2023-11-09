namespace SignalRChat.Domain.Entities
{
    public class GroupChatRoom : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int CreatorId { get; set; }
        public List<PersonJoinToGroup> Users { get; set; } = new List<PersonJoinToGroup>();
        public List<MessageJoinToGroup> Messages { get; set; } = new List<MessageJoinToGroup>();
    }
}
