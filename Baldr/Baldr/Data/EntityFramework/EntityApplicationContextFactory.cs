using Core.Data.EntityFramework;
using Data.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Data.EntityFramework
{
    /// <summary>
    /// This class is used for running EF CLI options.
    /// Without it Migrations and Updates fail stating a missing data provider
    /// </summary>
    public class EntityApplicationContextFactory : IDbContextFactory<BaldrDbContext>
    {

        public BaldrDbContext CreateDbContext()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true);

            IConfigurationRoot config = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<BaldrDbContext>();
            optionsBuilder.GetDbProvider(environmentName, config["ConnectionStrings:DefaultConnection"]);

            return new BaldrDbContext(optionsBuilder.Options);
        }
    }
}