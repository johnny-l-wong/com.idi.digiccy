using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Domain.Transaction
{
    public interface ITransactionService
    {
        void Start();

        void Stop();

        void Bid(int uid, decimal price, decimal size);

        void Ask(int uid, decimal price, decimal size);

        void Queue();
    }
}
