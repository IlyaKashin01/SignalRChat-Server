using Microsoft.EntityFrameworkCore;
using SignalRChat.Data.Repositories.Interfaces;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Impl
{
    public class GroupRepository : BaseRepository<GroupChatRoom>, IGroupRepository
    {
        public GroupRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<GroupChatRoom?>> GetAllGroupAsync(int personId)
        {
            return await _context.Groups
                .Where(x => x.PersonId == personId)
                .Union(_context.GroupMembers.Where(x => x.PersonId == personId).Select(x => x.Group))
                .ToListAsync();
        }

        public async Task<int> GetCreatorIdAsync(int groupId)
        {
            var id = await _context.Groups.Where(x => x.Id == groupId).Select(x => x.PersonId).FirstOrDefaultAsync();
            if (id != 0)
                return id;
            return 0;
        }
        public async Task<GroupMessage?> GetLastGroupMessageAsync(int groupId)
        {
            return await _context.GroupMessages
                                 .Where(x => (x.GroupId == groupId))
                                 .OrderBy(x => x.SentAt)
                                 .LastOrDefaultAsync();

        }
    }
}