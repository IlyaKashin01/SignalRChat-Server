using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO.Messages
{
    public class PersonalMessageRequest
    {
        public string PersonLogin { get; set; } = string.Empty;
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsNewDialog { get; set; }

    }
}
