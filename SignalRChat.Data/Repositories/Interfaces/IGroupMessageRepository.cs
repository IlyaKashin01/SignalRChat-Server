using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Interfaces
{
    public interface IGroupMessageRepository: IBaseRepository<GroupMessage>
    {
        Task<IEnumerable<GroupMessage>> GetAllMessageInGroupAsync(int myId, int petsonId);
        Task<IEnumerable<GroupChatRoom>> GetAllGroupAsync(int personId);
        Task<IEnumerable<GroupMessage>> SearchGroupMessageAsync();
        Task<GroupMessage> UpdateMessage(GroupMessage message);
    }
}
