using Microsoft.AspNetCore.Hosting;
using System.IO;
#if NET461
using System.Linq;
using Microsoft.AspNetCore.Hosting.WindowsServices;
#endif

namespace IDI.Digiccy.Transaction.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

#if NET461
            if (args.Contains("--windows-service"))
            //if (Debugger.IsAttached || args.Contains("--debug"))
            {
                host.RunAsWindowsService();
            }
            else
            {
                host.Run();
            }
#else
            host.Run();
#endif
        }

        public static IWebHost BuildWebHost(string[] args) => new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:17528")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}
