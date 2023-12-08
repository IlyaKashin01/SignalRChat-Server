using SignalRChat.Core.DTO;
using SignalRChat.Core.DTO.Group;
using SignalRChat.Core.DTO.Members;
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
        Task<IEnumerable<Dialog>> GetAllGroupsAsync(int personId);
        Task<int> AddPersonToGroupAsync(MemberRequest request);
        Task<GroupResponse> GetGroupByIdAsync(int groupId);
        Task<string> GetCreatorLoginAsync(int groupId);
        Task<IEnumerable<MemberInGroup>> GetAllMembersInGroupAsync(int groupId);
    }
}
