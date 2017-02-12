using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SkeletonCQRS.Data.Entities;

namespace SkeletonCQRS.Data.Repositories
{
    public class GenericRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository()
        {
            _context = new SkeletonContext();
            _dbSet = _context.Set<TEntity>();
        }

        public TEntity Get(TId id)
        {
            return _dbSet.Find(id);
        }
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expr)
        {
            return _dbSet.Where(expr);
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            _context.SaveChanges();
        }
    }

    public interface IRepository<TEntity, in TId>
        where TEntity : IEntity<TId>
    {
        TEntity Get(TId id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expr);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
    }
}