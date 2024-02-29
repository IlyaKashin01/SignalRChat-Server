using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Interfaces
{
    public interface IPersonService
    {
        Task<PersonResponse> FindPersonByLoginAcync(string login);
        Task<IEnumerable<PersonResponse>> GetAllUsersAsync(int personId);
        Task <PersonResponse> GetPersonByIdAsync(int personId);
        Task<IEnumerable<PersonResponse>> GetAllUsersToAddGroupAsync(int groupId, int personId);
        Task<OperationResult<string>> GetLoginByIdAsync(int personId);
    }
}
