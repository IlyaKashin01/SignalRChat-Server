using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Data.Repositories.Interfaces;
using SignalRChat.Domain.Entities;

namespace SignalRChat.Data.Repositories.Impl
{
    public class ChatRepository : BaseRepository<PersonalMessage>
    {
        public ChatRepository(AppDbContext context) : base(context) { }

        public Task<IEnumerable<int>> GetAllChats(int personId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PersonalMessage>> GetMessagesBetweenUsersAsync(int user1Id, int user2Id)
        {
            return await _context.PersonalMessages.Where(x => x.SenderId == user1Id || x.RecipientId == user2Id &&
             x.SenderId == user2Id || x.RecipientId == user1Id).ToListAsync();
        }

        public async Task<IEnumerable<Person?>> GetUsersInGroupAsync(int groupId)
        {
            return await _context.Groups.Where(x => x.Id == groupId)
                                        .Select(x => x.Person)
                                        .ToListAsync();
        }

        public Task UpdateMessageAsync(PersonalMessage message)
        {
            throw new NotImplementedException();
        }
    }
}