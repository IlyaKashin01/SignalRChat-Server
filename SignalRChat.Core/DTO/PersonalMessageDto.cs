using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Dto
{
    public class PersonalMessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; } 
        public string SenderLogin { get; set; } = string.Empty;
        public int RecipientId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public bool IsCheck {  get; set; }
        public bool IsNewDialog { get; set; }
    }
}
