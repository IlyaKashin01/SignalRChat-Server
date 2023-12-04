using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Interfaces
{
    public interface IPersonalMessageRepository: IBaseRepository<PersonalMessage>
    {
        Task<IEnumerable<PersonalMessage>> GetAllMessagesInDialogAsync(int myId, int personId);
        Task<IEnumerable<Person>> GetAllPersonalDialogsAsync(int personId);
        Task<PersonalMessage> GetLastPersonalMessageAsync(int senderId, int recipientId);
        Task<IEnumerable<PersonalMessage>> SearchPersonalMessageAsync(string message, int senderId, int recipientId);
        Task<PersonalMessage> UpdateMessageAsync(PersonalMessage message);
        Task<IEnumerable<PersonalMessage>> ChangeStatusIncomingMessagesAsync(int senderId, int recipientId);
    }
}
