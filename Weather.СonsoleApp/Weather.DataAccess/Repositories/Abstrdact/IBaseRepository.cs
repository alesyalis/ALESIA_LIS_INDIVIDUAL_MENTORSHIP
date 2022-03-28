using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Weather.DataAccess.Repositories.Abstrdact
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
         Task<TEntity> GetByIdAsync(int id);

         Task<TEntity> CreateAsync(TEntity entity);

         Task BulkSaveAsync(List<TEntity> entity);   

    }
}
