using IDI.Core.Common;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction.Services
{
    public interface ITransactionService
    {
        bool Running { get; }

        void Start();

        void Stop();

        Result Bid(int uid, decimal price, decimal size);

        Result Ask(int uid, decimal price, decimal size);

        Result<KLine> GetKLine(KLineRange range);
    }
}
