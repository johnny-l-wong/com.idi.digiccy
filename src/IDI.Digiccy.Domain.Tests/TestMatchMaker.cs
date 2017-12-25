using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Domain.Transaction;
using IDI.Digiccy.Models.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Digiccy.Domain.Tests
{
    [TestClass]
    public class TestMatchMaker
    {
        private Matchmaker maker;

        [TestInitialize]
        public void Init()
        {
            TranQueue.Instance.Clear();
            maker = new Matchmaker();
        }

        [TestMethod]
        public void Should_Do_Success_WhenTakerIsSeller()
        {
            var bid = new BidOrder(10001, 10, 100);
            var ask = new AskOrder(10002, 9, 100);

            TranQueue.Instance.EnQueue(bid);
            TranQueue.Instance.EnQueue(ask);

            var result = maker.Do();

            Assert.AreEqual(TranStatus.Success, result.Status);
            Assert.AreEqual(1, result.Items.Count);
            Assert.AreEqual(10M, result.Items[0].Price);
            Assert.AreEqual(100, result.Items[0].Volume);
            Assert.AreEqual(Counterparty.Seller, result.Items[0].Taker);
            Assert.AreEqual(ask, result.Items[0].Ask);
            Assert.AreEqual(ask.Volume, result.Items[0].Ask.Volume);
            Assert.AreEqual(bid, result.Items[0].Bid);
            Assert.AreEqual(bid.Volume, result.Items[0].Bid.Volume);
        }

        [TestMethod]
        public void Should_Do_Success_WhenTakerIsBuyer()
        {
            var ask = new AskOrder(10002, 9, 100);
            var bid = new BidOrder(10001, 10, 100);

            TranQueue.Instance.EnQueue(ask);
            TranQueue.Instance.EnQueue(bid);

            var result = maker.Do();

            Assert.AreEqual(TranStatus.Success, result.Status);
            Assert.AreEqual(1, result.Items.Count);
            Assert.AreEqual(9M, result.Items[0].Price);
            Assert.AreEqual(100, result.Items[0].Volume);
            Assert.AreEqual(Counterparty.Buyer, result.Items[0].Taker);
            Assert.AreEqual(ask, result.Items[0].Ask);
            Assert.AreEqual(ask.Volume, result.Items[0].Ask.Volume);
            Assert.AreEqual(bid, result.Items[0].Bid);
            Assert.AreEqual(bid.Volume, result.Items[0].Bid.Volume);
        }

        [TestMethod]
        public void Should_Do_Success_WhenBidOrderPartialTransaction()
        {
            var bid = new BidOrder(10001, 10, 100);
            var ask1 = new AskOrder(10002, 9, 50);
            var ask2 = new AskOrder(10003, 9, 45);

            TranQueue.Instance.EnQueue(bid);
            TranQueue.Instance.EnQueue(ask1);
            TranQueue.Instance.EnQueue(ask2);

            var result = maker.Do();

            Assert.AreEqual(TranStatus.Success, result.Status);
            Assert.AreEqual(2, result.Items.Count);

            Assert.AreEqual(10M, result.Items[0].Price);
            Assert.AreEqual(50, result.Items[0].Volume);
            Assert.AreEqual(Counterparty.Seller, result.Items[0].Taker);

            Assert.AreEqual(10M, result.Items[1].Price);
            Assert.AreEqual(45, result.Items[1].Volume);
            Assert.AreEqual(Counterparty.Seller, result.Items[1].Taker);

            Assert.AreEqual(5, bid.Remain());
        }

        [TestMethod]
        public void Should_Do_Success_WhenAskOrderPartialTransaction()
        {
            var ask = new AskOrder(10001, 9, 100);
            var bid1 = new BidOrder(10002, 10, 50);
            var bid2 = new BidOrder(10003, 10, 45);

            TranQueue.Instance.EnQueue(ask);
            TranQueue.Instance.EnQueue(bid1);
            TranQueue.Instance.EnQueue(bid2);

            var result = maker.Do();

            Assert.AreEqual(TranStatus.Success, result.Status);
            Assert.AreEqual(2, result.Items.Count);

            Assert.AreEqual(9M, result.Items[0].Price);
            Assert.AreEqual(50, result.Items[0].Volume);
            Assert.AreEqual(Counterparty.Buyer, result.Items[0].Taker);

            Assert.AreEqual(9M, result.Items[1].Price);
            Assert.AreEqual(45, result.Items[1].Volume);
            Assert.AreEqual(Counterparty.Buyer, result.Items[1].Taker);

            Assert.AreEqual(5, ask.Remain());
        }
    }
}
