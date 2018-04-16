using Core.Data.EntityFramework;
using Data.EntityFramework;
using Data.EntityFramework.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baldr.Api.Tests.Integration
{
    public class TestDbContextFactory : IDbContextFactory<BaldrDbContext>
    {
        private IHostingEnvironment HostingEnvironment;
        private IConfiguration Configuration;

        public TestDbContextFactory(IHostingEnvironment hostingEnvironment, IConfiguration configuration) {
            this.HostingEnvironment = hostingEnvironment;
            this.Configuration = configuration;
        }

        public BaldrDbContext CreateDbContext() {
            var optionsBuilder = new DbContextOptionsBuilder<BaldrDbContext>();
            optionsBuilder.GetDbProvider(this.HostingEnvironment.EnvironmentName, this.Configuration["ConnectionStrings:DefaultConnection"]);

            var context = new BaldrDbContext(optionsBuilder.Options);

            // TODO this should be moved to the testing class instead of here
            // TODO not sure right now if it runs the latest migration?
            if (this.HostingEnvironment.EnvironmentName.Equals("IntegrationTesting"))
            {
                // Delete the database per test. This wil probably cause horrible integration testing latency if many tests are to be run
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }

            return context;
        }
    }
}
