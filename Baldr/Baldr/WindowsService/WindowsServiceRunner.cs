using DasMulli.Win32.ServiceUtils;

namespace Baldr.WindowsService
{
    public static class WindowsServiceRunner
    {
        public static void Run(string contentRootPath)
        {
            var baldrService = new BaldrService(contentRootPath);
            var serviceHost = new Win32ServiceHost(baldrService);
            serviceHost.Run();
        }
    }
}
