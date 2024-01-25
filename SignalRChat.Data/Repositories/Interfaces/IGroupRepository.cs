using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Interfaces
{
    public interface IGroupRepository : IBaseRepository<GroupChatRoom>
    {
        Task<string?> GetCreatorLoginAsync(int groupId);
        Task<IEnumerable<GroupChatRoom?>> GetAllGroupAsync(int personId);
        Task<GroupMessage?> GetLastGroupMessageAsync(int groupId);
        Task<bool> LeaveGroupAsync(int groupId, int personId);
        Task<bool> ReturnToGroupAsync(int groupId, int personId);
        Task<int> GetCountMembersInGroupAsync(int groupId);
        Task<int> GetCountUnreadMessagesInGroupAsync(int groupId);
    }
}