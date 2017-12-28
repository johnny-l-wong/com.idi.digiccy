using System.Collections.Generic;
using Newtonsoft.Json;

namespace IDI.Digiccy.Models.Transaction
{
    public class Depths
    {
        [JsonProperty("asks")]
        public List<List<decimal>> Asks { get; set; } = new List<List<decimal>>();

        [JsonProperty("bids")]
        public List<List<decimal>> Bids { get; set; } = new List<List<decimal>>();
    }
}
