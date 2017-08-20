using DasMulli.Win32.ServiceUtils;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Baldr
{
    public static class Host
    {
        public static IWebHost BuildHost(string pathToContentRoot, string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(pathToContentRoot)
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .Build();
        }

        public static void BuildAndRunHost(string pathToContentRoot, string[] args)
        {
            var host = BuildHost(pathToContentRoot, args);
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
            var host = BuildHost(pathToContentRoot, null);

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
