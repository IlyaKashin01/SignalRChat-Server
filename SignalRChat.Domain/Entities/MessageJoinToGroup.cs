using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Entities
{
    public class MessageJoinToGroup: BaseEntity
    {
        public GroupChatRoom? Group { get; set; }
        public int GroupId { get; set; }
        public GroupMessage? Message { get; set; }
        public int MessageId { get; set; }
        
    }
}
