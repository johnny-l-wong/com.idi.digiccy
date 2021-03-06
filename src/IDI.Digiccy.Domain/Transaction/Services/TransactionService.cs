﻿using System;
using IDI.Core.Common;
using IDI.Core.Extensions;
using IDI.Core.Logging;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Base;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ILogger _logger;
        private readonly TradeDrive _drive;
        private readonly Guid _id;

        public bool Running => _drive.Running;

        public TransactionService(ILogger logger)
        {
            _id = Guid.NewGuid();
            _logger = logger;
            _drive = new TradeDrive();
            _drive.DriveStarted += OnStarted;
            _drive.DriveStopped += OnStopped;
            _drive.BidEnqueue += OnBidEnqueue;
            _drive.AskEnqueue += OnAskEnqueue;
            _drive.TradeCompleted += OnTradeCompleted;
            _drive.Start();
        }

        #region Events
        private void OnTradeCompleted(TradeResult result)
        {
            _logger.Info($"trade:{result.ToJson()}");
        }

        private void OnAskEnqueue(TradeOrder order)
        {
            _logger.Info($"ask:{order.ToJson()}");
        }

        private void OnBidEnqueue(TradeOrder order)
        {
            _logger.Info($"bid:{order.ToJson()}");
        }

        private void OnStopped()
        {
            _logger.Info("transaction service stopped");
        }

        private void OnStarted()
        {
            _logger.Info("transaction service started");
        }
        #endregion

        public Result Ask(int uid, decimal price, decimal size)
        {
            _drive.Ask(uid, price, size);

            return Result.Success("ask success.");
        }

        public Result Bid(int uid, decimal price, decimal size)
        {
            _drive.Bid(uid, price, size);

            return Result.Success("bid success.");
        }

        public Result<KLine> GetKLine(KLineRange range)
        {
            return Result.Success(_drive.GetKLine(range));
        }

        public void Start()
        {
            _drive.Start();
        }

        public void Stop()
        {
            _drive.Stop();
        }
    }
}
