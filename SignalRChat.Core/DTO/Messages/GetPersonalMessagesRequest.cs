using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO.Messages
{
    public class GetPersonalMessagesRequest
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
    }
}
