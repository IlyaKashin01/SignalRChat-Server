using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Entities
{
    public class PersonJoinToGroup : BaseEntity
    {
        public Person? Person { get; set; }
        public int PersonId { get; set; }
        public GroupChatRoom? Group { get; set; }
        public int GroupId { get; set; }
    }
}
