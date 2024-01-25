using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.DTO;
using SignalRChat.Core.DTO.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Interfaces
{
    public interface IPersonalMessageService
    {
        Task<OperationResult<PersonalMessageResponse>> SavePersonalMessageAsync(PersonalMessageRequest request);
        Task<IEnumerable<Dialog>> SavePersonalMessageWithCreateDialogAsync(PersonalMessageRequest request);
        Task<IEnumerable<PersonalMessageResponse>> GetAllMessageInDialogAsync(int myId, int personId);
        Task<IEnumerable<Dialog>> GetAllDialogsAsync(int personId);
        Task<bool> UpdatePersonalMessageAsync(PersonalMessageResponse request);
        Task<IEnumerable<PersonalMessageResponse>> ChangeStatusIncomingMessagesAsync(int senderId, int recipientId);
    }
}
