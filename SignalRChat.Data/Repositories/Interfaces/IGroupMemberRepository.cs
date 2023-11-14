using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Interfaces
{
    public interface IGroupMemberRepository: IBaseRepository<GroupMember>
    {
        Task<IEnumerable<GroupMember>> GetAllGroupMembersAsync(int groupId);
        Task<GroupMember?> FindGroupMemberByPersonIdAsync(int groupId, int personId);
    }
}
