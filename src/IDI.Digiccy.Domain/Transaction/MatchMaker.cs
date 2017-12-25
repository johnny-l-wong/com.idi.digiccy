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
            asks = asks.OrderBy(e => e.Price).ThenBy(e => e.Date).ToList();

            var items = new List<TranResult.Item>();

            foreach (var ask in asks)
            {
                if (bid.Remain() == 0)
                    break;

                var item = MakeMatch(bid, ask);

                items.Add(item);
            }

            return items.Count > 0 ? TranResult.Success(items) : TranResult.None();
        }

        private TranResult MakeMatch(AskOrder ask, List<BidOrder> bids)
        {
            bids = bids.OrderByDescending(e => e.Price).ThenBy(e => e.Date).ToList();

            var items = new List<TranResult.Item>();

            foreach (var bid in bids)
            {
                if (ask.Remain() == 0)
                    break;

                var item = MakeMatch(bid, ask);

                items.Add(item);
            }

            return items.Count > 0 ? TranResult.Success(items) : TranResult.None();
        }

        private TranResult.Item MakeMatch(BidOrder bid, AskOrder ask)
        {
            decimal price = bid.Date < ask.Date ? bid.Price : ask.Price;
            decimal volume = bid.Remain() <= ask.Remain() ? bid.Remain() : ask.Remain();
            var taker = bid.Date < ask.Date ? Counterparty.Seller : Counterparty.Buyer;

            bid.Volume += volume;
            ask.Volume += volume;

            return new TranResult.Item
            {
                Ask = ask,
                Bid = bid,
                Price = price,
                Volume = volume,
                Taker = taker
            };
        }
    }
}
