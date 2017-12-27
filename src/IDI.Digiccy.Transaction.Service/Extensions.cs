#if NET461
using System.ServiceProcess;
using Microsoft.AspNetCore.Hosting;
#endif

namespace IDI.Digiccy.Transaction.Service
{
    public static class Extensions
    {
#if NET461
        public static void RunAsTransactionService(this IWebHost host)
        {
            ServiceBase.Run(new TransactionHostService(host));
        }
#endif
    }
}
