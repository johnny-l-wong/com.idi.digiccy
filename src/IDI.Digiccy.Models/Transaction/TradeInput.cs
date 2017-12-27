using IDI.Core.Common;
using IDI.Digiccy.Common.Enums;
using Newtonsoft.Json;

namespace IDI.Digiccy.Models.Transaction
{
    public class TradeInput: IInput
    {
        [JsonProperty("uid")]
        public int UID { get; set; }

        [JsonProperty("tran_type")]
        public TranType Type { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("size")]
        public decimal Size { get; set; }
    }
}
