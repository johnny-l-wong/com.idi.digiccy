using System.Collections.Generic;
using System.Linq;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Domain.Base;
using IDI.Digiccy.Models;
using IDI.Digiccy.Models.Base;

namespace IDI.Digiccy.Domain
{
    public class MatchMaker : IMatchMaker
    {
        private readonly int interval = 50;

        public bool Running { get; private set; }

        public List<TranOrder> Queue { get; private set; } = new List<TranOrder>();

        public void EnQueue(TranOrder order)
        {
            this.Queue.Add(order);
        }

        public bool TryDeQueue(out TranOrder order)
        {
            order = null;

            if (this.Queue.Count == 0)
                return false;

            order = this.Queue.OrderBy(e => e.Date).First();

            return true;
        }

        public void Start()
        {
            while (this.Running)
            {
                TranOrder order;

                if (TryDeQueue(out order))
                {
                    MakeMacth(order);
                }

                System.Threading.Thread.Sleep(interval);
            }
        }

        public void Stop()
        {
            this.Running = false;
        }

        private void MakeMacth(TranOrder order)
        {
            switch (order.Type)
            {
                case TranType.Bid:
                    var askOrders = this.Queue.Where(e => e.Price <= order.Price).Select(e => e as AskOrder).ToList();
                    MakeMatch(order as BidOrder, askOrders);
                    break;
                case TranType.Ask:
                    var bidOrders = this.Queue.Where(e => e.Price >= order.Price).Select(e => e as BidOrder).ToList();
                    MakeMatch(order as AskOrder, bidOrders);
                    break;
                default:
                    break;
            }
        }

        private void MakeMatch(BidOrder order, List<AskOrder> list)
        {

        }

        private void MakeMatch(AskOrder order, List<BidOrder> list)
        {

        }
    }
}
