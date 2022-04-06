using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskDb.Models;

namespace TaskDb
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        Microsoft.EntityFrameworkCore.DbContext _context;
        DbSet<TEntity> _dbSet;

        public EFGenericRepository(Microsoft.EntityFrameworkCore.DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }
        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }
        public IEnumerable<TEntity> Take(int count)
        {
            return _dbSet.AsNoTracking().Take(count).ToList();
        }
        public IEnumerable<TEntity> Take(int count, Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).Take(count).ToList();
        }
        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }
        public TEntity FindById(int id)
        {
            var entity = _dbSet.Find(id);
            _context.Entry(entity).State = EntityState.Detached;
            _context.SaveChanges();
            return entity;
        }
        public TEntity FindByIdForReload(int id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
            {
                _context.Entry(item).Reload();
            }

            return item;
        }
       public TEntity Create(TEntity item)
        {
            var itemNew = _dbSet.Add(item).Entity;
            _context.SaveChanges();
            _context.Entry(item).State = EntityState.Detached;
            _context.SaveChanges();
            return itemNew;
        }

        public TEntity Reload(int id)
        {
            var item = _dbSet.Find(id);
            _context.Entry(item).State = EntityState.Detached;
            var result = _context.Entry(item).GetDatabaseValues();
            if (result == null)
            {
                return null;
            }
            else
            {
                return (TEntity)result.ToObject();
            }
        }
        public void TrackingSwitching(TEntity OldItem)
        {
            _context.Entry(OldItem).State = EntityState.Unchanged;
        }
        public TEntity Update(TEntity item, byte[] rowversion, string operation = "")
        {
            if (item is IRowVersion)
                {
                    _context.Entry(item).OriginalValues["RowVersion"] = rowversion;
                }
                _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(item).State = EntityState.Detached;
            _context.SaveChanges();

            return item;
        }

        public void Remove(TEntity item)
        {
            _dbSet.Attach(item);
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        public TEntity Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(item).State = EntityState.Detached;
            _context.SaveChanges();
            return item;
        }
    }
}
