using System.IO;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using Baldr.WindowsService;
using Core.Bootstrap;

namespace Baldr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool isService = true;
            string pathToContentRoot = string.Empty;

            BootstrapLogger.Log("Application starting");      

            if (Debugger.IsAttached || args.Contains("--console"))
            {
                isService = false;
            }

            pathToContentRoot = Directory.GetCurrentDirectory();
            BootstrapLogger.Log($"Content root : {pathToContentRoot}");
            
            if (isService)
            {
                BootstrapLogger.Log("Running as a service.");

                var assemblyPath = typeof(Program).GetTypeInfo().Assembly.Location;
                pathToContentRoot = Path.GetDirectoryName(assemblyPath);
                BootstrapLogger.Log($"Assembly path : {assemblyPath}");
                BootstrapLogger.Log($"Service content root : {pathToContentRoot}");

                WindowsServiceRunner.Run(pathToContentRoot);
            }
            else
            {
                BootstrapLogger.Log("Running as a non-service.");
                Host.BuildAndRun(pathToContentRoot);
            }
        }
    }
}
