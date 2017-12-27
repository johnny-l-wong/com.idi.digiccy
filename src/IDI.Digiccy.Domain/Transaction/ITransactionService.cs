using IDI.Core.Common;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction
{
    public interface ITransactionService
    {
        bool Running { get; }

        TranDetail Detail { get; }

        Depth Depth { get; }

        void Start();

        void Stop();

        Result Bid(int uid, decimal price, decimal size);

        Result Ask(int uid, decimal price, decimal size);
    }
}
