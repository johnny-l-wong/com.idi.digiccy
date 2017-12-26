using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Base;

namespace IDI.Digiccy.Models.Transaction
{
    /// <summary>
    /// 交易应单
    /// </summary>
    public class AskOrder : TransactionOrder
    {
        public AskOrder(int uid, decimal price, decimal size) : base(uid, price, size, TransactionType.Ask) { }
    }
}
