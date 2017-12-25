using System.Collections.Generic;
using IDI.Digiccy.Common.Enums;

namespace IDI.Digiccy.Models.Transaction
{
    /// <summary>
    /// 撮合结果 
    /// Order Execution & Trade Confirmation
    /// </summary>
    public class TranResult
    {
        public class Item
        {
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
        }

        public List<Item> Items { get; set; } = new List<Item>();

        public TranStatus Status { get; set; }

        public static TranResult Success(List<Item> items)
        {
            return new TranResult { Items= items, Status = TranStatus.Success };
        }

        public static TranResult Fail()
        {
            return new TranResult { Status = TranStatus.Fail };
        }

        public static TranResult None()
        {
            return new TranResult { Status = TranStatus.None };
        }
    }
}
