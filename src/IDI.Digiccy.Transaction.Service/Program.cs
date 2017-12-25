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

            device.Start();
        }

        private static void OnTransactionCompleted(TranResult result)
        {
            if (result.Status != TranStatus.Success)
                return;

            Console.WriteLine($"Price             Volume             Taker             ");

            foreach (var item in result.Items)
            {
                Console.WriteLine($"{item.Price} {item.Volume} {item.Taker}");
            }

            Console.WriteLine("------------------------------------------------------------");
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
