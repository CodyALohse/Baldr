using System.IO;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using Baldr.WindowsService;

namespace Baldr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool isService = true;
            string pathToContentRoot = string.Empty;
            var fileStream = new FileStream("E:\\Dev\\netCore\\publish\\log.txt", FileMode.OpenOrCreate);
            using (var writer = new StreamWriter(fileStream))
            {
                writer.WriteLine("Application starting");


                if (Debugger.IsAttached || args.Contains("--console"))
                {
                    isService = false;
                }

                pathToContentRoot = Directory.GetCurrentDirectory();
                writer.WriteLine($"Content root : {pathToContentRoot}");

                if (isService)
                {
                    writer.WriteLine("Running as a service.");
                    var assemblyPath = typeof(Program).GetTypeInfo().Assembly.Location;
                    writer.WriteLine($"Assembly path : {assemblyPath}");
                    pathToContentRoot = Path.GetDirectoryName(assemblyPath);
                    writer.WriteLine($"Service content root : {pathToContentRoot}");
                }
            }

            if (isService)
            {
                WindowsServiceRunner.Run(pathToContentRoot);
            }
            else
            {
                Host.BuildAndRun(pathToContentRoot);
            }
            
        }

        
    }
}
