using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Core.Dto;
using SignalRChat.Core.DTO;
using SignalRChat.Core.Service.Interfaces;
using SignalRChat.Core.Services.Interfaces;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Security.Claims;

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
    public ChatHub(IPersonalMessageService personalMessageService, IGroupMessageService groupMessageService, IGroupService groupService)
        {
            _personalMessageService = personalMessageService;
            _groupMessageService = groupMessageService;
            _groupService = groupService;
        }
        public async Task SendPersonalMessage(PersonalMessageDto request)
        {
            request.SentAt = DateTime.UtcNow;
            var messageId = await _personalMessageService.SavePersonalMessageAsync(request);
            if (messageId != 0)
                    await this.Clients.Clients(pullConections.FirstOrDefault(x => x.Key == request.SenderId).Value, pullConections.FirstOrDefault(x => x.Key == request.RecipientId).Value).SendAsync("NewMessage", request);
            else await this.Clients.Caller.SendAsync("Error", "error save message to DB");
        }

        public async Task GetAllPersonalMessages(GetPersonalMessagesRequest request)
        {
                var messages = await _personalMessageService.GetAllMessageInDialogAsync(request.SenderId, request.RecipientId);
            if (messages != null)
            {
                await this.Clients.Caller.SendAsync("AllPersonalMessageInDialog", messages);
            }
            else
                await this.Clients.Caller.SendAsync("AllPersonalMessageInDialog", "messages is not exist");
        }
        public async Task GetAllPersonalDialogs(int personId)
        {
            var dialogs = await _personalMessageService.GetAllDialogsAsync(personId);
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
                await this.Clients.Caller.SendAsync("newGroup", "error added group");
        }

        public async Task GetAllGroups(int personId)
        {
            var groups = await _groupService.GetAllGroupsAsync(personId);
            if (groups != null)
                await this.Clients.Caller.SendAsync("AllGroups", groups);
            else
                await this.Clients.Caller.SendAsync("AllGroups", "groups is not exist");
        }

        public async Task AddPersonToGroup(MemberRequest request)
        {
            var memberId = await _groupService.AddPersonToGroup(request);
            if (memberId != 0)
                await this.Clients.Caller.SendAsync("PersonAdded", memberId);
            else
                await this.Clients.Caller.SendAsync("PersonAdded", "error added person to group");
        }

        public async Task SaveGroupMessage(GroupMessageDto message)
        {
            message.SentAt = DateTime.UtcNow;
            var messageId = await _groupMessageService.SaveGroupMessageAsync(message);
            if (messageId != 0) 
            await this.Clients.Group("test").SendAsync("NewGroupMessage", message.Content); 
            else
                await this.Clients.Caller.SendAsync("NewGroupMessage", "error save message to DB");
        }
    }
}
