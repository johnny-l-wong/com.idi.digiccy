using System.Collections.Generic;
using IDI.Digiccy.Common.Enums;

namespace IDI.Digiccy.Models.Transaction
{
    /// <summary>
    /// 交易结果 
    /// </summary>
    public class TradeResult
    {
        public List<TradeLog> Logs { get; set; } = new List<TradeLog>();

        public TradeStatus Status { get; set; }

        public static TradeResult Success(List<TradeLog> items)
        {
            return new TradeResult { Logs= items, Status = TradeStatus.Success };
        }

        public static TradeResult Fail()
        {
            return new TradeResult { Status = TradeStatus.Fail };
        }

        public static TradeResult None()
        {
            return new TradeResult { Status = TradeStatus.None };
        }
    }
}
