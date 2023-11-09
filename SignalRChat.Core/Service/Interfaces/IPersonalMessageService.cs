using SignalRChat.Core.Dto;
using SignalRChat.Core.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Interfaces
{
    public interface IPersonalMessageService
    {
        Task<int> SendPersonalMessageAsync(PersonalMessageDto request);
        Task<IEnumerable<PersonalMessageDto>> GetAllMessageInDialogAsync(int myId, int personId);
        Task<IEnumerable<PersonResponse>> GetAllDialogsAsync(int personId);
        Task<bool> UpdatePersonalMessageAsync(PersonalMessageDto request);
    }
}
