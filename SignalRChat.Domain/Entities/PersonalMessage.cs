using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Entities
{
    public class PersonalMessage: BaseEntity
    {
        public int SenderId { get; set; }
        public Person Recipient { get; set; } = new Person();
        public int RecipientId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public bool IsCheck { get; set; }
    }
}
