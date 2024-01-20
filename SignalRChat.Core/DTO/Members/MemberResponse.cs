using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO.Members
{
    public class MemberResponse
    {
        public string CreatorLogin { get; set; } = string.Empty;
        public List<MemberInGroup> GroupMembers { get; set; } = new List<MemberInGroup>();
    }
    public class MemberInGroup
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int PersonId { get; set; }
        public string MemberLogin { get; set; } = string.Empty;
        public string AddedByPersonLogin { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    }
}
