using System.Collections.Generic;
using IDI.Digiccy.Common.Enums;

namespace IDI.Digiccy.Models.Transaction
{
    /// <summary>
    /// 撮合结果 
    /// Order Execution & Trade Confirmation
    /// </summary>
    public class TransactionResult
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

        public TransactionStatus Status { get; set; }

        public static TransactionResult Success(List<Item> items)
        {
            return new TransactionResult { Items= items, Status = TransactionStatus.Success };
        }

        public static TransactionResult Fail()
        {
            return new TransactionResult { Status = TransactionStatus.Fail };
        }

        public static TransactionResult None()
        {
            return new TransactionResult { Status = TransactionStatus.None };
        }
    }
}
