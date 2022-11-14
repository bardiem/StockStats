using StockStats.Data;
using StockStats.Domain;
using System;
using System.Threading.Tasks;

namespace StockStats.DAL
{
    public class BaseRepo<T>
    {
        private readonly StockStatsDbContext _context;

        public BaseRepo(StockStatsDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Create(T entity)
        {
            if (entity is IHaveDateCreated)
            {
                (entity as IHaveDateCreated).DateCreated = DateTime.UtcNow;
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
