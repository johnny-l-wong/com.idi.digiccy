using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Base;

namespace IDI.Digiccy.Models.Transaction
{
    /// <summary>
    /// 交易定单
    /// </summary>
    public class BidOrder : TradeOrder
    {
        public BidOrder(int uid, decimal price, decimal size) : base(uid, price, size, TranType.Bid) { }
    }
}
