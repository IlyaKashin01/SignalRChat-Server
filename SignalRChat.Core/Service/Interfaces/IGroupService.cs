using SignalRChat.Core.Dto;
using SignalRChat.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Interfaces
{
    public interface IGroupService
    {
        Task<int> CreateGroupAsync(GroupRequest request);
        Task<IEnumerable<GroupResponse>> GetAllGroupsAsync(int personId);
        Task<int> AddPersonToGroup(MemberRequest request);
    }
}
