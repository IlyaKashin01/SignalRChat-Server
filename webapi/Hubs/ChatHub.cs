using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Core.DTO;
using SignalRChat.Core.DTO.Messages;
using SignalRChat.Core.Service.Interfaces;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace webapi.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IPersonalMessageService _personalMessageService;
        private readonly IGroupService _groupService;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private static ConcurrentDictionary<int, string> pullConections = new ConcurrentDictionary<int, string>();

        public ChatHub(IPersonalMessageService personalMessageService, IGroupService groupService, IPersonService personService, IMapper mapper)
        {
            _personalMessageService = personalMessageService;
            _groupService = groupService;
            _personService = personService;
            _mapper = mapper;
        }
        #region connection
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
        #region users
        public async Task GetOnlineMarkers()
        {
            await Clients.All.SendAsync("OnlineMarkers", pullConections.Keys.ToList());
        }
        public async Task GetAllUsers(int personId)
        {
            var users = await _personService.GetAllUsersAsync(personId);
            if (users != null)
                await Clients.Caller.SendAsync("AllUsers", users);
            else
                await Clients.Caller.SendAsync("Error", "нет новых пользователей, с которыми можно начать диалог");
        }
        #endregion
        #region messages
        public async Task SendPersonalMessage(PersonalMessageRequest message)
        {
            var recipientConnection = GetConnectionId(message.RecipientId);
            if(recipientConnection != null)
            if (message.IsNewDialog)
            {
                var newDialog = await _personalMessageService.SavePersonalMessageWithCreateDialogAsync(message);
                    if (recipientConnection != null)
                    {
                        var responseForRecipient = newDialog.FirstOrDefault(x => x.Name != message.PersonLogin);
                        if (responseForRecipient != null)
                        {
                            await Clients.Client(recipientConnection).SendAsync("NewDialog", responseForRecipient);
                            await Clients.Client(recipientConnection).SendAsync("Notification", responseForRecipient.Name, responseForRecipient.LastMessage);
                        }
                    }
                    await Clients.Caller.SendAsync("NewDialog", newDialog.FirstOrDefault(x => x.Name == message.PersonLogin));
            }
            else 
            {
                var createdMessage = await _personalMessageService.SavePersonalMessageAsync(message);
                if (createdMessage.Result != null)
                {
                    await Clients.Client(recipientConnection).SendAsync("NewMessage", createdMessage.Result);
                    await Clients.Client(recipientConnection).SendAsync("Notification", createdMessage.Result.SenderLogin, createdMessage.Result.Content);
                    await Clients.Caller.SendAsync("NewMessage", createdMessage.Result);
                }
                else
                    await Clients.Caller.SendAsync("Error", $"ошибка сохранения сообщения в БД: {createdMessage.ErrorCode} {createdMessage.Message}");
            }
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
                await Clients.Caller.SendAsync("AllPersonalMessageInDialog", groupedMessages);
            }
            else
                await Clients.Caller.SendAsync("Error", "сообщений нет");
        }
        public async Task ChangeStatusIncomingMessagesAsync(int senderId, int recipientId)
        {
            var messages = await _personalMessageService.ChangeStatusIncomingMessagesAsync(senderId, recipientId);
            if(messages != null)
            {
                await Clients.Clients(
                    pullConections.FirstOrDefault(x => x.Key == recipientId).Value,
                    pullConections.FirstOrDefault(x => x.Key == senderId).Value)
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
        #region dialogs
        public async Task GetAllDialogs(int personId)
        {
            var personalDialogs = await _personalMessageService.GetAllDialogsAsync(personId);

            var groups = await _groupService.GetAllGroupsAsync(personId);

            var dialogs = personalDialogs.Union(groups).OrderBy(x => x.Name);

            if (dialogs != null)
                await Clients.Caller.SendAsync("AllDialogs", dialogs);
            else
                await Clients.Caller.SendAsync("Error", "нет диалогов");
        }
        #endregion
    }
}
