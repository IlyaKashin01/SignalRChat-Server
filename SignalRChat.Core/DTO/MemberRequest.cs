namespace SignalRChat.Core.DTO
{
    public class MemberRequest
    {
        public int GroupId { get; set; }
        public int PersonId { get; set; }
        public int AddedByPerson { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
