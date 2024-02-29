using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Entities
{
    public class GroupMessage: BaseEntityMessage
    {
        public GroupChatRoom Group { get; set; } = new GroupChatRoom();
        public int GroupId { get; set; }
    }
}
