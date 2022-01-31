using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDb
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity item);
        TEntity FindById(int id);
        IEnumerable<TEntity> Get();
        IQueryable<TEntity> GetQuery();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> Take(int count);
        IEnumerable<TEntity> Take(int count, Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        TEntity Update(TEntity item);
    }
}
