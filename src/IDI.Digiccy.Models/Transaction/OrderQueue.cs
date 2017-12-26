using System.Collections.Generic;

namespace IDI.Digiccy.Models.Transaction
{
    public class OrderQueue
    {
        public List<Order> Asks { get; set; } = new List<Order>();

        public List<Order> Bids { get; set; } = new List<Order>();
    }
}
