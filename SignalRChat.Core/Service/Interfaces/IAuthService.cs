using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Interfaces
{
    public interface IAuthService
    {
        Task<OperationResult<AuthResponse>> AuthenticateAsync(AuthRequest request);
        Task<OperationResult<int>> SingupAsync(SignupRequest request);
    }
}
