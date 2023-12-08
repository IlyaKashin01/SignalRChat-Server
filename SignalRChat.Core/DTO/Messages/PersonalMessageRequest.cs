﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO.Messages
{
    public class PersonalMessageRequest
    {
        public int SenderId { get; set; }
        public string SenderLogin { get; set; } = string.Empty;
        public int RecipientId { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsNewDialog { get; set; }

    }
}
