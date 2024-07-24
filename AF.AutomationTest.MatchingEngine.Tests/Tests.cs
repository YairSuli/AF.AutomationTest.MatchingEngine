using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AF.AutomationTest.MatchingEngine.Tests
{
    [TestClass]
    public class Tests
    {
        private static MatchingApi _matchingApi;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            _matchingApi = new MatchingApi();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _matchingApi.ClearData();
        }

        // example test
        [TestMethod]
        public void FindMatchTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 100, 10, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 110, 10, date, Side.Sell);
            var record3 = _matchingApi.CreateRecord("Test", 110, 10, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsTrue(isMatched);
        }

        [TestMethod]
        public void SymbolNotMatchTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 100, 10, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("test", 110, 10, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched);
        }

        [TestMethod]
        public void QuantityNotMatchTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 100, 10, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 111, 10, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched);
        }

        [TestMethod]
        public void PriceNotMatchTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 100, 11, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 110, 10, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched);
        }

        [TestMethod]
        public void DateNotMatchTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 100, 11, date.AddMonths(1), Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 110, 10, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched);
        }

        [TestMethod]
        public void SideEqualhTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 100, 11, date.AddMonths(1), Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 110, 10, date, Side.Buy);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched);
        }
    }
}