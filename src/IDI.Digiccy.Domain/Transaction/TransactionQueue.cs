using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using IDI.Core.Utils;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Base;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction
{
    /// <summary>
    /// 交易队列
    /// </summary>
    public sealed class TransactionQueue : Singleton<TransactionQueue>
    {
        private List<TranOrder> items = new List<TranOrder>();
        private ConcurrentQueue<TranOrder> queue = new ConcurrentQueue<TranOrder>();

        private TransactionQueue() { }

        public void Clear()
        {
            queue = new ConcurrentQueue<TranOrder>();
            items.Clear();
        }

        public void Enqueue(TranOrder item)
        {
            queue.Enqueue(item);
        }

        public void Add(TranOrder item)
        {
            if (!items.Any(e => e.TranNo == item.TranNo))
                items.Add(item);
        }

        public bool Remove(TranOrder item)
        {
            if (items.Any(e => e.TranNo == item.TranNo))
                return items.Remove(item);

            return false;
        }

        public Depth Depth()
        {
            var depth = new Depth();
            var asks = items.Where(e => e.Type == TranType.Ask).OrderBy(e => e.Price).ToList();
            var bids = items.Where(e => e.Type == TranType.Bid).OrderBy(e => e.Price).ToList();

            foreach (var item in asks)
                depth.Asks.Add(new List<decimal> { asks.IndexOf(item) + 1, item.Price, item.Price });

            foreach (var item in bids)
                depth.Bids.Add(new List<decimal> { bids.IndexOf(item) + 1, item.Price, item.Price });

            return depth;
        }

        public bool TryDequeue(out TranOrder item)
        {
            return queue.TryDequeue(out item);
        }

        public List<TranOrder> GetMatchOrders(TranOrder order)
        {
            switch (order.Type)
            {
                case TranType.Bid:
                    return items.Where(e => e.Price <= order.Price && e.Type == TranType.Ask).ToList();
                case TranType.Ask:
                    return items.Where(e => e.Price >= order.Price && e.Type == TranType.Bid).ToList();
                default:
                    return new List<TranOrder>();
            }
        }
    }
}
