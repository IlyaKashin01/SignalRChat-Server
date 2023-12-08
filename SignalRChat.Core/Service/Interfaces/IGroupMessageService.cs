using SignalRChat.Core.DTO.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Interfaces
{
    public interface IGroupMessageService
    {
        Task<int> SaveGroupMessageAsync(GroupMessageResponse request);
        Task<IEnumerable<GroupMessageResponse>> GetAllGroupMessagesAsync(int groupId);
    }
}
