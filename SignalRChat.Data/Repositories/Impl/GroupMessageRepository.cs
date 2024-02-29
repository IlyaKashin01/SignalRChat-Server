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
    public class GroupMessageRepository: BaseMessageRepository<GroupMessage> ,IGroupMessageRepository
    {
        public GroupMessageRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<GroupMessage>> GetAllMessageInGroupAsync(int groupId, int? personId)
        {
            if (personId != 0)
            {
                var member = await _context.GroupMembers.FirstOrDefaultAsync(x => x.GroupId == groupId && x.PersonId == personId && x.DeleteDate != null);
                if (member != null)
                    return await _context.GroupMessages.Where(x => x.GroupId == groupId && x.SentAt <= member.DeleteDate).ToListAsync();
            }
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

        public async Task<IEnumerable<GroupMessage>> ChangeStatusIncomingMessagesAsync(int groupId)
        {
            var messages = await _context.GroupMessages.Where(x => x.GroupId == groupId && x.IsCheck == false).ToListAsync();

            foreach (var message in messages)
            {
                message.IsCheck = true;
                await _context.SaveChangesAsync();
            }
            return messages;
        }
        
    }
}
