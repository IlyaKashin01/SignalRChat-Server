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

        public Task<IEnumerable<GroupChatRoom>> GetAllGroupAsync(int personId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupMessage>> GetAllMessageInGroupAsync(int myId, int petsonId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupMessage>> SearchGroupMessageAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GroupMessage> UpdateMessage(GroupMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
