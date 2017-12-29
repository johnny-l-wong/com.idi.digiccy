using System;

namespace IDI.Digiccy.Models.Transaction
{
    public class Line
    {
        public string Range { get; set; }

        public DateTime Date { get; set; }

        public long MiniDate { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal HighPrice { get; set; }

        public decimal LowPrice { get; set; }

        public decimal ClosePrice { get; set; }

        public decimal Volume { get; set; }
    }
}
