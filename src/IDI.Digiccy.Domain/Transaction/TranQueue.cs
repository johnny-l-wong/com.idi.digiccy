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
    public sealed class TranQueue : Singleton<TranQueue>
    {
        private List<TranOrder> _items;

        private TranQueue()
        {
            _items = new List<TranOrder>();
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool EnQueue(TranOrder item)
        {
            this._items.Add(item);
            return true;
        }

        public bool TryDeQueue(out TranOrder item)
        {
            item = null;

            if (this._items.Count == 0)
                return false;

            item = this._items.OrderBy(e => e.Date).FirstOrDefault();

            if (item != null)
                this._items.Remove(item);

            return item != null;
        }

        public List<TranOrder> GetMatchOrders(TranOrder order)
        {
            switch (order.Type)
            {
                case TranType.Bid:
                    return _items.Where(e => e.Price <= order.Price && e.Type == TranType.Ask).ToList();
                case TranType.Ask:
                    return _items.Where(e => e.Price >= order.Price && e.Type == TranType.Bid).ToList();
                default:
                    return new List<TranOrder>();
            }
        }
    }
}
