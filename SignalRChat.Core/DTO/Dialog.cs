using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO
{
    public class Dialog
    {
        public string Name { get; set; } = string.Empty;
        public string LastMessage { get; set; } = string.Empty;
        public bool IsCheck { get; set; }
        public DateTime DateTime { get; set; }
    }
}
