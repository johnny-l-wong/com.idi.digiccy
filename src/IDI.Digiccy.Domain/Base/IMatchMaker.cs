using System.Collections.Generic;
using IDI.Digiccy.Models.Base;

namespace IDI.Digiccy.Domain.Base
{
    public interface IMatchMaker
    {
        bool Running { get; }

        List<TranOrder> Queue { get; }

        void EnQueue(TranOrder item);

        void Start();

        void Stop();
    }
}
