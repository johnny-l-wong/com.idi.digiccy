using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
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
            //if (args.Contains("--windows-service"))
            if (Debugger.IsAttached || args.Contains("--debug"))
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
