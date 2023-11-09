using SignalRChat.Core.Dto.Auth;
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
    }
}
