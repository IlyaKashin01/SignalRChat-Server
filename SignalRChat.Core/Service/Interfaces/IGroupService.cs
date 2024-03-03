using SignalRChat.Common.OperationResult;
using SignalRChat.Core.DTO;
using SignalRChat.Core.DTO.Group;
using SignalRChat.Core.DTO.Members;
using SignalRChat.Core.DTO.Messages;

namespace SignalRChat.Core.Service.Interfaces
{
    public interface IGroupService
    {
        Task<OperationResult<Dialog>> CreateGroupAsync(GroupRequest request);
        Task<IEnumerable<Dialog>> GetAllGroupsAsync(int personId);
        Task<OperationResult<Dialog>> GetGroupDialogByIdAsync(int groupId, int personId);
        Task<OperationResult<GroupMessageResponse>> AddPersonToGroupAsync(MemberRequest request);
        Task<GroupResponse> GetGroupByIdAsync(int groupId);
        Task<OperationResult<MemberResponse>> GetAllMembersInGroupAsync(int groupId);
        Task<OperationResult<LeaveAndReturnGroupResponse>> LeaveGroupAsync(LeaveGroupRequest request);
        Task<OperationResult<LeaveAndReturnGroupResponse>> ReturnToGroupAsync(ReturnGroupRequest request);
    }
}
