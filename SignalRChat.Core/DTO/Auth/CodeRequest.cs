using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO.Auth
{
    public class CodeRequest
    {
        public string Login { get; set; } = string.Empty;
        public int ResetCode { get; set; }
    }
}
