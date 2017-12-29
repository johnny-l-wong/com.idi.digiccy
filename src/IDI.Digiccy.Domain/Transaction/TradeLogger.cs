using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Core.Extensions;
using IDI.Core.Utils;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction
{
    /// <summary>
    /// 交易日志记录器
    /// </summary>
    public sealed class TradeLogger : Singleton<TradeLogger>
    {
        private List<TradeLog> logs;
        private TradeInfo info;
        private Dictionary<KLineRange, List<Line>> klines;

        private TradeLogger()
        {
            Init();
        }

        public void Init()
        {
            info = new TradeInfo();
            logs = new List<TradeLog>();
            klines = new Dictionary<KLineRange, List<Line>>();

            var ranges = typeof(KLineRange).GetEnumValues();

            foreach (KLineRange range in ranges)
            {
                klines.Add(range, new List<Line>());
            }
        }

        public TradeInfo GetInfo()
        {
            return info;
        }

        public List<Line> GetKLine(KLineRange range)
        {
            if (!klines.ContainsKey(range))
                return new List<Line>();

            return klines[range];
        }

        public List<Trade> GetTrades(int count = 100)
        {
            var trades = logs.OrderBy(e => e.Time).Take(count).Select(e => new Trade { SN = e.SN, Date = e.Time, Price = e.Price, Volume = e.Volume, Taker = e.Taker }).ToList();

            return trades;
        }

        public void Update(TradeLog log)
        {
            if (!logs.Any(e => e.SN == log.SN))
            {
                logs.Add(log);
            }
        }

        public void Update()
        {

        }

        public DateTime GetTimeScale(KLineRange range, DateTime time)
        {
            DateTime timescale = time;

            switch (range)
            {
                case KLineRange.D30:
                    timescale = time.FirstDayOfMonth();
                    break;
                case KLineRange.D7:
                    timescale = time.FirstDayOfWeek();
                    break;
                case KLineRange.D1:
                    timescale = time.Date;
                    break;
                case KLineRange.H1:
                    timescale = time.ToString("yyyy-MM-dd HH:00:00").AsDateTime();
                    break;
                case KLineRange.M30:
                    timescale = time.ToString("yyyy-MM-dd HH:00:00").AsDateTime().AddMinutes(Math.Floor((double)time.Minute / 30) * 30);
                    break;
                case KLineRange.M15:
                    timescale = time.ToString("yyyy-MM-dd HH:00:00").AsDateTime().AddMinutes(Math.Floor((double)time.Minute / 15) * 15);
                    break;
                case KLineRange.M5:

                    timescale = time.ToString("yyyy-MM-dd HH:00:00").AsDateTime().AddMinutes(Math.Floor((double)time.Minute / 5) * 5);
                    break;
                case KLineRange.M1:
                    timescale = time.ToString("yyyy-MM-dd HH:mm:00").AsDateTime().AddMinutes(time.Second > 0 ? 1 : 0);
                    break;
                default:
                    break;
            }

            return timescale;
        }
    }
}
