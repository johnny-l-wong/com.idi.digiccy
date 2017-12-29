using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Base;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction
{
    /// <summary>
    /// 撮合器
    /// </summary>
    public sealed class Matchmaker
    {
        public TradeResult Do()
        {
            TradeOrder order;

            if (TradeQueue.Instance.TryDequeue(out order))
            {
                var result = MakeMacth(order);

                //如果交易未完成,则重新加入待交易队列
                if (order.Remain() > 0)
                    TradeQueue.Instance.Add(order);

                return result;
            }


            return TradeResult.None();
        }

        private TradeResult MakeMacth(TradeOrder order)
        {
            switch (order.Type)
            {
                case TranType.Bid:
                    var asks = TradeQueue.Instance.GetMatchOrders(order).Select(e => e as AskOrder).ToList();
                    return MakeMatch(order as BidOrder, asks);
                case TranType.Ask:
                    var bids = TradeQueue.Instance.GetMatchOrders(order).Select(e => e as BidOrder).ToList();
                    return MakeMatch(order as AskOrder, bids);
                default:
                    return TradeResult.Fail();
            }
        }

        private TradeResult MakeMatch(BidOrder bid, List<AskOrder> asks)
        {
            asks = asks.OrderBy(e => e.Price).ThenBy(e => e.Date).ToList();

            var items = new List<TradeLog>();

            foreach (var ask in asks)
            {
                if (bid.Remain() == 0)
                    break;

                var item = MakeMatch(bid, ask);

                items.Add(item);
            }

            return items.Count > 0 ? TradeResult.Success(items) : TradeResult.None();
        }

        private TradeResult MakeMatch(AskOrder ask, List<BidOrder> bids)
        {
            bids = bids.OrderByDescending(e => e.Price).ThenBy(e => e.Date).ToList();

            var items = new List<TradeLog>();

            foreach (var bid in bids)
            {
                if (ask.Remain() == 0)
                    break;

                var item = MakeMatch(bid, ask);

                items.Add(item);
            }

            return items.Count > 0 ? TradeResult.Success(items) : TradeResult.None();
        }

        private TradeLog MakeMatch(BidOrder bid, AskOrder ask)
        {
            decimal price = bid.Date < ask.Date ? bid.Price : ask.Price;
            decimal volume = bid.Remain() <= ask.Remain() ? bid.Remain() : ask.Remain();
            var taker = bid.Date < ask.Date ? TradeParty.Seller : TradeParty.Buyer;

            bid.Volume += volume;
            ask.Volume += volume;

            if (ask.Remain() == 0)
                TradeQueue.Instance.Remove(ask);

            if (bid.Remain() == 0)
                TradeQueue.Instance.Remove(bid);

            var log = new TradeLog
            {
                Ask = ask,
                Bid = bid,
                Price = price,
                Volume = volume,
                Taker = taker,
                Time = DateTime.Now
            };

            TradeLogger.Instance.Update(log);

            return log;
        }
    }
}
