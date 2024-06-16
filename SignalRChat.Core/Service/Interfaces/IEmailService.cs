using SignalRChat.Common.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Interfaces
{
    public interface IEmailService
    {
        Task<OperationResult<bool>> SendEmailMessageAsync(string email, string message);
    }
}
