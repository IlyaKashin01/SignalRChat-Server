using SignalRChat.Core.Dto;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Interfaces
{
    public interface IPersonalMessageService
    {
        Task<int> SavePersonalMessageAsync(PersonalMessageDto request);
        Task<IEnumerable<PersonalMessageDto>> GetAllMessageInDialogAsync(int myId, int personId);
        Task<IEnumerable<Dialog>> GetAllDialogsAsync(int personId);
        Task<PersonalMessageDto> GetLastPersonalMessageByIdAsync(int senderId, int recipientId);
        Task<bool> UpdatePersonalMessageAsync(PersonalMessageDto request);
        Task<IEnumerable<PersonalMessageDto>> ChangeStatusIncomingMessagesAsync(int senderId, int recipientId);
    }
}
