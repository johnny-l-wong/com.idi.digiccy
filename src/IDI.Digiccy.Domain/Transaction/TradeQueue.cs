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
    public sealed class TradeQueue : Singleton<TradeQueue>
    {
        private List<TradeOrder> items = new List<TradeOrder>();
        private ConcurrentQueue<TradeOrder> queue = new ConcurrentQueue<TradeOrder>();

        private TradeQueue() { }

        public void Clear()
        {
            queue = new ConcurrentQueue<TradeOrder>();
            items.Clear();
        }

        public void Enqueue(TradeOrder item)
        {
            queue.Enqueue(item);
        }

        public void Add(TradeOrder item)
        {
            if (!items.Any(e => e.TranNo == item.TranNo))
                items.Add(item);
        }

        public bool Remove(TradeOrder item)
        {
            if (items.Any(e => e.TranNo == item.TranNo))
                return items.Remove(item);

            return false;
        }

        public Depths GetDepths()
        {
            var depth = new Depths();
            var asks = items.Where(e => e.Type == TranType.Ask).OrderBy(e => e.Price).ToList();
            var bids = items.Where(e => e.Type == TranType.Bid).OrderBy(e => e.Price).ToList();

            foreach (var item in asks)
                depth.Asks.Add(new List<decimal> { item.Price, item.Size, asks.IndexOf(item) + 1 });

            foreach (var item in bids)
                depth.Bids.Add(new List<decimal> { item.Price, item.Size, bids.IndexOf(item) + 1, });

            return depth;
        }

        public bool TryDequeue(out TradeOrder item)
        {
            return queue.TryDequeue(out item);
        }

        public List<TradeOrder> GetMatchOrders(TradeOrder order)
        {
            switch (order.Type)
            {
                case TranType.Bid:
                    return items.Where(e => e.Price <= order.Price && e.Type == TranType.Ask).ToList();
                case TranType.Ask:
                    return items.Where(e => e.Price >= order.Price && e.Type == TranType.Bid).ToList();
                default:
                    return new List<TradeOrder>();
            }
        }
    }
}
