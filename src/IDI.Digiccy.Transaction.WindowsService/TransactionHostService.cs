using System.Threading.Tasks;
using IDI.Digiccy.Domain.Transaction;
using Microsoft.AspNetCore.Hosting;
#if NET461
using Microsoft.AspNetCore.Hosting.WindowsServices;
#endif

namespace IDI.Digiccy.Transaction.WindowsService
{
#if NET461
    internal class TransactionHostService : WebHostService
#else
    internal class TransactionHostService : HostService
#endif
    {
        private readonly ITransactionService service;

        public TransactionHostService(IWebHost host) : base(host)
        {
            service = new TransactionService();
        }

        protected override void OnStarted()
        {
            service.Start();
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            service.Stop();
            base.OnStopped();
        }
    }
}
