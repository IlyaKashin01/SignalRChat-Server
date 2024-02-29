using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Core.DTO;
using SignalRChat.Core.DTO.Group;
using SignalRChat.Core.DTO.Members;
using SignalRChat.Core.DTO.Messages;
using SignalRChat.Core.Service.Interfaces;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace webapi.Hubs
{
    [Authorize]
    public class GroupHub : Hub
    {
        private readonly IGroupService _groupService;
        private readonly IGroupMessageService _groupMessageService;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private static ConcurrentDictionary<int, string> pullConections = new ConcurrentDictionary<int, string>();

        public GroupHub(IGroupMessageService groupMessageService, IGroupService groupService, IPersonService personService, IMapper mapper)
        {
            _groupMessageService = groupMessageService;
            _groupService = groupService;
            _personService = personService;
            _mapper = mapper;
        }

        #region connection hub
        public override Task OnConnectedAsync()
        {
            int userId = int.Parse(Context!.User!.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            string connectionId = Context.ConnectionId;

            pullConections.AddOrUpdate(userId, connectionId, (key, value) => connectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            int userId = int.Parse(Context!.User!.Claims.First(c => c.Type == ClaimTypes.Sid).Value);

            pullConections.TryRemove(userId, out _);

            return base.OnDisconnectedAsync(exception);
        }

        public static string? GetConnectionId(int userId)
        {
            pullConections.TryGetValue(userId, out string? connectionId);
            return connectionId;
        }
        #endregion

        #region connection group
        public async Task OnConnectedGroupsAsync(int personId)
        {
            var groups = await _groupService.GetAllGroupsAsync (personId);
            var connectionId = GetConnectionId(personId);
            if (connectionId != null)
                foreach (var group in groups)
                {
                    await Groups.AddToGroupAsync(connectionId, group.Name);
                }
            else
                await Clients.Caller.SendAsync("Error", "Невозможно подключиться к группам т.к. нет соединения с хабом");
        }
        public async Task OnDisConnectedGroupsAsync(int personId)
        {
            var groups = await _groupService.GetAllGroupsAsync(personId);
            var connectionId = GetConnectionId(personId);
            if (groups != null && connectionId != null)
                foreach (var group in groups)
                {
                    await Groups.RemoveFromGroupAsync(connectionId, group.Name);
                }
            else
                await Clients.Caller.SendAsync("Error", "Ошибка отключения пользователя от групп");
        }
        #endregion

        #region group management
        public async Task CreateGroup(GroupRequest request)
        {
            var group = await _groupService.CreateGroupAsync(request);
            if (group.Result != null)
                await Clients.Caller.SendAsync("NewGroup", group.Result);
            else
                await Clients.Caller.SendAsync("Error", $"{group.ErrorCode} {group.Message}");
        }
        public async Task LeaveGroupAsync(LeaveGroupRequest request)
        {
            var result = await _groupService.LeaveGroupAsync(request);
            if (result.Success && result.Result != null)
            {
                var connectionId = GetConnectionId(request.personId);
                var group = await _groupService.GetGroupByIdAsync(request.groupId);
                if (group != null && connectionId != null)
                    await Groups.RemoveFromGroupAsync(connectionId, group.Name);
                await Clients.Group(result.Result.GroupName).SendAsync("NewGroupMessage", result.Result.Message);
            }
            else
                await Clients.Caller.SendAsync("Error", $"{result.Message} {result.ErrorCode}");
        }

        public async Task ReturnToGroupAsync(ReturnGroupRequest request)
        {
            var result = await _groupService.ReturnToGroupAsync(request);
            if (result.Success && result.Result != null)
            {
                var connectionId = GetConnectionId(request.personId);
                if (connectionId != null)
                {
                    await Groups.AddToGroupAsync(connectionId, result.Result.GroupName);
                    await Clients.Group(result.Result.GroupName).SendAsync("NewGroupMessage", result.Result.Message);
                }
            }
            else
                await Clients.Caller.SendAsync("Error", $"{result.Message} {result.ErrorCode}");
        }
        #endregion

        #region messages
        public async Task SaveGroupMessage(GroupMessageRequest message)
        {
            var savedMessage = await _groupMessageService.SaveGroupMessageAsync(message);
            var group = await _groupService.GetGroupByIdAsync(message.GroupId);

            if (savedMessage != null && group != null)
            {
                await Clients.Group(group.Name).SendAsync("NewGroupMessage", savedMessage);
                var senderConnection = GetConnectionId(message.SenderId);
                if(senderConnection != null)
                    await Clients.GroupExcept(group.Name, senderConnection).SendAsync("Notification", group.Name, savedMessage.Content);
            }

            else
                await Clients.Caller.SendAsync("Error", "ошибка сохранения группового сообщения в БД");
        }
        public async Task GetAllGroupMessages(int groupId, int? personId)
        {
            var messages = await _groupMessageService.GetAllGroupMessagesAsync(groupId, personId);
            if (messages != null)
            {
                var groupedMessages = messages.GroupBy(x => x.SentAt.Date).Select(group => new
                {
                    SentAt = group.Key,
                    Messages = group.ToList()
                });
                await Clients.Caller.SendAsync("AllGroupMessages", groupedMessages);
            }
            else
                await Clients.Caller.SendAsync("Error", "сообщений нет");
        }
        public async Task ChangeStatusIncomingMessagesAsync(int groupId, string groupName)
        {
            var messages = await _groupMessageService.ChangeStatusIncomingMessagesAsync(groupId);
            if (messages != null)
            {
                await Clients.Group(groupName)
                                  .SendAsync("MessagesWithNewStatus", messages.GroupBy(x => x.SentAt.Date).Select(group => new
                                  {
                                      SentAt = group.Key,
                                      Messages = group.ToList()
                                  }));
            }
            else
                await Clients.Caller.SendAsync("Error", "ошибка при изменении статуса полученных сообщений");
        }
        #endregion

        #region members
        public async Task GetAllUsersToAddGroup(int groupId, int personId)
        {
            var users = await _personService.GetAllUsersToAddGroupAsync(groupId, personId);
            if (users != null)
                await Clients.Caller.SendAsync("AllUsersToAddGroup", users);
            else
                await Clients.Caller.SendAsync("Error", "нет пользователей, доступных для добавления в группу");
        }

        public async Task AddPersonToGroup(MemberRequest request)
        {
            var group = await _groupService.GetGroupDialogByIdAsync(request.GroupId);
            if (group.Result != null)
            {
                var memberMessage = await _groupService.AddPersonToGroupAsync(request);
                if (memberMessage.Result != null)
                {
                    var connectionId = GetConnectionId(request.PersonId);
                    if (connectionId != null)
                    {
                        await Groups.AddToGroupAsync(connectionId, group.Result.Name);
                        await Clients.Client(connectionId).SendAsync("NewGroup", group.Result);
                        await Clients.Client(connectionId).SendAsync("Notification", group.Result.Name, "Вас добавили в группу");
                    }
                    await Clients.Caller.SendAsync("PersonAdded", memberMessage.Result);
                    await Clients.Group(group.Result.Name).SendAsync("NewGroupMessage", memberMessage.Result);
                }
                else
                    await Clients.Caller.SendAsync("Error", $"{memberMessage.ErrorCode} {memberMessage.Message}");
            }
            else
                await Clients.Caller.SendAsync("Error", $"{group.ErrorCode} {group.Message}");
        }
        public async Task GetAllMembersInGroup(int groupId)
        {
            var members = await _groupService.GetAllMembersInGroupAsync(groupId);
            if (members.Result != null)
                {
                    await Clients.Caller.SendAsync("AllMembers", members.Result);
                }
            else
                await Clients.Caller.SendAsync("Error", $"{members.ErrorCode} {members.Message}");
        }
        #endregion
    } 
}
