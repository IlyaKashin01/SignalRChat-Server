using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.DTO
{
    public class MemberResponse
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int PersonId { get; set; }
        public int AddedByPerson { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    }
}
