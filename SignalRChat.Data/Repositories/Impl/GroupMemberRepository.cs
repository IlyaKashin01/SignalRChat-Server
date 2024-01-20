using Microsoft.EntityFrameworkCore;
using SignalRChat.Data.Repositories.Interfaces;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Impl
{
    public class GroupMemberRepository : BaseRepository<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<GroupMember?> FindGroupMemberByPersonIdAsync(int groupId, int personId)
        {
            return await _context.GroupMembers.FirstOrDefaultAsync(x => x.GroupId == groupId && x.PersonId == personId && x.DeleteDate == null); 
        }

        public async Task<IEnumerable<GroupMember>> GetAllGroupMembersAsync(int groupId)
        {
            return await _context.GroupMembers.Where(x => x.GroupId == groupId && x.DeleteDate == null).ToListAsync();
        }
    }
}
