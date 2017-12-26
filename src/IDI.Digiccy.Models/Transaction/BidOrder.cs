using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Base;

namespace IDI.Digiccy.Models.Transaction
{
    /// <summary>
    /// 交易定单
    /// </summary>
    public class BidOrder : TransactionOrder
    {
        public BidOrder(int uid, decimal price, decimal size) : base(uid, price, size, TransactionType.Bid) { }
    }
}
