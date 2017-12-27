using System;
using System.Collections.Generic;
using IDI.Digiccy.Common.Enums;

namespace IDI.Digiccy.Models.Transaction
{
    public class TranDetail
    {
        public class Item
        {
            public DateTime Date { get; set; }

            public decimal Price { get; set; }

            public decimal Volume { get; set; }

            public Counterparty Taker { get; set; }
        }

        public List<Item> Items { get; set; } = new List<Item>();
    }
}
