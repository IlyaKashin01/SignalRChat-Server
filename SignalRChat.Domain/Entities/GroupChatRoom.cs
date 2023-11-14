namespace SignalRChat.Domain.Entities
{
    public class GroupChatRoom : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public Person Person { get; set; } = new Person();
        public int PersonId { get; set; }
        public List<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();
    }
}
