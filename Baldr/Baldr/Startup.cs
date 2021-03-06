﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Data.EntityFramework.Startup;
using Serilog;
using AutoMapper;
using Data.EntityFramework;
using Core.Data.EntityFramework;

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
            var dbContextFactory = new EntityApplicationContextFactory();
            dbContextFactory.CreateDbContext();
            services.AddSingleton<IDbContextFactory<BaldrDbContext>, EntityApplicationContextFactory>();
            services.AddDataProvider<BaldrDbContext>();

            // Add CORS support
            // ** NOTE : configure CORS to be much more restrictive **
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Add framework services.
            services.AddMvc(
                    options => options.RespectBrowserAcceptHeader = true
                );

            // Swagger
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Baldr.Api", Version = "v1" });
            });

            // Automapper
            services.AddAutoMapper();

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

            app.UseCors("CorsPolicy");
            app.UseMvc();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Baldr.Api");
            });
        }
    }
}
