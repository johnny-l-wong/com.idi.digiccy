﻿using IDI.Digiccy.Models.Base;

namespace IDI.Digiccy.Domain.Transaction
{
    public static class Extensions
    {
        public static decimal Remain(this TransactionOrder order)
        {
            return order.Size - order.Volume;
        }
    }
}
