﻿using System.Threading.Tasks;
using IDI.Digiccy.Models.Base;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction
{
    internal class TradeDrive
    {
        private bool running = false;
        private Matchmaker maker;

        public bool Running => running;

        public TradeDrive()
        {
            running = false;
            maker = new Matchmaker();
        }

        #region TransactionCompleted
        public delegate void TradeHandler(TradeResult result);

        public event TradeHandler TradeCompleted;

        protected virtual void OnTradeCompleted(TradeResult result)
        {
            TradeCompleted?.Invoke(result);
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
        public delegate void BidEnqueueHandler(TradeOrder order);

        public event BidEnqueueHandler BidEnqueue;

        protected virtual void OnBidEnqueue(TradeOrder order)
        {
            BidEnqueue?.Invoke(order);
        }
        #endregion

        #region AskEnqueue
        public delegate void AskEnqueueHandler(TradeOrder order);

        public event AskEnqueueHandler AskEnqueue;

        protected virtual void OnAskEnqueue(TradeOrder order)
        {
            AskEnqueue?.Invoke(order);
        }
        #endregion

        public KLine GetKLine()
        {
            return new KLine { Depths = TradeQueue.Instance.GetDepths(), Trades = TradeLogger.Instance.GetTrades() };
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

            TradeQueue.Instance.Enqueue(order);

            OnBidEnqueue(order);
        }

        public void Ask(int uid, decimal price, decimal size)
        {
            var order = new AskOrder(uid, price, size);

            TradeQueue.Instance.Enqueue(order);

            OnAskEnqueue(order);
        }

        private void Run()
        {
            while (true)
            {
                if (!running)
                    continue;

                var result = maker.Do();

                OnTradeCompleted(result);
            }
        }
    }
}