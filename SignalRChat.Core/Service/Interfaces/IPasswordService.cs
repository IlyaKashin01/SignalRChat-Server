using SignalRChat.Common.OperationResult;
using SignalRChat.Core.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Interfaces
{
    public interface IPasswordService
    {
        Task<OperationResult<bool>> GetResetCodeAsync(string login);
        Task<OperationResult<bool>> CheckResetCodeAsync(CodeRequest request);
        Task<OperationResult<bool>> ChangePasswordAsync(ChangePassRequest request);
    }
}
