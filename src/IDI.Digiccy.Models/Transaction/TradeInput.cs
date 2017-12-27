using IDI.Core.Common;
using IDI.Digiccy.Common.Enums;

namespace IDI.Digiccy.Models.Transaction
{
    public class TradeInput: IInput
    {
        public int UID { get; set; }

        public TranType Type { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }
    }
}
