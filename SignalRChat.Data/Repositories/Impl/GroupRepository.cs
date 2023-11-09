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
        public async Task<GroupChatRoom?> getGroupByNameAndCreatorAsync(string groupName, int CreatorId)
        {
            return await _context.Groups.FirstOrDefaultAsync(x => x.Name == groupName && x.CreatorId == CreatorId);
        }

        public async Task<GroupChatRoom?> getGroupByNameAndUsersAsync(string groupName, int PersonId)
        {
            return await _context.Groups.Where(x => x.Name == groupName)
                                        .Include(x => x.Users)
                                        .FirstOrDefaultAsync(x => x.Id == PersonId);
        }
    }
}