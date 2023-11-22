using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Interfaces
{
    public interface IGroupRepository : IBaseRepository<GroupChatRoom>
    {
        Task<int> GetCreatorIdAsync(int groupId);
        Task<IEnumerable<GroupChatRoom?>> GetAllGroupAsync(int personId);
    }
}