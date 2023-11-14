using SignalRChat.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Interfaces
{
    public interface IGroupMessageService
    {
        Task<int> SaveGroupMessageAsync(GroupMessageDto request);
        Task<IEnumerable<GroupMessageDto>> GetAllGroupMessagesAsync(int groupId);
    }
}
