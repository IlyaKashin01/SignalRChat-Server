using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Core.Dto;
using SignalRChat.Core.DTO;
using SignalRChat.Core.Service.Interfaces;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace webapi.Hubs
{
    [Authorize]
    public class GroupHub : Hub
    {
        private readonly IPersonalMessageService _personalMessageService;
        private readonly IGroupService _groupService;
        private readonly IGroupMessageService _groupMessageService;
        private readonly IPersonService _personService;
        private readonly IHubContext<ChatHub> _chatHubContext;
        private static ConcurrentDictionary<int, string> pullConections = new ConcurrentDictionary<int, string>();

        public GroupHub(IPersonalMessageService personalMessageService, IGroupMessageService groupMessageService, IGroupService groupService, IPersonService personService, IHubContext<ChatHub> chatHubContext)
        {
            _personalMessageService = personalMessageService;
            _groupMessageService = groupMessageService;
            _groupService = groupService;
            _personService = personService;
            _chatHubContext = chatHubContext;
        }

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

        public async Task GetAllUsersToAddGroup(int groupId, int personId)
        {
            var users = await _personService.GetAllUsersToAddGroupAsync(groupId, personId);
            if (users != null)
                await Clients.Caller.SendAsync("AllUsersToAddGroup", users);
            else
                await Clients.Caller.SendAsync("Error", "нет пользователей, доступных для добавления в группу");
        }

        public async Task CreateGroup(GroupRequest request)
        {
            var groupId = await _groupService.CreateGroupAsync(request);
            if (groupId != 0)
                await Clients.Caller.SendAsync("NewGroup", groupId);
            else
                await Clients.Caller.SendAsync("Error", "ошибка создания группы");
        }

        public async Task AddPersonToGroup(MemberRequest request)
        {
            var group = await _groupService.GetGroupByIdAsync(request.GroupId);
            if (group != null)
            {
                var memberId = await _groupService.AddPersonToGroupAsync(request);
                if (memberId != 0)
                {
                    var conectionId = pullConections.FirstOrDefault(x => x.Key == request.PersonId).Value;
                    await Groups.AddToGroupAsync(conectionId, group.Name);
                    await _chatHubContext.Clients.Client(conectionId).SendAsync("GetAllDialogs", request.PersonId);
                    await Clients.Caller.SendAsync("PersonAdded", memberId);
                }
            }
            else
                await Clients.Caller.SendAsync("Error", "попытка добавить пользователя в несуществующую группу");
        }

        public async Task SaveGroupMessage(GroupMessageDto message)
        {
            message.SentAt = DateTime.UtcNow;
            var messageId = await _groupMessageService.SaveGroupMessageAsync(message);
            var group = await _groupService.GetGroupByIdAsync(message.GroupId);
            if (messageId != 0 && group != null)
                await Clients.Group(group.Name).SendAsync("NewGroupMessage", message);
            else
                await Clients.Caller.SendAsync("Error", "ошибка сохранения группового сообщения в БД");
        }
        public async Task GetAllGroupMessages(int groupId)
        {
            var messages = await _groupMessageService.GetAllGroupMessagesAsync(groupId);
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
        public async Task GetAllMembersInGroup(int groupId)
        {
            var members = await _groupService.GetAllMembersInGroupAsync(groupId);
            var creatorLogin = await _groupService.GetCreatorLoginAsync(groupId);
            if (members != null && creatorLogin != "")
            {
                var response = new MemberResponse { CreatorLogin = creatorLogin, GroupMembers = members.ToList() };
                await Clients.Caller.SendAsync("AllMembers", response);
            }
            else
                await Clients.Caller.SendAsync("Error", "нет участников в группе");
        }
        public async Task OnConnectedGroupsAsync(IEnumerable<Dialog> groups, int personId)
        {
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
    }
}
