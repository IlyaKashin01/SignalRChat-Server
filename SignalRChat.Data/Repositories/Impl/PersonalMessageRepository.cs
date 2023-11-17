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

        public Task<IEnumerable<PersonalMessage>> SearchPersonalMessageAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PersonalMessage> UpdateMessage(PersonalMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
