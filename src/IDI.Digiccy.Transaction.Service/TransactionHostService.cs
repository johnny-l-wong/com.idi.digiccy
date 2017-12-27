using IDI.Core.Infrastructure;
using IDI.Core.Logging;
using IDI.Digiccy.Domain.Transaction;
using Microsoft.AspNetCore.Hosting;
#if NET461
using Microsoft.AspNetCore.Hosting.WindowsServices;
#endif

namespace IDI.Digiccy.Transaction.Service
{
#if NET461
    internal class TransactionHostService : WebHostService
#else
    internal class TransactionHostService : HostService
#endif
    {
        //private readonly ITransactionService service;
        private readonly ILogger logger;

        public TransactionHostService(IWebHost host) : base(host)
        {
            //this.service = Runtime.GetService<ITransactionService>();
            this.logger = Runtime.GetService<ILogger>();
            logger.Info("Transaction service initial.");
        }

        protected override void OnStarted()
        {
            //service.Start();
            base.OnStarted();
            logger.Info("Transaction service started.");
        }

        protected override void OnStopped()
        {
            //service.Stop();
            base.OnStopped();
            logger.Info("Transaction service stopped.");
        }
    }
}
