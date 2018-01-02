using System;
using IDI.Digiccy.Common.Enums;
using Newtonsoft.Json;

namespace IDI.Digiccy.Models.Transaction
{
    public class TradeInfo
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("open")]
        public decimal Open { get; set; }

        [JsonProperty("close")]
        public decimal Close { get; set; }

        [JsonProperty("high")]
        public decimal High { get; set; }

        [JsonProperty("low")]
        public decimal Low { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        [JsonProperty("rate")]
        public decimal Rate
        {
            get
            {
                if (this.Open == 0)
                    return 0.00M;

                return Math.Abs(this.Price - this.Open) * 100 / this.Open;
            }
        }

        [JsonProperty("trend")]
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
