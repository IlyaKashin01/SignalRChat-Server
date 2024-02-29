using SignalRChat.Domain.Entities;

namespace SignalRChat.Data.Repositories.Interfaces
{
    public interface IGroupRepository : IBaseRepository<GroupChatRoom>
    {
        Task<string?> GetCreatorLoginAsync(int groupId);
        Task<IEnumerable<GroupChatRoom?>> GetAllGroupAsync(int personId);
        Task<GroupMessage?> GetLastGroupMessageAsync(int groupId);
        Task<GroupChatRoom?> LeaveGroupAsync(int groupId, int personId, bool isExcluded);
        Task<GroupChatRoom?> ReturnToGroupAsync(int groupId, int personId);
        Task<int> GetCountMembersInGroupAsync(int groupId);
        Task<int> GetCountUnreadMessagesInGroupAsync(int groupId);
    }
}