using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;

namespace Core.Tests
{
    public class FakeContextProvider : IContextProvider
    {
        public List<Object> fakeContext = new List<Object>();


        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            this.fakeContext.Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public TEntity Get<TEntity>(Guid id) where TEntity : class
        {
            return (TEntity) this.fakeContext.FirstOrDefault(e => e.GetType().GetProperty("Id").GetValue(e).Equals(id));
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
