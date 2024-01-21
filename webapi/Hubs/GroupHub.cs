using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
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
        private static ConcurrentDictionary<int, string> pullConections = new ConcurrentDictionary<int, string>();

        public GroupHub(IGroupMessageService groupMessageService, IGroupService groupService, IPersonService personService)
        {
            _groupMessageService = groupMessageService;
            _groupService = groupService;
            _personService = personService;
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
            var groupId = await _groupService.CreateGroupAsync(request);
            if (groupId != 0)
                await Clients.Caller.SendAsync("NewGroup", groupId);
            else
                await Clients.Caller.SendAsync("Error", "ошибка создания группы");
        }
        public async Task LeaveGroupAsync(LeaveGroupRequest request)
        {
            var result = await _groupService.LeaveGroupAsync(request.groupId, request.personId);
            if (result)
            {
                var connectionId = GetConnectionId(request.personId);
                var group = await _groupService.GetGroupByIdAsync(request.groupId);
                if (connectionId != null)
                    await Groups.RemoveFromGroupAsync(connectionId, group.Name);
                if (group != null)
                {
                    if (request.creatorLogin == "")
                    {
                        await _groupMessageService.SaveGroupMessageAsync(new GroupMessageRequest
                        {
                            Content = $"{request.personLogin} покинул группу",
                            GroupId = request.groupId,
                            SenderId = 0
                        });
                        await Clients.Group(group.Name).SendAsync("NewGroupMessage", $"{request.personLogin} покинул группу");
                    }
                    else
                    {
                        var person = await _personService.GetPersonByIdAsync(request.personId);
                        if (person != null)
                        {
                            await _groupMessageService.SaveGroupMessageAsync(new GroupMessageRequest
                            {
                                Content = $"{request.creatorLogin} исключил {person.Login}",
                                GroupId = request.groupId,
                                SenderId = 0
                            });
                            await Clients.Group(group.Name).SendAsync("NewGroupMessage", $"{request.creatorLogin} исключил {person.Login}");
                        }
                        else
                            await Clients.Caller.SendAsync("Error", "попытка исключить не существующего пользователя");
                    }
                    await Clients.Caller.SendAsync("memberStatus", result);
                }
            }
            else
                await Clients.Caller.SendAsync("Error", "ошибка при покидании группы");
        }

        public async Task ReturnToGroupAsync(int groupId, string groupName, int personId, string personLogin)
        {
            var result = await _groupService.ReturnToGroupAsync(groupId, personId);
            if (result)
            {
                var connectionId = GetConnectionId(personId);
                if (connectionId != null)
                {
                    await Groups.AddToGroupAsync(connectionId, groupName);
                    await _groupMessageService.SaveGroupMessageAsync(new GroupMessageRequest { Content = $"{personLogin} покинул группу", GroupId = groupId, SenderId = 0 });
                    await Clients.Group(groupName).SendAsync("NewGroupMessage", $"{personLogin} вернулся в группу");
                    await Clients.Caller.SendAsync("memberStatus", result);
                }
            }
            else
                await Clients.Caller.SendAsync("Error", "ошибка при покидании группы");
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
            var group = await _groupService.GetGroupByIdAsync(request.GroupId);
            if (group != null)
            {
                var memberId = await _groupService.AddPersonToGroupAsync(request);
                if (memberId != 0)
                {
                    var connectionId = GetConnectionId(request.PersonId);
                    if (connectionId != null)
                    {
                        await Groups.AddToGroupAsync(connectionId, group.Name);
                        await Clients.Client(connectionId).SendAsync("Notification", group.Name, "Вас добавили в группу");
                    }
                    await this.Clients.Caller.SendAsync("PersonAdded", memberId);
                    var addedPerson = await _personService.GetPersonByIdAsync(request.AddedByPerson);
                    var person = await _personService.GetPersonByIdAsync(request.PersonId);
                    if (addedPerson != null && person != null)
                    {
                        var message = await _groupMessageService.SaveGroupMessageAsync(new GroupMessageRequest { Content = $"{addedPerson.Login} добавил пользователя {person.Login} в группу", GroupId = group.Id, SenderId = 0 });
                        if(message != null)
                            await Clients.Group(group.Name).SendAsync("NewGroupMessage", message.Content);
                    }
                }
            }
            else
                await this.Clients.Caller.SendAsync("Error", "Не удалось добавить пользователя в группу");
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
        #endregion
    } 
}
