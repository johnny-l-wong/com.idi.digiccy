using Microsoft.AspNetCore.Hosting;
#if NET461
using Microsoft.AspNetCore.Hosting.WindowsServices;
#endif

namespace IDI.Digiccy.Transaction.WindowsService
{
#if NET461
    internal class TransactionHostService : WebHostService
    {
        public TransactionHostService(IWebHost host) : base(host)
        {
        }

        protected override void OnStarting(string[] args)
        {
            base.OnStarting(args);
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }
    }
#endif
}
