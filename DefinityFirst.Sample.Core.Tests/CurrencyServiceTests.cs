using System;
using DefinityFirst.Core.Entities.Simple;
using DefinityFirst.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefinityFirst.Sample.Core.Tests
{
    [TestClass]
    public class CurrencyServiceTests
    {
        [TestMethod]
        public void GetDollarsToPesosRatesTest()
        {
            const string fromCode ="USD";
            const string toCode = "MXN";
            DateTime fromDate = new DateTime(2008, 6, 1);
            DateTime toDate = new DateTime(2008, 6, 30);

            CurrencyService service = new CurrencyService();

            CurrencyEntity currency = service.GetCurrencyWithRates(fromCode, toCode, fromDate, toDate);

            Assert.IsNotNull(currency);
            Assert.AreEqual(currency.Code, fromCode);
            Assert.IsNotNull(currency.Rates);
            Assert.AreEqual(currency.Rates.Count, 30);

            currency.Rates.ForEach(r =>
            {
                Assert.AreEqual(r.FromCurrencyCode, fromCode);
                Assert.AreEqual(r.ToCurrencyCode, toCode);
                Assert.IsTrue(r.RateDate <= toDate);
                Assert.IsTrue(r.RateDate >= fromDate);
            });
        }

        [TestMethod]
        public void GetDollarsToPesosRatesEagerLoadingTest()
        {
            const string fromCode = "USD";
            const string toCode = "MXN";
            DateTime fromDate = new DateTime(2008, 6, 1);
            DateTime toDate = new DateTime(2008, 6, 30);

            CurrencyService service = new CurrencyService();

            CurrencyEntity currency = service.GetCurrencyWithRatesEagerLoading(fromCode, toCode, fromDate, toDate);

            Assert.IsNotNull(currency);
            Assert.AreEqual(currency.Code, fromCode);
            Assert.IsNotNull(currency.Rates);
            Assert.AreEqual(currency.Rates.Count, 30);

            currency.Rates.ForEach(r =>
            {
                Assert.AreEqual(r.FromCurrencyCode, fromCode);
                Assert.AreEqual(r.ToCurrencyCode, toCode);
                Assert.IsTrue(r.RateDate <= toDate);
                Assert.IsTrue(r.RateDate >= fromDate);
            });
        }
    }
}
