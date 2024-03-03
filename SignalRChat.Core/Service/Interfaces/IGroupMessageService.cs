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
        Task<GroupMessageResponse> SaveGroupMessageAsync(GroupMessageRequest request);
        Task<IEnumerable<GroupMessageResponse>> GetAllGroupMessagesAsync(int groupId, int? personId);
        Task<IEnumerable<GroupMessageResponse>> ChangeStatusIncomingMessagesAsync(int groupId, int senderId);
    }
}
