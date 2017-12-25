using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Domain.Transaction;
using IDI.Digiccy.Models.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Digiccy.Domain.Tests
{
    [TestClass]
    public class TestMatchMaker
    {
        private MatchMaker maker;

        [TestInitialize]
        public void Init()
        {
            TranQueue.Instance.Clear();
            maker = new MatchMaker();
        }

        [TestMethod]
        public void Should_Do_Success_WhenTakerIsSeller()
        {
            var bid = new BidOrder(10000, 10, 100);
            var ask = new AskOrder(10002, 9, 100);

            TranQueue.Instance.EnQueue(bid);
            TranQueue.Instance.EnQueue(ask);

            var result = maker.Do();

            Assert.AreEqual(TranStatus.Success, result.Status);
            Assert.AreEqual(1, result.Items.Count);
            Assert.AreEqual(10M, result.Items[0].Price);
            Assert.AreEqual(100, result.Items[0].Volume);
            Assert.AreEqual(Counterparty.Seller, result.Items[0].Taker);
        }

        [TestMethod]
        public void Should_Do_Success_WhenTakerIsBuyer()
        {
            var ask = new AskOrder(10002, 9, 100);
            var bid = new BidOrder(10000, 10, 100);

            TranQueue.Instance.EnQueue(ask);
            TranQueue.Instance.EnQueue(bid);

            var result = maker.Do();

            Assert.AreEqual(TranStatus.Success, result.Status);
            Assert.AreEqual(1, result.Items.Count);
            Assert.AreEqual(9M, result.Items[0].Price);
            Assert.AreEqual(100, result.Items[0].Volume);
            Assert.AreEqual(Counterparty.Buyer, result.Items[0].Taker);
        }
    }
}
