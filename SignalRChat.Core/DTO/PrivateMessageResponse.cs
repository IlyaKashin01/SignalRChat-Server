using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Core.Dto
{
    public class PrivateMessageResponse
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }
}