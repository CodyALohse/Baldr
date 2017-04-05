using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseModel
    {
        public IContextProvider ContextProvider { get; set; }

        public void Add(TEntity entity)
        {
            this.ContextProvider.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.ContextProvider.AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.ContextProvider.Find(predicate);
        }

        public TEntity Get(Guid id)
        {
            return this.ContextProvider.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.ContextProvider.GetAll<TEntity>();
        }

        public void Remove(TEntity entity)
        {
            this.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.RemoveRange(entities);
        }
    }
}