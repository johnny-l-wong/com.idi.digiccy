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

        private TransactionQueue()
        {
            items = new List<TransactionOrder>();
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool EnQueue(TransactionOrder item)
        {
            this.items.Add(item);
            return true;
        }

        public bool TryDeQueue(out TransactionOrder item)
        {
            item = null;

            if (this.items.Count == 0)
                return false;

            item = this.items.OrderBy(e => e.Date).FirstOrDefault();

            if (item != null)
                this.items.Remove(item);

            return item != null;
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
