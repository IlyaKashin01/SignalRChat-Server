using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Interfaces
{
    public interface IGroupRepository : IBaseRepository<GroupChatRoom>
    {
        public Task<GroupChatRoom?> getGroupByNameAndCreatorAsync(string groupName, int CreatorId);
        public Task<GroupChatRoom?> getGroupByNameAndUsersAsync(string groupName, int PersonId);
        Task<IEnumerable<GroupChatRoom?>> GetAllGroupAsync(int personId);
    }
}