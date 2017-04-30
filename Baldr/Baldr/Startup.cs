using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Data.EntityFramework.Startup;
using Serilog;

namespace Baldr
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDataProvider();

            // Add framework services.
            services.AddMvc();

            // Add IConfiguration as a singleton so we can inject IConfiguration
            // into other classes to load settings from a json file.
            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var logFilePath = this.Configuration["Logging:LogFilePath"];
            if (string.IsNullOrEmpty(logFilePath))
            {
                logFilePath = "./";
            }

            var serilog = new LoggerConfiguration()
                .ReadFrom.Configuration(this.Configuration)
                .CreateLogger();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile($"{logFilePath}/baldr-{{Date}}.log");
            loggerFactory.AddSerilog(serilog);
            app.UseMvc();
        }
    }
}
