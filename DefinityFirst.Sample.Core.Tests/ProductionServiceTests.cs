using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefinityFirst.Core.Services;
using DefinityFirst.Core.Entities;
using System.Collections.Generic;
using DefinityFirst.Core.Entities.Simple;
using DefinityFirst.Core.Exceptions;
using DefinityFirst.Core.Entities.Complex;

namespace DefinityFirst.Sample.Core.Tests
{
    [TestClass]
    public class ProductionServiceTests
    {
        [TestMethod]
        public void GetProductsAll()
        {
            int totalRows = 0;
            List<ProductEntity> result;

            ProductionService service = new ProductionService();

            result = service.GetProducts(null, null, null, null, null, 1, 1000, out totalRows);

            Assert.IsNotNull(result);
            Assert.IsTrue(totalRows > 0);
            Assert.IsTrue(result.Count <= 1000);
        }

        [TestMethod]
        public void GetProductsFilteredByCategory()
        {
            int totalRows = 0;
            List<ProductEntity> result;

            ProductionService service = new ProductionService();

            //1 = bikes
            result = service.GetProducts(null, 1, null, null, null, 1, 1000, out totalRows);

            Assert.IsNotNull(result);
            Assert.IsTrue(totalRows > 0);
            Assert.IsTrue(result.Count <= 1000);
            Assert.IsTrue(result.TrueForAll(x => x.CategoryId == 1));
        }

        [TestMethod]
        public void AddProductCategory()
        {
            ProductCategoryEntity category = new ProductCategoryEntity
            {
                Name = "Category-" + Guid.NewGuid().ToString("N")
            };

            ProductionService service = new ProductionService();

            service.SaveProductCategory(category);

            Assert.IsTrue(category.Id > 0);

            ProductCategoryEntity dbEntity = service.GetProductCategory(category.Id);

            Assert.AreEqual(dbEntity.Id, category.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateRecordException))]
        public void AddDuplicateProductCategory()
        {
            ProductCategoryEntity category = new ProductCategoryEntity
            {
                Name = "Clothing"
            };

            ProductionService service = new ProductionService();

            service.SaveProductCategory(category);

            Assert.Fail();
        }

        [TestMethod]
        public void GetCategoryBreakdown()
        {
            ProductionService service = new ProductionService();

            CategoryBreakdownEntity catBreakdown = service.GetCategoryBreakdown(1);

            Assert.IsNotNull(catBreakdown);
        }
    }
}
