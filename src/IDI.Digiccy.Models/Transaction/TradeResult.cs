using System;
using System.Collections.Generic;
using IDI.Digiccy.Common.Enums;

namespace IDI.Digiccy.Models.Transaction
{
    /// <summary>
    /// 交易结果 
    /// </summary>
    public class TradeResult
    {
        public class Log
        {
            /// <summary>
            /// 交易流水号
            /// </summary>
            public int SN => int.Parse(Time.ToString("yyyyMMddHHmmssfff"));

            /// <summary>
            /// 买单
            /// </summary>
            public BidOrder Bid { get; set; }

            /// <summary>
            /// 卖单
            /// </summary>
            public AskOrder Ask { get; set; }

            /// <summary>
            /// 成交价
            /// </summary>
            public decimal Price { get; set; }

            /// <summary>
            /// 成交量
            /// </summary>
            public decimal Volume { get; set; }

            public Counterparty Taker { get; set; }

            /// <summary>
            /// 成交时间
            /// </summary>
            public DateTime Time { get; set; }
        }

        /// <summary>
        /// 交易日志
        /// </summary>
        public List<Log> Logs { get; set; } = new List<Log>();

        public TranStatus Status { get; set; }

        public static TradeResult Success(List<Log> items)
        {
            return new TradeResult { Logs= items, Status = TranStatus.Success };
        }

        public static TradeResult Fail()
        {
            return new TradeResult { Status = TranStatus.Fail };
        }

        public static TradeResult None()
        {
            return new TradeResult { Status = TranStatus.None };
        }
    }
}
