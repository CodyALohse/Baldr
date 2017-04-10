using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core;
using Data.EntityFramework.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Data.EntityFramework
{
    public class EntityContextProvider : IContextProvider
    {
        private DbContext Context {get; set;}

        private IConfiguration Configuration;

        private readonly IHostingEnvironment HostingEnvironment;

        public EntityContextProvider(IConfiguration configuration, IHostingEnvironment hostingEnvironment){
            this.Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;
        }

        private DbContext ApplicationContext
        {
            get
            {
                if (this.Context == null)
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                    optionsBuilder.GetDbProvider(this.HostingEnvironment.EnvironmentName, this.Configuration["ConnectionStrings:DefaultConnection"]);

                    this.Context = new ApplicationDbContext(optionsBuilder.Options);

                    // TODO this should be moved to the testing class instead of here
                    // TODO not sure right now if it runs the latest migration?
                    if (this.HostingEnvironment.EnvironmentName.Equals("IntegrationTesting"))
                    {
                        // Delete the database per test. This wil probably cause horrible integration testing latency if many tests are to be run
                        this.Context.Database.EnsureDeleted();
                        this.Context.Database.Migrate();
                    }
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

        public TEntity Get<TEntity>(int id) where TEntity : class
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