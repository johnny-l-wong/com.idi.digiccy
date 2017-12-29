using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Domain.Transaction;
using IDI.Digiccy.Models.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Digiccy.Domain.Tests
{
    [TestClass]
    public class TestMatchmaker
    {
        private Matchmaker maker;

        [TestInitialize]
        public void Init()
        {
            TradeQueue.Instance.Clear();
            maker = new Matchmaker();
        }

        [TestMethod]
        public void Should_Do_None_WhenAskUnmatched()
        {
            var bid = new BidOrder(10001, 10, 100);
            var ask = new AskOrder(10002, 11, 100);

            TradeQueue.Instance.Enqueue(bid);
            TradeQueue.Instance.Enqueue(ask);

            var result = maker.Do();

            Assert.AreEqual(TradeStatus.None, result.Status);
            Assert.AreEqual(0, result.Logs.Count);
        }

        [TestMethod]
        public void Should_Do_None_WhenBidUnmatched()
        {
            var ask = new AskOrder(10001, 11, 100);
            var bid = new BidOrder(10002, 10, 100);

            TradeQueue.Instance.Enqueue(bid);
            TradeQueue.Instance.Enqueue(ask);

            var result = maker.Do();

            Assert.AreEqual(TradeStatus.None, result.Status);
            Assert.AreEqual(0, result.Logs.Count);
        }

        [TestMethod]
        public void Should_Do_Success_WhenTakerIsSeller()
        {
            var bid = new BidOrder(10001, 10, 100);
            var ask = new AskOrder(10002, 9, 100);

            TradeQueue.Instance.Enqueue(bid);
            TradeQueue.Instance.Enqueue(ask);

            var result = maker.Do();
            Assert.AreEqual(TradeStatus.None, result.Status);

            result = maker.Do();
            Assert.AreEqual(TradeStatus.Success, result.Status);
            Assert.AreEqual(1, result.Logs.Count);
            Assert.AreEqual(10M, result.Logs[0].Price);
            Assert.AreEqual(100, result.Logs[0].Volume);
            Assert.AreEqual(TradeParty.Seller, result.Logs[0].Taker);
            Assert.AreEqual(ask, result.Logs[0].Ask);
            Assert.AreEqual(ask.Volume, result.Logs[0].Ask.Volume);
            Assert.AreEqual(bid, result.Logs[0].Bid);
            Assert.AreEqual(bid.Volume, result.Logs[0].Bid.Volume);
        }

        [TestMethod]
        public void Should_Do_Success_WhenTakerIsBuyer()
        {
            var ask = new AskOrder(10002, 9, 100);
            var bid = new BidOrder(10001, 10, 100);

            TradeQueue.Instance.Enqueue(ask);
            TradeQueue.Instance.Enqueue(bid);

            var result = maker.Do();
            Assert.AreEqual(TradeStatus.None, result.Status);

            result = maker.Do();
            Assert.AreEqual(TradeStatus.Success, result.Status);
            Assert.AreEqual(1, result.Logs.Count);
            Assert.AreEqual(9M, result.Logs[0].Price);
            Assert.AreEqual(100, result.Logs[0].Volume);
            Assert.AreEqual(TradeParty.Buyer, result.Logs[0].Taker);
            Assert.AreEqual(ask, result.Logs[0].Ask);
            Assert.AreEqual(ask.Volume, result.Logs[0].Ask.Volume);
            Assert.AreEqual(bid, result.Logs[0].Bid);
            Assert.AreEqual(bid.Volume, result.Logs[0].Bid.Volume);
        }

        [TestMethod]
        public void Should_Do_Success_WhenBidOrderPartialTransaction()
        {
            var bid = new BidOrder(10001, 10, 100);
            var ask1 = new AskOrder(10002, 9, 50);
            var ask2 = new AskOrder(10003, 9, 45);

            TradeQueue.Instance.Enqueue(bid);
            TradeQueue.Instance.Enqueue(ask1);
            TradeQueue.Instance.Enqueue(ask2);

            var result = maker.Do();

            Assert.AreEqual(TradeStatus.None, result.Status);

            result = maker.Do();
            Assert.AreEqual(TradeStatus.Success, result.Status);
            Assert.AreEqual(1, result.Logs.Count);
            Assert.AreEqual(10M, result.Logs[0].Price);
            Assert.AreEqual(50, result.Logs[0].Volume);
            Assert.AreEqual(TradeParty.Seller, result.Logs[0].Taker);

            result = maker.Do();
            Assert.AreEqual(TradeStatus.Success, result.Status);
            Assert.AreEqual(1, result.Logs.Count);
            Assert.AreEqual(10M, result.Logs[0].Price);
            Assert.AreEqual(45, result.Logs[0].Volume);
            Assert.AreEqual(TradeParty.Seller, result.Logs[0].Taker);

            Assert.AreEqual(5, bid.Remain());
        }

        [TestMethod]
        public void Should_Do_Success_WhenAskOrderPartialTransaction()
        {
            var ask = new AskOrder(10001, 9, 100);
            var bid1 = new BidOrder(10002, 10, 50);
            var bid2 = new BidOrder(10003, 10, 45);

            TradeQueue.Instance.Enqueue(ask);
            TradeQueue.Instance.Enqueue(bid1);
            TradeQueue.Instance.Enqueue(bid2);

            var result = maker.Do();

            Assert.AreEqual(TradeStatus.None, result.Status);

            result = maker.Do();

            Assert.AreEqual(TradeStatus.Success, result.Status);
            Assert.AreEqual(1, result.Logs.Count);
            Assert.AreEqual(9M, result.Logs[0].Price);
            Assert.AreEqual(50, result.Logs[0].Volume);
            Assert.AreEqual(TradeParty.Buyer, result.Logs[0].Taker);

            result = maker.Do();
            Assert.AreEqual(TradeStatus.Success, result.Status);
            Assert.AreEqual(1, result.Logs.Count);
            Assert.AreEqual(9M, result.Logs[0].Price);
            Assert.AreEqual(45, result.Logs[0].Volume);
            Assert.AreEqual(TradeParty.Buyer, result.Logs[0].Taker);

            Assert.AreEqual(5, ask.Remain());
        }

        [TestMethod]
        public void Should_Change_Depth_WhenUnmatched()
        {
            var bid = new BidOrder(10001, 10, 100);
            var ask = new AskOrder(10002, 11, 100);

            TradeQueue.Instance.Enqueue(bid);
            TradeQueue.Instance.Enqueue(ask);

            var result = maker.Do();
            var depth = TradeQueue.Instance.GetDepths();

            Assert.AreEqual(TradeStatus.None, result.Status);
            Assert.AreEqual(0, result.Logs.Count);
            Assert.AreEqual(1, depth.Bids.Count);

            result = maker.Do();
            depth = TradeQueue.Instance.GetDepths();

            Assert.AreEqual(TradeStatus.None, result.Status);
            Assert.AreEqual(0, result.Logs.Count);
            Assert.AreEqual(1, depth.Bids.Count);
            Assert.AreEqual(1, depth.Asks.Count);
        }
    }
}
