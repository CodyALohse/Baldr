using DasMulli.Win32.ServiceUtils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Baldr
{
    public static class Host
    {
        public static IWebHost BuildHost(string pathToContentRoot)
        {
            return new WebHostBuilder()
              .UseKestrel()
              .UseContentRoot(pathToContentRoot)
              .UseIISIntegration()
              .UseStartup<Startup>()
              .UseApplicationInsights()
              .Build();
        }

        public static void BuildAndRun(string pathToContentRoot)
        {
            var host = BuildHost(pathToContentRoot);
            host.Run();
        }

        /// <summary>
        /// Need to return the IWebHost so we can dispose of it when the service is stopped.
        /// </summary>
        /// <param name="pathToContentRoot"></param>
        /// <param name="serviceStop"></param>
        /// <param name="serviceStopCallback"></param>
        /// <returns></returns>
        public static IWebHost ServiceBuildHost(string pathToContentRoot, bool serviceStop, ServiceStoppedCallback serviceStopCallback)
        {
            var host = BuildHost(pathToContentRoot);

            // Make sure the windows service is stopped if the ASP.NET Core stack stops for any reason
            host
                .Services
                .GetRequiredService<IApplicationLifetime>()
                .ApplicationStopped
                .Register(() => 
                {
                    if (!serviceStop)
                    {
                        serviceStopCallback();
                    }
                });

            return host;
        }
    }
}
