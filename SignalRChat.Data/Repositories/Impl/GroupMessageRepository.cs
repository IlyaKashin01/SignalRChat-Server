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
    public class GroupMessageRepository: BaseRepository<GroupMessage> ,IGroupMessageRepository
    {
        public GroupMessageRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<GroupMessage>> GetAllMessageInGroupAsync(int groupId)
        {
            return await _context.GroupMessages.Where(x => x.GroupId == groupId).ToListAsync();
        }

        public async Task<IEnumerable<GroupMessage>> SearchGroupMessageAsync(int groupId, string message)
        {
            return await _context.GroupMessages.Where(x => x.GroupId == groupId && x.Content == message).ToListAsync();
        }

        public async Task<bool> UpdateMessage(GroupMessage message)
        {
            var groupMessage = await _context.GroupMessages.FirstOrDefaultAsync(x => x.Id == message.Id);
            if (groupMessage is not null)
            {
                groupMessage.Content = message.Content;
                groupMessage.UpdatedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
