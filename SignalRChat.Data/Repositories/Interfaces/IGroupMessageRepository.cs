using SignalRChat.Domain.Entities;

namespace SignalRChat.Data.Repositories.Interfaces
{
    public interface IGroupMessageRepository: IBaseMessageRepository<GroupMessage>
    {
        Task<IEnumerable<GroupMessage>> GetAllMessageInGroupAsync(int groupId, int? personId);
        Task<IEnumerable<GroupMessage>> SearchGroupMessageAsync(int groupId, string message);
        Task<bool> UpdateMessage(GroupMessage message);
        Task<IEnumerable<GroupMessage>> ChangeStatusIncomingMessagesAsync(int groupId, int senderId);
    }
}
