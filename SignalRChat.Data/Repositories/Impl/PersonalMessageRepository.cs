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
    public class PersonalMessageRepository : BaseRepository<PersonalMessage>, IPersonalMessageRepository
    {
        public PersonalMessageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PersonalMessage>> GetAllMessagesInDialogAsync(int myId, int personId)
        {
            return await _context.PersonalMessages.Where(x => (x.SenderId == myId && x.RecipientId == personId) ||
             (x.SenderId == personId && x.RecipientId == myId)).OrderBy(x => x.SentAt).ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetAllPersonalDialogsAsync(int personId)
        {
            var dialogs = await _context.PersonalMessages
            .Where(m => m.SenderId == personId || m.RecipientId == personId)
            .Select(m => m.SenderId)
            .Union(_context.PersonalMessages.Where(m => m.SenderId == personId || m.RecipientId == personId)
            .Select(m => m.RecipientId))
            .Distinct()
            .Where(id => id != personId)
            .ToListAsync();
            return await _context.Users.Where(u => dialogs.Contains(u.Id)).ToListAsync();
        }


        public async Task<PersonalMessage> GetLastPersonalMessageAsync(int senderId, int recipientId)
        {
            return await _context.PersonalMessages
                                 .Where(x => (x.SenderId == senderId && x.RecipientId == recipientId) || (x.SenderId == recipientId && x.RecipientId == senderId))
                                 .OrderBy(x => x.SentAt) 
                                 .LastAsync();
        }

        public async Task<IEnumerable<PersonalMessage>> SearchPersonalMessageAsync(string message, int senderId, int recipientId)
        {
            return await _context.PersonalMessages.Where(x => (x.SenderId == senderId && x.RecipientId == recipientId) || (x.SenderId == recipientId && x.RecipientId == senderId) && x.Content == message).ToListAsync();
        }

        public async Task<PersonalMessage> UpdateMessageAsync(PersonalMessage message)
        {
            var updatedMessage = await _context.PersonalMessages.FirstAsync(x => x.Id == message.Id);
            if(updatedMessage != null)
            {
                updatedMessage.Content = message.Content;
                updatedMessage.UpdatedDate = DateTime.UtcNow;
                updatedMessage.IsCheck = message.IsCheck;
                return updatedMessage;
            }
            return message;
        }

        public async Task<IEnumerable<PersonalMessage>> ChangeStatusIncomingMessagesAsync (int  senderId, int recipientId)
        {
            var messages = await _context.PersonalMessages.Where(x => x.SenderId == senderId && x.RecipientId == recipientId && x.IsCheck == false).OrderBy(x => x.SentAt).ToListAsync();

            foreach (var message in messages)
            {
                message.IsCheck = true;
                await _context.SaveChangesAsync();
            }
            return messages;
        }
    }
}
