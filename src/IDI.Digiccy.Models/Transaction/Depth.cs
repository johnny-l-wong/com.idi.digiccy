using System.Collections.Generic;
using IDI.Digiccy.Common.Enums;
using Newtonsoft.Json;

namespace IDI.Digiccy.Models.Transaction
{
    public class Depth
    {
        [JsonProperty("asks")]
        public List<List<decimal>> Asks { get; set; } = new List<List<decimal>>();

        [JsonProperty("bids")]
        public List<List<decimal>> Bids { get; set; } = new List<List<decimal>>();
    }
}
