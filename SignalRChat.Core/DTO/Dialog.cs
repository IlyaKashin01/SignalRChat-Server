﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO
{
    public class Dialog
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string LastMessage { get; set; } = string.Empty;
        public bool IsCheck { get; set; }
        public DateTime SentAt { get; set; }
        public int CountUnreadMessages { get; set; }
        public string SenderLogin { get; set; } = string.Empty;
        public bool IsGroup { get; set; }
        public int? CountMembers { get; set; }
        public string? CreatorLogin {  get; set; }
    }
}
