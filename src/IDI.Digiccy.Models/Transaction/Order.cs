using IDI.Digiccy.Common.Enums;

namespace IDI.Digiccy.Models.Transaction
{
    public class Order
    {
        public int SN { get; set; }

        public decimal Price { get; set; }

        public decimal Volume { get; set; }

        public TransactionType Type { get; set; }
    }
}
