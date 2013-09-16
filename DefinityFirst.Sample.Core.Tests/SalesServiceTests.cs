using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefinityFirst.Core.Services;
using DefinityFirst.Core.Entities;
using System.Collections.Generic;
using DefinityFirst.Core.Entities.Listings;

namespace DefinityFirst.Sample.Core.Tests
{
    [TestClass]
    public class SalesServiceTests
    {
        [TestMethod]
        public void GetSalesOrderHeadersAll()
        {
            int totalRows = 0;
            List<SalesOrderListEntity> result;

            SalesService service = new SalesService();

            result = service.GetSalesOrderList(null, null, null, 1, 100, out totalRows);

            Assert.IsNotNull(result);
            Assert.IsTrue(totalRows > 0);
            Assert.IsTrue(result.Count <= 100);
        }

        [TestMethod]
        public void GetSalesOrder()
        {
            SalesService service = new SalesService();

            SalesOrderEntity salesOrder = service.GetSalesOrder(43662);

            Assert.IsNotNull(salesOrder);
            Assert.AreEqual(43662, salesOrder.SalesOrderId);
            Assert.IsNotNull(salesOrder.Items);
        }

        [TestMethod]
        public void GetSalesSummaryByProductAll()
        {
            int totalRows = 0;
            SalesService service = new SalesService();

            List<ProductSalesSummaryItem> list =
                service.GetSalesSummaryByProduct(null, null, null, 1, 10, out totalRows);

            Assert.IsNotNull(list);
            Assert.IsTrue(totalRows > 0);
            Assert.IsTrue(list.Count <= 10);
        }
    }
}
