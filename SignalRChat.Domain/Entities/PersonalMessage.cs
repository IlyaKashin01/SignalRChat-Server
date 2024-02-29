namespace SignalRChat.Domain.Entities
{
    public class PersonalMessage: BaseEntityMessage
    {
        public Person Recipient { get; set; } = new Person();
        public int RecipientId { get; set; }
    }
}
