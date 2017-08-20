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
            string pathToContentRoot = string.Empty;

            BootstrapLogger.Log("Application starting");      

            pathToContentRoot = Directory.GetCurrentDirectory();
            BootstrapLogger.Log($"Content root : {pathToContentRoot}");
            
            if ( !Debugger.IsAttached && args.Contains("--console"))
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
                Host.BuildAndRunHost(pathToContentRoot, args);
            }
        }
    }
}
