using SignalRChat.Data.Repositories.Interfaces;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Impl
{
    public class BaseMessageRepository<TEntity>: IBaseMessageRepository<TEntity> where TEntity : BaseEntityMessage
    {
        protected readonly AppDbContext _context;

        public BaseMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null) return false;

            entity.DeleteDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public Task<bool> IsExistAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
