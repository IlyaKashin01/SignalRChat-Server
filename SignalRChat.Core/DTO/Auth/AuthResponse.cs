using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Dto.Auth
{
#nullable disable
    public class AuthResponse
    {
        public PersonResponse Person { get; set; }
        public string Token { get; set; }
    }
}
