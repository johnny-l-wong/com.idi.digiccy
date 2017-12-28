using System.Collections.Generic;
using Newtonsoft.Json;

namespace IDI.Digiccy.Models.Transaction
{
    public class TranInfo
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("depths")]
        public Depth Depths { get; set; } = new Depth();

        [JsonProperty("detail")]
        public TranDetail Detail { get; set; } = new TranDetail();

        [JsonProperty("lines")]
        public List<List<decimal>> Klines { get; set; } = new List<List<decimal>>();
    }
}
