using System;
using IDI.Digiccy.Common.Enums;
using IDI.Digiccy.Domain.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Digiccy.Domain.Tests.Transaction
{
    [TestClass]
    public class TradeLoggerUnitTest
    {
        [TestInitialize]
        public void Init()
        {
            TradeLogger.Instance.Init();
        }

        [TestMethod]
        public void Should_GetTimeScale_D30()
        {
            Assert.AreEqual(new DateTime(2017, 1, 1), TradeLogger.Instance.GetTimeScale(KLineRange.D30, new DateTime(2017, 1, 1)));
            Assert.AreEqual(new DateTime(2017, 2, 1), TradeLogger.Instance.GetTimeScale(KLineRange.D30, new DateTime(2017, 2, 2)));
            Assert.AreEqual(new DateTime(2017, 7, 1), TradeLogger.Instance.GetTimeScale(KLineRange.D30, new DateTime(2017, 7, 8)));
        }

        [TestMethod]
        public void Should_GetTimeScale_D7()
        {
            Assert.AreEqual(new DateTime(2016, 12, 26), TradeLogger.Instance.GetTimeScale(KLineRange.D7, new DateTime(2017, 1, 1)));
            Assert.AreEqual(new DateTime(2017, 1, 2), TradeLogger.Instance.GetTimeScale(KLineRange.D7, new DateTime(2017, 1, 2)));
            Assert.AreEqual(new DateTime(2017, 1, 2), TradeLogger.Instance.GetTimeScale(KLineRange.D7, new DateTime(2017, 1, 8)));
        }

        [TestMethod]
        public void Should_GetTimeScale_D1()
        {
            Assert.AreEqual(new DateTime(2017, 1, 1), TradeLogger.Instance.GetTimeScale(KLineRange.D1, new DateTime(2017, 1, 1, 00, 00, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1), TradeLogger.Instance.GetTimeScale(KLineRange.D1, new DateTime(2017, 1, 1, 59, 59, 59)));
            Assert.AreEqual(new DateTime(2017, 1, 2), TradeLogger.Instance.GetTimeScale(KLineRange.D1, new DateTime(2017, 1, 2, 00, 00, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 2), TradeLogger.Instance.GetTimeScale(KLineRange.D1, new DateTime(2017, 1, 2, 59, 59, 59)));
        }

        [TestMethod]
        public void Should_GetTimeScale_M30()
        {
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M30, new DateTime(2017, 1, 1, 00, 00, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M30, new DateTime(2017, 1, 1, 00, 29, 59)));

            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 30, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M30, new DateTime(2017, 1, 1, 00, 30, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 30, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M30, new DateTime(2017, 1, 1, 00, 59, 59)));
        }

        public void Should_GetTimeScale_H1()
        {
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.H1, new DateTime(2017, 1, 1, 00, 00, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.H1, new DateTime(2017, 1, 1, 00, 59, 59)));

            Assert.AreEqual(new DateTime(2017, 1, 1, 01, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.H1, new DateTime(2017, 1, 1, 01, 00, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 01, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.H1, new DateTime(2017, 1, 1, 01, 59, 59)));
        }


        [TestMethod]
        public void Should_GetTimeScale_M15()
        {
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M15, new DateTime(2017, 1, 1, 00, 00, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M15, new DateTime(2017, 1, 1, 00, 14, 59)));

            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 15, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M15, new DateTime(2017, 1, 1, 00, 15, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 15, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M15, new DateTime(2017, 1, 1, 00, 29, 59)));

            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 30, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M15, new DateTime(2017, 1, 1, 00, 30, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 30, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M15, new DateTime(2017, 1, 1, 00, 44, 59)));

            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 45, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M15, new DateTime(2017, 1, 1, 00, 45, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 45, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M15, new DateTime(2017, 1, 1, 00, 59, 59)));
        }

        [TestMethod]
        public void Should_GetTimeScale_M5()
        {
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M5, new DateTime(2017, 1, 1, 00, 00, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M5, new DateTime(2017, 1, 1, 00, 00, 01)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M5, new DateTime(2017, 1, 1, 00, 04, 59)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 05, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M5, new DateTime(2017, 1, 1, 00, 05, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 05, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M5, new DateTime(2017, 1, 1, 00, 05, 01)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 55, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M5, new DateTime(2017, 1, 1, 00, 59, 59)));
        }

        [TestMethod]
        public void Should_GetTimeScale_M1()
        {
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 00, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M1, new DateTime(2017, 1, 1, 00, 00, 00)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 01, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M1, new DateTime(2017, 1, 1, 00, 00, 01)));
            Assert.AreEqual(new DateTime(2017, 1, 1, 00, 01, 00), TradeLogger.Instance.GetTimeScale(KLineRange.M1, new DateTime(2017, 1, 1, 00, 00, 59)));
        }
    }
}
