using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.DataAccess.Configuration;

namespace Weather.DataAccess.Repositories.Abstrdact
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context; 
            _dbSet = _context.Set<TEntity>();   
        }
        public async Task BulkSaveAsync(IEnumerable<TEntity> entity)
        {
            await _dbSet.AddRangeAsync(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}