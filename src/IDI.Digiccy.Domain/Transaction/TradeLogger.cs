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
            info = new TradeInfo { Price = 30, Open = 30, Close = 30, High = 30, Low = 30 };
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

        public List<List<decimal>> GetKLine(KLineRange range)
        {
            if (!klines.ContainsKey(range))
                return new List<List<decimal>>();

            var lines = klines[range];

            return lines.Select(e => new List<decimal> { e.TimeScale.AsLong(), e.Open, e.High, e.Low, e.Close, e.Volume }).ToList();
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

            this.info.Price = log.Price;

            if (this.info.High < log.Price)
                this.info.High = log.Price;

            if (this.info.Low > log.Price)
                this.info.Low = log.Price;

            this.info.Volume += log.Volume;

            CreateOrUpdateKLine(log.Time, log.Volume);
        }

        public void Update()
        {
            CreateOrUpdateKLine(DateTime.Now, 0);
        }

        private void CreateOrUpdateKLine(DateTime time, decimal volume)
        {
            var ranges = typeof(KLineRange).GetEnumValues();

            foreach (KLineRange range in ranges)
            {
                var timescale = GetTimeScale(range, time);

                var line = klines[range].SingleOrDefault(e => e.TimeScale == timescale && e.Range == range);

                if (line == null)
                {
                    line = new Line { TimeScale = timescale, Range = range, Open = info.Price, Close = info.Price, High = info.Price, Low = info.Price, Volume = volume };

                    klines[range].Add(line);
                }
                else
                {
                    var index = klines[range].IndexOf(line);

                    if (klines[range][index].High < info.Price)
                        klines[range][index].High = info.Price;

                    if (klines[range][index].Low > info.Price)
                        klines[range][index].Low = info.Price;

                    klines[range][index].Close = info.Price;
                    klines[range][index].Volume += volume;
                }
            }
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
