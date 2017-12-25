using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Base;

namespace IDI.Digiccy.Models
{
    /// <summary>
    /// 交易定单
    /// </summary>
    public class BidOrder : TranOrder
    {
        public BidOrder(int uid, decimal price, decimal size) : base(uid, price, size, TranType.Bid) { }
    }
}
