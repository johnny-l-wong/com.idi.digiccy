using Newtonsoft.Json;

namespace IDI.Digiccy.Models.Transaction
{
    public class Quotation
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("info")]
        public TradeInfo Info { get; set; }

        [JsonProperty("data")]
        public KLine KLine { get; set; } = new KLine();

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
