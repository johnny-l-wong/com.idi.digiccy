using System.Collections.Generic;
using System.Linq;
using IDI.Core.Utils;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction
{
    /// <summary>
    /// 交易日志记录器
    /// </summary>
    public sealed class TradeLogger : Singleton<TradeLogger>
    {
        private List<TradeLog> logs = new List<TradeLog>();

        private TradeLogger() { }

        public void Add(TradeLog log)
        {
            if (!logs.Any(e => e.SN == log.SN))
                logs.Add(log);
        }

        public List<Trade> GetTrades(int count = 100)
        {
            var trades = logs.OrderBy(e => e.Time).Take(count).Select(e => new Trade { SN = e.SN, Date = e.Time, Price = e.Price, Volume = e.Volume, Taker = e.Taker }).ToList();

            return trades;
        }
    }
}
