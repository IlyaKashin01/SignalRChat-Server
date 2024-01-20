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
        Task<IEnumerable<GroupMessage>> GetAllMessageInGroupAsync(int groupId, int? personId);
        Task<IEnumerable<GroupMessage>> SearchGroupMessageAsync(int groupId, string message);
        Task<bool> UpdateMessage(GroupMessage message);
        Task<IEnumerable<GroupMessage>> ChangeStatusIncomingMessagesAsync(int groupId);
    }
}
