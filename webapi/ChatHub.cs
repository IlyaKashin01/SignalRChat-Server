using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Core.Dto;
using SignalRChat.Core.DTO;
using SignalRChat.Core.Service.Interfaces;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace webapi
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Receive", message);
        }

        private readonly IPersonalMessageService _personalMessageService;
        private readonly IGroupService _groupService;
        private readonly IGroupMessageService _groupMessageService;
        private readonly IPersonService _personService;
        private static ConcurrentDictionary<int, string> pullConections = new ConcurrentDictionary<int, string>();
        public override Task OnConnectedAsync()
        {
            int userId = Int32.Parse(Context!.User!.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            string connectionId = Context.ConnectionId;

            pullConections.AddOrUpdate(userId, connectionId, (key, value) => connectionId);
         
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            int userId = Int32.Parse(Context!.User!.Claims.First(c => c.Type == ClaimTypes.Sid).Value);

            pullConections.TryRemove(userId, out _);

            return base.OnDisconnectedAsync(exception);
        }

        public static string? GetConnectionId(int userId)
        {
            pullConections.TryGetValue(userId, out string? connectionId);
            return connectionId;
        }
        public ChatHub(IPersonalMessageService personalMessageService, IGroupMessageService groupMessageService, IGroupService groupService, IPersonService personService)
        {
            _personalMessageService = personalMessageService;
            _groupMessageService = groupMessageService;
            _groupService = groupService;
            _personService = personService;
        }
        public async Task GetAllUsers (int personId)
        {
            var users = await _personService.GetAllUsersAsync(personId);
            if (users != null)
                await this.Clients.Caller.SendAsync("AllUsers", users);
            else
                await this.Clients.Caller.SendAsync("Error", "users is not exist");
        }
        public async Task SendPersonalMessage(PersonalMessageDto message)
        {
            message.SentAt = DateTime.UtcNow;
            var messageId = await _personalMessageService.SavePersonalMessageAsync(message);
            if (messageId != 0)
                    await this.Clients.Clients(
                        pullConections.FirstOrDefault(x => x.Key == message.SenderId).Value, 
                        pullConections.FirstOrDefault(x => x.Key == message.RecipientId).Value)
                                      .SendAsync("NewMessage", message);
            else await this.Clients.Caller.SendAsync("Error", "error save message to DB");
        }

        public async Task GetAllPersonalMessages(GetPersonalMessagesRequest request)
        {
                var messages = await _personalMessageService.GetAllMessageInDialogAsync(request.SenderId, request.RecipientId);
            if (messages != null)
            {
                var groupedMessages = messages.GroupBy(x => x.SentAt.Date).Select(group => new
                {
                    SentAt = group.Key,
                    Messages = group.ToList()
                });
                await this.Clients.Caller.SendAsync("AllPersonalMessageInDialog", groupedMessages);
            }
            else
                await this.Clients.Caller.SendAsync("Error", "messages is not exist");
        }
        public async Task GetAllDialogs(int personId)
        {
            var dialogs = new List<Dialog>();
            var personalDialogs = await _personalMessageService.GetAllDialogsAsync(personId);
            foreach ( var personalDialog in personalDialogs) {
                var dialog = new Dialog();
                dialog.Id = personalDialog.Id;
                dialog.Name = personalDialog.Login;
                dialog.IsGroup = false;
                dialogs.Add(dialog);
            }
            var groups = await _groupService.GetAllGroupsAsync(personId);
            await OnConnectedGroupsAsync(groups, personId);
            foreach ( var group in groups)
            {
                var dialog = new Dialog();
                dialog.Id = group.Id;
                dialog.Name = group.Name;
                dialog.IsGroup = true;
                dialogs.Add(dialog);
            }
            if (dialogs != null)
                await this.Clients.Caller.SendAsync("AllDialogs", dialogs);
            else
                await this.Clients.Caller.SendAsync("Error", "dialogs does not exist");
        }

        public async Task CreateGroup(GroupRequest request)
        {
            var groupId = await _groupService.CreateGroupAsync(request);
            if (groupId != 0)
                await this.Clients.Caller.SendAsync("newGroup", groupId); 
            else
                await this.Clients.Caller.SendAsync("Error", "error added group");
        }

        public async Task AddPersonToGroup(MemberRequest request)
        {
            var memberId = await _groupService.AddPersonToGroupAsync(request);
            if (memberId != 0)
            {
                var group = await _groupService.GetGroupByIdAsync(request.GroupId);
                var connectionId = GetConnectionId(memberId);
                if (group != null && connectionId != null)
                    await Groups.AddToGroupAsync(connectionId, group.Name);
                await this.Clients.Caller.SendAsync("PersonAdded", memberId);
            }
            else
                await this.Clients.Caller.SendAsync("Error", "Не удалось добавить пользователя в группу");
        }

        public async Task SaveGroupMessage(GroupMessageDto message)
        {
            message.SentAt = DateTime.UtcNow;
            var messageId = await _groupMessageService.SaveGroupMessageAsync(message);
            var group = await _groupService.GetGroupByIdAsync(message.GroupId);
            if (messageId != 0 && group != null) 
            await this.Clients.Group(group.Name).SendAsync("NewGroupMessage", message); 
            else
                await this.Clients.Caller.SendAsync("Error", "error save message to DB");
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
                await this.Clients.Caller.SendAsync("AllGroupMessages", groupedMessages);
            }
            else
                await this.Clients.Caller.SendAsync("Error", "messages is not exist");
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
                await Clients.Caller.SendAsync("Error", "Нет участников");
        }

        private async Task OnConnectedGroupsAsync(IEnumerable<GroupResponse> groups, int personId)
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
