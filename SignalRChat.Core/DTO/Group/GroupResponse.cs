using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO.Group
{
    public class GroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PersonId { get; set; }
    }
}
