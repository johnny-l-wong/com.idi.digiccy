using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using IDI.Digiccy.Common;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Base;

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
            queue.Clear();
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
