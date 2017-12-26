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
        public TransactionResult Do()
        {
            TransactionOrder order;

            if (TransactionQueue.Instance.TryDequeue(out order))
            {
                var result = MakeMacth(order);

                //未撮合完成则重新加入待撮合队列
                if (order.Remain() > 0)
                    TransactionQueue.Instance.Add(order);

                return result;
            }


            return TransactionResult.None();
        }

        private TransactionResult MakeMacth(TransactionOrder order)
        {
            switch (order.Type)
            {
                case TransactionType.Bid:
                    var asks = TransactionQueue.Instance.GetMatchOrders(order).Select(e => e as AskOrder).ToList();
                    return MakeMatch(order as BidOrder, asks);
                case TransactionType.Ask:
                    var bids = TransactionQueue.Instance.GetMatchOrders(order).Select(e => e as BidOrder).ToList();
                    return MakeMatch(order as AskOrder, bids);
                default:
                    return TransactionResult.Fail();
            }
        }

        private TransactionResult MakeMatch(BidOrder bid, List<AskOrder> asks)
        {
            asks = asks.OrderBy(e => e.Price).ThenBy(e => e.Date).ToList();

            var items = new List<TransactionResult.Item>();

            foreach (var ask in asks)
            {
                if (bid.Remain() == 0)
                    break;

                var item = MakeMatch(bid, ask);

                items.Add(item);
            }

            return items.Count > 0 ? TransactionResult.Success(items) : TransactionResult.None();
        }

        private TransactionResult MakeMatch(AskOrder ask, List<BidOrder> bids)
        {
            bids = bids.OrderByDescending(e => e.Price).ThenBy(e => e.Date).ToList();

            var items = new List<TransactionResult.Item>();

            foreach (var bid in bids)
            {
                if (ask.Remain() == 0)
                    break;

                var item = MakeMatch(bid, ask);

                items.Add(item);
            }

            return items.Count > 0 ? TransactionResult.Success(items) : TransactionResult.None();
        }

        private TransactionResult.Item MakeMatch(BidOrder bid, AskOrder ask)
        {
            decimal price = bid.Date < ask.Date ? bid.Price : ask.Price;
            decimal volume = bid.Remain() <= ask.Remain() ? bid.Remain() : ask.Remain();
            var taker = bid.Date < ask.Date ? Counterparty.Seller : Counterparty.Buyer;

            bid.Volume += volume;
            ask.Volume += volume;

            return new TransactionResult.Item
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
