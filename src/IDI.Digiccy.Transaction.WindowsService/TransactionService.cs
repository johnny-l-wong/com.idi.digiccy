using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDI.Digiccy.Domain.Transaction;

namespace IDI.Digiccy.Transaction.WindowsService
{
    public class TransactionService : ITransactionService
    {
        public void Ask(int uid, decimal price, decimal size)
        {
            throw new NotImplementedException();
        }

        public void Bid(int uid, decimal price, decimal size)
        {
            throw new NotImplementedException();
        }

        public void Queue()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
