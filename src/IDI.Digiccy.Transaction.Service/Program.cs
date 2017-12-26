using System;
using System.Threading.Tasks;
using IDI.Digiccy.Domain.Transaction;

namespace IDI.Digiccy.Transaction.Service
{
    class Program
    {
        private static ITransactionService service;

        static void Main(string[] args)
        {
            service = new TransactionService();

            string cmd = string.Empty;

            while (cmd != "exit")
            {
                cmd = Console.ReadLine();

                if (cmd == "start")
                {
                    Task.Factory.StartNew(service.Start);
                }

                if (cmd == "stop")
                {
                    service.Stop();
                }

                if (cmd == "queue")
                {
                    service.Queue();
                }

                if (cmd.StartsWith("bid,") || cmd.StartsWith("ask,"))
                {
                    Entrust(cmd);
                }
            }
            Console.ReadKey();
        }

        static void Entrust(string cmd)
        {
            var args = cmd.Split(",");

            if (args.Length != 4)
                return;

            int uid = 0; decimal price, size;

            if (int.TryParse(args[1], out uid) && decimal.TryParse(args[2], out price) && decimal.TryParse(args[3], out size))
            {
                switch (args[0])
                {
                    case "bid":
                        service.Bid(uid, price, size);
                        break;
                    case "ask":
                        service.Ask(uid, price, size);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
