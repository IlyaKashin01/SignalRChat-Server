using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO.Auth
{
    public class TelegramAuthRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public long AuthDate { get; set; }
        public string Hash { get; set; } = string.Empty;
    }
}
