using System.Threading.Tasks;
using IDI.Core.Utils;
using IDI.Digiccy.Models.Base;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction
{
    public sealed class TransactionDevice : Singleton<TransactionDevice>
    {
        private bool running = false;
        private Matchmaker maker;

        private TransactionDevice()
        {
            running = false;
            maker = new Matchmaker();
        }

        #region TransactionCompleted
        public delegate void TransactionHandler(TranResult result);

        public event TransactionHandler TransactionCompleted;

        private void OnTransactionCompleted(TranResult result)
        {
            TransactionCompleted?.Invoke(result);
        }
        #endregion

        #region DeviceStart
        public delegate void DeviceStartHandler();

        public event DeviceStartHandler DeviceStart;

        private void OnDeviceStart()
        {
            DeviceStart?.Invoke();
        }
        #endregion

        #region DeviceStop
        public delegate void DeviceStopHandler();

        public event DeviceStopHandler DeviceStop;

        private void OnDeviceStop()
        {
            DeviceStop?.Invoke();
        }
        #endregion

        #region BidEnqueue
        public delegate void BidEnqueueHandler(TranOrder order);

        public event BidEnqueueHandler BidEnqueue;

        private void OnBidEnqueue(TranOrder order)
        {
            BidEnqueue?.Invoke(order);
        }
        #endregion

        #region AskEnqueue
        public delegate void AskEnqueueHandler(TranOrder order);

        public event AskEnqueueHandler AskEnqueue;

        private void OnAskEnqueue(TranOrder order)
        {
            AskEnqueue?.Invoke(order);
        }
        #endregion

        public TranQueue Queue()
        {
            return TransactionQueue.Instance.Current();
        }

        public void Start()
        {
            running = true;

            OnDeviceStart();

            Task.Factory.StartNew(Run);
        }

        public void Stop()
        {
            running = false;

            OnDeviceStop();
        }

        public void Bid(int uid, decimal price, decimal size)
        {
            var order = new BidOrder(uid, price, size);

            TransactionQueue.Instance.Enqueue(order);

            OnBidEnqueue(order);
        }

        public void Ask(int uid, decimal price, decimal size)
        {
            var order = new AskOrder(uid, price, size);

            TransactionQueue.Instance.Enqueue(order);

            OnAskEnqueue(order);
        }

        private void Run()
        {
            while (running)
            {
                var result = maker.Do();

                OnTransactionCompleted(result);
            }
        }
    }
}
