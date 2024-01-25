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
                .Union(_context.GroupMembers.Where(x => x.PersonId == personId || (x.PersonId == personId && x.DeleteDate != null))
                .Select(x => x.Group))
                .ToListAsync();
        }

        public async Task<int> GetCountMembersInGroupAsync(int groupId)
        {
            return await _context.GroupMembers.Where(x => x.GroupId == groupId).CountAsync();
        }

        public async Task<int> GetCountUnreadMessagesInGroupAsync(int groupId)
        {
            return await _context.GroupMessages.Where(x => x.GroupId == groupId && x.IsCheck == false).CountAsync();
        }

        public async Task<string?> GetCreatorLoginAsync(int groupId)
        {
            var id = await _context.Groups.Where(x => x.Id == groupId).Select(x => x.PersonId).FirstOrDefaultAsync();
            return await _context.Users.Where(x => x.Id == id).Select(x => x.Login).FirstOrDefaultAsync();
        }
        public async Task<GroupMessage?> GetLastGroupMessageAsync(int groupId)
        {
            return await _context.GroupMessages
                                 .Where(x => (x.GroupId == groupId))
                                 .OrderBy(x => x.SentAt)
                                 .LastOrDefaultAsync();

        }

        public async Task<bool> LeaveGroupAsync(int groupId, int personId)
        {
            var deletedPerson = await _context.GroupMembers.FirstOrDefaultAsync(x => x.GroupId == groupId && x.PersonId == personId);

            if (deletedPerson != null)
            {
                deletedPerson.DeleteDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ReturnToGroupAsync(int groupId, int personId)
        {
            var deletedPerson = await _context.GroupMembers.FirstOrDefaultAsync(x => x.GroupId == groupId && x.PersonId == personId);

            if (deletedPerson != null)
            {
                deletedPerson.DeleteDate = null;
                deletedPerson.UpdatedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return false;
            }
            return true;
        }
    }
}