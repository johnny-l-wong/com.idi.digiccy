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
        public void Do()
        {

        }

        private void MakeMacth(TranOrder order)
        {
            switch (order.Type)
            {
                case TranType.Bid:
                    var asks = TranQueue.Instance.GetMatchOrders(order).Select(e => e as AskOrder).ToList();
                    MakeMatch(order as BidOrder, asks);
                    break;
                case TranType.Ask:
                    var bids = TranQueue.Instance.GetMatchOrders(order).Select(e => e as BidOrder).ToList();
                    MakeMatch(order as AskOrder, bids);
                    break;
                default:
                    break;
            }
        }

        private void MakeMatch(BidOrder bid, List<AskOrder> asks)
        {

        }

        private void MakeMatch(AskOrder ask, List<BidOrder> bids)
        {

        }
    }
}
