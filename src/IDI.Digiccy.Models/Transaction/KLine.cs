using System.Collections.Generic;
using Newtonsoft.Json;

namespace IDI.Digiccy.Models.Transaction
{
    public class KLine
    {
        [JsonProperty("info")]
        public TradeInfo Info { get; set; }

        [JsonProperty("depths")]
        public Depths Depths { get; set; } = new Depths();

        [JsonProperty("trades")]
        public List<Trade> Trades { get; set; } = new List<Trade>();

        [JsonProperty("lines")]
        public List<List<decimal>> Lines { get; set; } = new List<List<decimal>>();
    }
}
