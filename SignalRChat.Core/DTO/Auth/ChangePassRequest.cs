using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO.Auth
{
    public class ChangePassRequest
    {
        public string Login { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
