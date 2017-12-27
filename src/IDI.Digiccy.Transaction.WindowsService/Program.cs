using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
#if NET461
using System.Linq;
using Microsoft.AspNetCore.Hosting.WindowsServices;
#endif

namespace IDI.Digiccy.Transaction.WindowsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

#if NET461
            if (args.Contains("--windows-service"))
            {
                host.RunAsService();
            }
            else
            {
                host.Run();
            }
#else
            host.Run();
#endif
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
