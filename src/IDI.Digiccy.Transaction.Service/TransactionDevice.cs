using IDI.Digiccy.Domain.Transaction;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Transaction.Service
{
    public class TransactionDevice
    {
        private bool _isRunning = false;
        private Matchmaker _maker;

        public TransactionDevice()
        {
            _isRunning = false;
            _maker = new Matchmaker();
        }

        #region TransactionCompleted
        public delegate void TransactionHandler(TranResult result);

        public event TransactionHandler TransactionCompleted;

        protected virtual void OnTransactionCompleted(TranResult result)
        {
            TransactionCompleted?.Invoke(result);
        }
        #endregion

        #region DeviceStart
        public delegate void DeviceStartHandler();

        public event DeviceStartHandler DeviceStart;

        protected virtual void OnDeviceStart()
        {
            DeviceStart?.Invoke();
        }
        #endregion

        #region DeviceStop
        public delegate void DeviceStopHandler();

        public event DeviceStopHandler DeviceStop;

        protected virtual void OnDeviceStop()
        {
            DeviceStop?.Invoke();
        }
        #endregion

        public void Start()
        {
            _isRunning = true;

            OnDeviceStart();

            Run();
        }

        public void Stop()
        {
            _isRunning = false;

            OnDeviceStop();
        }

        public bool Bid(int uid, decimal price, decimal size)
        {
            return TranQueue.Instance.EnQueue(new BidOrder(uid, price, size));
        }

        public bool Ask(int uid, decimal price, decimal size)
        {
            return TranQueue.Instance.EnQueue(new AskOrder(uid, price, size));
        }

        private void Run()
        {
            while (_isRunning)
            {
                OnTransactionCompleted(_maker.Do());
            }
        }
    }
}
