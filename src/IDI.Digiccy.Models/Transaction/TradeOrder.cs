using System;
using IDI.Core.Utils;
using IDI.Digiccy.Common.Enums;

namespace IDI.Digiccy.Models.Base
{
    /// <summary>
    /// 交易单
    /// </summary>
    public abstract class TradeOrder
    {
        /// <summary>
        /// 交易单号
        /// </summary>
        public long TranNo { get; private set; }

        /// <summary>
        /// 委托人编号
        /// </summary>
        public int UID { get; private set; }

        /// <summary>
        /// 价位
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// 大小
        /// </summary>
        public decimal Size { get; private set; }

        /// <summary>
        /// 成交量
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public TranType Type { get; private set; }

        public DateTime Date { get; private set; }

        public TradeOrder(int uid, decimal price, decimal size, TranType type)
        {
            this.TranNo = SFID.NewID();
            this.UID = uid;
            this.Price = price;
            this.Size = size;
            this.Type = type;
            this.Date = DateTime.Now;
        }
    }
}
