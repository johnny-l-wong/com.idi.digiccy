using IDI.Digiccy.Models;

namespace IDI.Digiccy.Domain
{
    /// <summary>
    /// 撮合成交回报
    /// </summary>
    public class ExecutionReport
    {
        /// <summary>
        /// 定单
        /// </summary>
        public BidOrder BidOrder { get; set; }

        /// <summary>
        /// 应单
        /// </summary>
        public AskOrder AskOrder { get; set; }

        /// <summary>
        /// 撮合价
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// 佣金
        /// </summary>
        public decimal Commission { get; private set; }
    }
}
