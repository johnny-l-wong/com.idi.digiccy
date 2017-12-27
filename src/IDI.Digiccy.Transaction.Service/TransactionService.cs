using IDI.Core.Extensions;
using IDI.Core.Logging;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Domain.Transaction;
using IDI.Digiccy.Models.Base;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Transaction.Service
{
    public class TransactionService : ITransactionService
    {
        public TransactionDevice Device => TransactionDevice.Instance;
        public ILogger Logger { get; private set; }

        public TransactionService(ILogger logger)
        {
            Logger = logger;
            Device.DeviceStart += OnDeviceStart;
            Device.DeviceStop += OnDeviceStop;
            Device.BidEnqueue += OnBidEnqueue;
            Device.AskEnqueue += OnAskEnqueue;
            Device.TransactionCompleted += OnTransactionCompleted;
        }

        private void OnBidEnqueue(TranOrder order)
        {
            Logger.Info($"OnBidEnqueue->{order.ToJson()}");
        }

        private void OnAskEnqueue(TranOrder order)
        {
            Logger.Info($"OnAskEnqueue->{order.ToJson()}");
        }

        public void Start()
        {
            Device.Start();
            Logger.Info("Device started.");
        }

        public void Stop()
        {
            Device.Stop();
            Logger.Info("Device stopped.");
        }

        public void Bid(int uid, decimal price, decimal size)
        {
            Device.Bid(uid, price, size);
            Logger.Info($"Bid:{uid},{price},{size}");
        }

        public void Ask(int uid, decimal price, decimal size)
        {
            Device.Ask(uid, price, size);
            Logger.Info($"Ask:{uid},{price},{size}");
        }

        public void Queue()
        {
            var queue = Device.Queue();

            Logger.Info($"Queue:{queue.ToJson()}");
        }

        private void OnTransactionCompleted(TranResult result)
        {
            if (result.Status != TranStatus.Success)
                return;

            Logger.Info($"OnTransactionCompleted->{result.ToJson()}");
        }

        private void OnDeviceStop()
        {
            Logger.Info("Device stopping");
        }

        private void OnDeviceStart()
        {
            Logger.Info("Device starting");
        }
    }
}
