using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO.Group
{
    public class LeaveGroupRequest
    {
        public int groupId { get; set; }
        public int personId { get; set; }
        public string personLogin {  get; set; } = string.Empty;
        public string creatorLogin { get; set; } = string.Empty;
        public bool IsExcluded { get; set; }
    }
}
