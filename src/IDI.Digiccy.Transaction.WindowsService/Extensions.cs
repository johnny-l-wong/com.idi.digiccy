#if NET461
using System.ServiceProcess;
using Microsoft.AspNetCore.Hosting;
#endif

namespace IDI.Digiccy.Transaction.WindowsService
{
    public static class Extensions
    {
#if NET461
        public static void RunAsTransactionService(this IWebHost host)
        {
            var service = new TransactionHostService(host);
            ServiceBase.Run(service);
        }
#endif
    }
}
