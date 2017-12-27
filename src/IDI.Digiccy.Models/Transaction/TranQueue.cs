﻿using System.Collections.Generic;
using IDI.Digiccy.Common.Enums;

namespace IDI.Digiccy.Models.Transaction
{
    public class TranQueue
    {
        public class Item
        {
            public int SN { get; set; }

            public decimal Price { get; set; }

            public decimal Volume { get; set; }

            public TranType Type { get; set; }
        }

        public List<Item> Asks { get; set; } = new List<Item>();

        public List<Item> Bids { get; set; } = new List<Item>();
    }
}
