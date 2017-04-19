using DasMulli.Win32.ServiceUtils;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Baldr.WindowsService
{
    public class BaldrService : IWin32Service
    {
        private string ContentRootPath;

        private bool StopRequestedByWindows = false;

        private IWebHost WebHost;

        public BaldrService(string contentRootPath)
        {
            this.ContentRootPath = contentRootPath;
        }

        public string ServiceName => "BaldrV1";

        public void Start(string[] startupArguments, ServiceStoppedCallback serviceStoppedCallback)
        {
            Console.WriteLine("Starting Bladr as a Windows Service");

            if (string.IsNullOrWhiteSpace(this.ContentRootPath))
            {
                throw new ArgumentNullException("ContentRootPath", "Path is missing.");
            }

            this.WebHost = Host.ServiceBuildHost(this.ContentRootPath, this.StopRequestedByWindows, serviceStoppedCallback);
            this.WebHost.Start();
        }

        public void Stop()
        {
            this.StopRequestedByWindows = true;
            this.WebHost.Dispose();
        }
    }
}
