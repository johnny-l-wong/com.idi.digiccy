using IDI.Digiccy.Models.Base;

namespace IDI.Digiccy.Domain.Transaction
{
    public static class Extensions
    {
        public static decimal Remain(this TranOrder order)
        {
            return order.Size - order.Volume;
        }
    }
}
