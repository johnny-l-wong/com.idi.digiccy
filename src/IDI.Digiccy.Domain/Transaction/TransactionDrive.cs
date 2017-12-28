using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Base;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction
{
    internal class TransactionDrive
    {
        private bool running = false;
        private Matchmaker maker;
        private List<Trade> trades;

        public bool Running => running;

        public TransactionDrive()
        {
            running = false;
            maker = new Matchmaker();
            trades = new List<Trade>();
        }

        #region TransactionCompleted
        public delegate void TransactionHandler(TradeResult result);

        public event TransactionHandler TransactionCompleted;

        protected virtual void OnTransactionCompleted(TradeResult result)
        {
            if (result.Status == TranStatus.Success)
                trades.AddRange(result.Logs.Select(e => new Trade { SN = e.SN, Date = e.Time, Price = e.Price, Volume = e.Volume, Taker = e.Taker }));

            TransactionCompleted?.Invoke(result);
        }
        #endregion

        #region DeviceStart
        public delegate void DriveStartedHandler();

        public event DriveStartedHandler DriveStarted;

        protected virtual void OnDriveStarted()
        {
            DriveStarted?.Invoke();
        }
        #endregion

        #region DeviceStop
        public delegate void DriveStoppedHandler();

        public event DriveStoppedHandler DriveStopped;

        protected virtual void OnDriveStopped()
        {
            DriveStopped?.Invoke();
        }
        #endregion

        #region BidEnqueue
        public delegate void BidEnqueueHandler(TranOrder order);

        public event BidEnqueueHandler BidEnqueue;

        protected virtual void OnBidEnqueue(TranOrder order)
        {
            BidEnqueue?.Invoke(order);
        }
        #endregion

        #region AskEnqueue
        public delegate void AskEnqueueHandler(TranOrder order);

        public event AskEnqueueHandler AskEnqueue;

        protected virtual void OnAskEnqueue(TranOrder order)
        {
            AskEnqueue?.Invoke(order);
        }
        #endregion

        public KLine Get()
        {
            return new KLine { Depths = TransactionQueue.Instance.GetDepths(), Trades = trades };
        }

        public void Start()
        {
            running = true;

            Task.Factory.StartNew(Run);

            OnDriveStarted();
        }

        public void Stop()
        {
            running = false;

            OnDriveStopped();
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
            while (true)
            {
                if (!running)
                    continue;

                var result = maker.Do();

                OnTransactionCompleted(result);
            }
        }
    }
}
