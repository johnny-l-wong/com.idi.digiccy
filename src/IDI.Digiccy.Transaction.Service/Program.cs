using System;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Transaction.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var device = new TransactionDevice();
            device.DeviceStart += OnDeviceStart;
            device.DeviceStop += OnDeviceStop;
            device.TransactionCompleted += OnTransactionCompleted;

            device.Bid(10001, 10, 100);
            device.Ask(10002, 10, 100);

            device.Start();
        }

        private static void OnTransactionCompleted(TranResult result)
        {
            if (result.Status != TranStatus.Success)
                return;

            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Bid",-10} {"Bid.Price",-10} {"Ask",-10} {"Ask.Price",-10} {"Price",10} {"Volume",10} {"Taker",-10}");

            foreach (var item in result.Items)
            {
                Console.WriteLine($"{item.Bid.UID,-10} {item.Bid.Price,-10} {item.Ask.UID,-10} {item.Ask.Price,-10} {item.Price,10} {item.Volume,10} {item.Taker,-10}");
            }

            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }

        private static void OnDeviceStop()
        {
            Console.WriteLine(">>>Stop");
        }

        private static void OnDeviceStart()
        {
            Console.WriteLine(">>>Start");
        }
    }
}
