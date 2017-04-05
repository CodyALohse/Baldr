using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core;

namespace Data.EntityFramework
{
    public class EntityContextProvider : IContextProvider
    {
        private DbContext Context {get; set;}

        private IConfiguration Configuration;
        public EntityContextProvider(IConfiguration configuration){
            this.Configuration = configuration;
        }

        private DbContext ApplicationContext
        {
            get
            {
                if (this.Context == null)
                {
                    // TODO need to pass in the connection string here I think
                    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                    optionsBuilder.UseInMemoryDatabase(this.Configuration.GetConnectionString("DefaultConnection"));
                
                    this.Context = new ApplicationDbContext(optionsBuilder.Options);
                }

                return this.Context;
            }
        }

        public void Save()
        {
            this.ApplicationContext.SaveChanges();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            this.ApplicationContext.Set<TEntity>().Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            this.ApplicationContext.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return this.ApplicationContext.Set<TEntity>().Where(predicate);
        }

        public TEntity Get<TEntity>(Guid id) where TEntity : class
        {
            return this.ApplicationContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return this.ApplicationContext.Set<TEntity>().ToList();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            this.ApplicationContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            this.ApplicationContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}