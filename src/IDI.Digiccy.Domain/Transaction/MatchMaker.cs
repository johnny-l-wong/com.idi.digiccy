using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Base;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction
{
    /// <summary>
    /// 交易撮合器
    /// </summary>
    public sealed class MatchMaker
    {
        public TranResult Do()
        {
            TranOrder order;

            if (TranQueue.Instance.TryDeQueue(out order))
                return MakeMacth(order);

            return TranResult.None();
        }

        private TranResult MakeMacth(TranOrder order)
        {
            switch (order.Type)
            {
                case TranType.Bid:
                    var asks = TranQueue.Instance.GetMatchOrders(order).Select(e => e as AskOrder).ToList();
                    return MakeMatch(order as BidOrder, asks);
                case TranType.Ask:
                    var bids = TranQueue.Instance.GetMatchOrders(order).Select(e => e as BidOrder).ToList();
                    return MakeMatch(order as AskOrder, bids);
                default:
                    return TranResult.Fail();
            }
        }

        private TranResult MakeMatch(BidOrder bid, List<AskOrder> asks)
        {
            throw new NotImplementedException();
        }

        private TranResult MakeMatch(AskOrder ask, List<BidOrder> bids)
        {
            throw new NotImplementedException();
        }
    }
}
