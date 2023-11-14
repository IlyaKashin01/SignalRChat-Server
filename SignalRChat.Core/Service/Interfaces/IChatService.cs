using SignalRChat.Core.Dto;
using SignalRChat.Core.DTO;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Services.Interfaces
{
    public interface IChatService
    {
        Task<int> SendMessageAsync(PersonalMessageDto request);
        Task<int> SendMessageToGroupAsync(GroupMessageDto request);
        Task<int> JoinGroupAsync(GroupRequest request);
        Task LeaveGroupAsync(int userId, string groupName);
        Task<IEnumerable<Person?>> GetUsersInGroupAsync(int groupId);
        Task<IEnumerable<PrivateMessageResponse>> GetMessagesBetweenUsersAsync(int user1Id, int user2Id);
        Task<IEnumerable<PrivateMessageResponse>> GetMessagesInGroupAsync(int groupId);
        Task<bool> DeleteMessageAsync(int messageId);
    }
}
