using System;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Domain.Transaction;
using IDI.Digiccy.Models.Base;
using IDI.Digiccy.Models.Transaction;

namespace IDI.Digiccy.Transaction.WindowsService
{
    public class TransactionService : ITransactionService
    {
        //private readonly TransactionDevice device;

        public TransactionDevice Device => TransactionDevice.Instance;

        public TransactionService()
        {
            //device = new TransactionDevice();
            //device.DeviceStart += OnDeviceStart;
            //device.DeviceStop += OnDeviceStop;
            //device.BidEnqueue += OnBidEnqueue;
            //device.AskEnqueue += OnAskEnqueue;
            //device.TransactionCompleted += OnTransactionCompleted;

            Device.DeviceStart += OnDeviceStart;
            Device.DeviceStop += OnDeviceStop;
            Device.BidEnqueue += OnBidEnqueue;
            Device.AskEnqueue += OnAskEnqueue;
            Device.TransactionCompleted += OnTransactionCompleted;
        }

        private void OnBidEnqueue(TranOrder order)
        {
            Line("bid");
            Console.WriteLine($"{"Type",-10} {"UID",-10} {"Price",-10} {"Size",-10}");
            Console.WriteLine($"{order.Type,-10} {order.UID,-10} {order.Price,-10} {order.Size,-10}");
            Line();
        }

        private void OnAskEnqueue(TranOrder order)
        {
            Line("ask");
            Console.WriteLine($"{"Type",-10} {"UID",-10} {"Price",-10} {"Size",-10}");
            Console.WriteLine($"{order.Type,-10} {order.UID,-10} {order.Price,-10} {order.Size,-10}");
            Line();
        }

        public void Start()
        {
            Device.Start();
        }

        public void Stop()
        {
            Device.Stop();
        }

        public void Bid(int uid, decimal price, decimal size)
        {
            Device.Bid(uid, price, size);
        }

        public void Ask(int uid, decimal price, decimal size)
        {
            Device.Ask(uid, price, size);
        }

        public void Queue()
        {
            var queue = Device.Queue();

            Line("queue");
            Console.WriteLine($"{"Buy/Sell",-10} {"Price",-10} {"Volume",-10}");
            queue.Asks.ForEach(e => Console.WriteLine($"sell{e.SN,-6} {e.Price,-10} {e.Volume,-10}"));
            queue.Bids.ForEach(e => Console.WriteLine($"buy{e.SN,-7} {e.Price,-10} {e.Volume,-10}"));
            Line();
        }

        private void Line(string caption = "-")
        {
            Console.WriteLine($"------------------------------{caption}------------------------------");
        }

        private void OnTransactionCompleted(TranResult result)
        {
            if (result.Status != TranStatus.Success)
                return;

            Line("transaction completed");
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
