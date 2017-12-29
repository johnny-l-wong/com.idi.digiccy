using System;
using IDI.Digiccy.Common.Enums;

namespace IDI.Digiccy.Models.Transaction
{
    public class TradeInfo
    {
        public decimal Price { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Volume { get; set; }

        public decimal Rate
        {
            get
            {
                if (this.Open == 0)
                    return 0.00M;

                return Math.Abs(this.Price - this.Open) * 100 / this.Open;
            }
        }

        public Trend Trend
        {
            get
            {
                if (this.Price > this.Open)
                    return Trend.Rise;

                if (this.Price < this.Open)
                    return Trend.Fall;

                return Trend.Level;
            }
        }
    }
}
