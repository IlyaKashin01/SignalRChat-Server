using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO.Messages
{
    public class GroupMessageRequest
    {
        public int GroupId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
