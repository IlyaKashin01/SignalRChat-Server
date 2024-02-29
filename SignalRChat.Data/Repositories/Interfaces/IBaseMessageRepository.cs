using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Interfaces
{
    public interface IBaseMessageRepository<TEntity> where TEntity : BaseEntityMessage
    {
        Task<int> CreateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<TEntity?> GetByIdAsync(int id);
        Task<bool> IsExistAsync(int id);
    }
}
