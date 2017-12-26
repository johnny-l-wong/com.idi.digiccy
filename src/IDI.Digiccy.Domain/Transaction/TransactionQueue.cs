using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using IDI.Digiccy.Common;
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
        private List<TransactionOrder> items;
        private ConcurrentQueue<TransactionOrder> queue;

        private TransactionQueue()
        {
            queue = new ConcurrentQueue<TransactionOrder>();
            items = new List<TransactionOrder>();
        }

        public void Clear()
        {
            queue = new ConcurrentQueue<TransactionOrder>();
            items.Clear();
        }

        public void Enqueue(TransactionOrder item)
        {
            queue.Enqueue(item);
        }

        public void Add(TransactionOrder item)
        {
            if (!items.Any(e => e.TranNo == item.TranNo))
                items.Add(item);
        }

        public bool Remove(TransactionOrder item)
        {
            if (items.Any(e => e.TranNo == item.TranNo))
                return items.Remove(item);

            return false;
        }

        public OrderQueue Current()
        {
            var data = new OrderQueue();

            var asks = items.Where(e => e.Type == TransactionType.Ask).OrderBy(e => e.Price).ToList();

            foreach (var item in asks)
                data.Asks.Add(new Order { SN = asks.IndexOf(item)+1, Price = item.Price, Volume = item.Price, Type = item.Type });

            var bids = items.Where(e => e.Type == TransactionType.Bid).OrderBy(e => e.Price).ToList();

            foreach (var item in bids)
                data.Bids.Add(new Order { SN = bids.IndexOf(item)+1, Price = item.Price, Volume = item.Price, Type = item.Type });

            return data;
        }

        public bool TryDequeue(out TransactionOrder item)
        {
            return queue.TryDequeue(out item);
            //item = null;

            //if (items.Count == 0)
            //    return false;

            //item = items.OrderBy(e => e.Date).FirstOrDefault();

            //if (item != null)
            //    this.items.Remove(item);

            //return item != null;
        }

        public List<TransactionOrder> GetMatchOrders(TransactionOrder order)
        {
            switch (order.Type)
            {
                case TransactionType.Bid:
                    return items.Where(e => e.Price <= order.Price && e.Type == TransactionType.Ask).ToList();
                case TransactionType.Ask:
                    return items.Where(e => e.Price >= order.Price && e.Type == TransactionType.Bid).ToList();
                default:
                    return new List<TransactionOrder>();
            }
        }
    }
}
