using System;
using System.Collections.Generic;
using IDI.Digiccy.Common.Enums;
using Newtonsoft.Json;

namespace IDI.Digiccy.Models.Transaction
{
    public class TranDetail
    {
        public class Item
        {
            [JsonProperty("time")]
            public string Time => this.Date.ToString("HH:mm:ss");

            [JsonProperty("price")]
            public decimal Price { get; set; }

            [JsonProperty("volume")]
            public decimal Volume { get; set; }

            [JsonProperty("isbuy")]
            public bool IsBuy => Taker == Counterparty.Buyer;

            [JsonIgnore]
            public DateTime Date { get; set; }

            [JsonIgnore]
            public Counterparty Taker { get; set; }
        }

        public List<Item> Trades { get; set; } = new List<Item>();
    }
}
