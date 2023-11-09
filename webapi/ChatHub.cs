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
        private readonly IPersonService _personService;
        public ChatHub(IPersonalMessageService personalMessageService, IPersonService personService)
        {
            _personalMessageService = personalMessageService;
            _personService = personService;
        }
        [Authorize]
        public async Task SendPersonalMessage(int senderId, int recipientId, string message)
        {
                    var request = new PersonalMessageDto { SenderId = senderId, RecipientId = recipientId, Content = message, SentAt = DateTime.UtcNow, IsCheck = false };
                    var messageId = await _personalMessageService.SendPersonalMessageAsync(request);
            if (messageId != 0) 
                        await this.Clients.Caller.SendAsync("PersonalMessage", request.Content);
            else await this.Clients.Caller.SendAsync("PersonalMessage", "error save message to DB");
        }

        public async Task GetAllPersonalMessages(int senderId,int recipientId)
        {
                var messages = await _personalMessageService.GetAllMessageInDialogAsync(senderId, recipientId);
                if (messages != null)
                {
                    await this.Clients.Caller.SendAsync("AllPersonalMessageInDialog", messages);
                }
        }
    }
}
