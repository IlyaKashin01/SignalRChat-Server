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

        public async Task<GroupChatRoom?> getGroupByNameAndCreatorAsync(string groupName, int CreatorId)
        {
            return await _context.Groups.FirstOrDefaultAsync(x => x.Name == groupName && x.PersonId == CreatorId);
        }

        public async Task<GroupChatRoom?> getGroupByNameAndUsersAsync(string groupName, int PersonId)
        {
            return await _context.Groups.Where(x => x.Name == groupName)
                                        .FirstOrDefaultAsync(x => x.Id == PersonId);
        }
    }
}