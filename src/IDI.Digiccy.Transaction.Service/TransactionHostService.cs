using IDI.Core.Infrastructure;
using IDI.Digiccy.Domain.Transaction.Services;
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
        private readonly ITransactionService service;
        //private readonly ILogger logger;

        public TransactionHostService(IWebHost host) : base(host)
        {
            this.service = Runtime.GetService<ITransactionService>();
            //this.logger = Runtime.GetService<ILogger>();
            //logger.Info("transaction service initialized.");
        }

        protected override void OnStarted()
        {
            //service.Start();
            base.OnStarted();
            //logger.Info("transaction service started.");
        }

        protected override void OnStopped()
        {
            //service.Stop();
            base.OnStopped();
            //logger.Info("transaction service stopped.");
        }
    }
}
