using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Entities
{
    public class GroupMessage: BaseEntity
    {
        public int GroupId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public bool IsCheck { get; set; }
    }
}
