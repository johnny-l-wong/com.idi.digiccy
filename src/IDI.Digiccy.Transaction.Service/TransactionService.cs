using System;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Domain.Transaction;
using IDI.Digiccy.Models.Base;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Transaction.Service
{
    public class TransactionService: ITransactionService
    {
        private readonly TransactionDevice device;

        public TransactionService()
        {
            device = new TransactionDevice();
            device.DeviceStart += OnDeviceStart;
            device.DeviceStop += OnDeviceStop;
            device.BidCompleted += OnBidCompleted;
            device.AskCompleted += OnAskCompleted;
            device.TransactionCompleted += OnTransactionCompleted;
        }

        private void OnBidCompleted(TransactionOrder order)
        {
            Line();
            Console.WriteLine($"{"Type",-10} {"UID",-10} {"Price",-10} {"Size",-10}");
            Console.WriteLine($"{order.Type,-10} {order.UID,-10} {order.Price,-10} {order.Size,-10}");
            Line();
        }

        private void OnAskCompleted(TransactionOrder order)
        {
            Line();
            Console.WriteLine($"{"Type",-10} {"UID",-10} {"Price",-10} {"Size",-10}");
            Console.WriteLine($"{order.Type,-10} {order.UID,-10} {order.Price,-10} {order.Size,-10}");
            Line();
        }

        public void Start()
        {
            device.Start();
        }

        public void Stop()
        {
            device.Stop();
        }

        public void Bid(int uid, decimal price, decimal size)
        {
            device.Bid(uid, price, size);
        }

        public void Ask(int uid, decimal price, decimal size)
        {
            device.Ask(uid, price, size);
        }

        private void Line()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }

        private void OnTransactionCompleted(TransactionResult result)
        {
            if (result.Status != TransactionStatus.Success)
                return;

            Line();
            Console.WriteLine($"{"Bid",-10} {"Bid.Price",-10} {"Ask",-10} {"Ask.Price",-10} {"Price",10} {"Volume",10} {"Taker",-10}");

            foreach (var item in result.Items)
            {
                Console.WriteLine($"{item.Bid.UID,-10} {item.Bid.Price,-10} {item.Ask.UID,-10} {item.Ask.Price,-10} {item.Price,10} {item.Volume,10} {item.Taker,-10}");
            }

            Line();
        }

        private void OnDeviceStop()
        {
            Console.WriteLine(">>>Stop");
        }

        private void OnDeviceStart()
        {
            Console.WriteLine(">>>Start");
        }
    }
}
