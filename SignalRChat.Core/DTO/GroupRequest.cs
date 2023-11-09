using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Core.Dto
{
    public class GroupRequest
    {
        public string Name { get; set; } = string.Empty;
        public int CreatorId { get; set; }
    }
}