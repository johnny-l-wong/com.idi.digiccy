using System;
using IDI.Core.Extensions;
using IDI.Digiccy.Common.Enums;

namespace IDI.Digiccy.Models.Transaction
{
    /// <summary>
    /// 交易日志
    /// </summary>
    public class TradeLog
    {
        /// <summary>
        /// 交易流水号
        /// </summary>
        public long SN => Time.AsLong();

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

        public TradeParty Taker { get; set; }

        /// <summary>
        /// 成交时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
