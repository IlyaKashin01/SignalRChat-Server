namespace SignalRChat.Core.DTO.Group
{
    public class ReturnGroupRequest
    {
        public int groupId { get; set; } = 0;
        public string groupName { get; set; } = string.Empty;
        public int personId { get; set; } = 0;
        public string personLogin { get; set; } = string.Empty;
    }
}
