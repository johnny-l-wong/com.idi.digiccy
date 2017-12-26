﻿using System.Collections.Generic;
using IDI.Digiccy.Models.Base;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction
{
    public class TransactionDevice
    {
        private bool isRunning = false;
        private Matchmaker maker;

        public TransactionDevice()
        {
            isRunning = false;
            maker = new Matchmaker();
        }

        #region TransactionCompleted
        public delegate void TransactionHandler(TransactionResult result);

        public event TransactionHandler TransactionCompleted;

        protected virtual void OnTransactionCompleted(TransactionResult result)
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

        #region BidCompleted
        public delegate void BidHandler(TransactionOrder order);

        public event BidHandler BidCompleted;

        protected virtual void OnBidCompleted(TransactionOrder order)
        {
            BidCompleted?.Invoke(order);
        }
        #endregion

        #region AskCompleted
        public delegate void AskHandler(TransactionOrder order);

        public event AskHandler AskCompleted;

        protected virtual void OnAskCompleted(TransactionOrder order)
        {
            AskCompleted?.Invoke(order);
        }
        #endregion

        public OrderQueue Queue()
        {
            return TransactionQueue.Instance.Current();
        }

        public void Start()
        {
            isRunning = true;

            OnDeviceStart();

            Run();
        }

        public void Stop()
        {
            isRunning = false;

            OnDeviceStop();
        }

        public void Bid(int uid, decimal price, decimal size)
        {
            var order = new BidOrder(uid, price, size);

            TransactionQueue.Instance.Enqueue(order);

            OnBidCompleted(order);
        }

        public void Ask(int uid, decimal price, decimal size)
        {
            var order = new AskOrder(uid, price, size);

            TransactionQueue.Instance.Enqueue(order);

            OnAskCompleted(order);
        }

        private void Run()
        {
            while (isRunning)
            {
                var result = maker.Do();

                OnTransactionCompleted(result);
            }
        }
    }
}
