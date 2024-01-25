using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Interfaces
{
    public interface IPersonRepository: IBaseRepository<Person>
    {
        Task<Person?> FindByLoginAsync(string login);
        Task<string?> GetLoginByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllUsersAsync(int personId);
        Task<IEnumerable<Person>> GetAllUsersToAddGroupAsync(int groupId, int personId);
    }
}
