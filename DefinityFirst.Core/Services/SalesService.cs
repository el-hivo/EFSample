using DefinityFirst.Core.Entities;
using DefinityFirst.Core.Entities.Listings;
using DefinityFirst.Core.Entities.Simple;
using DefinityFirst.Sample.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinityFirst.Core.Services
{
    public class SalesService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<SalesOrderListEntity> GetSalesOrderList(int ? customerId, DateTime ? startDate, DateTime ? endDate, int pageNumber, int pageSize, out int totalRows)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                //The structure that I want to return does not have the customerId as part of its columns
                //So i need to filter based on the original object
                IQueryable<SalesOrderHeader> query = db.SalesOrderHeader.AsQueryable();

                #region Apply filters

                if (customerId.HasValue)
                {
                    query = query.Where(so => so.CustomerID == customerId);
                }

                if (startDate.HasValue)
                {
                    query = query.Where(so => so.OrderDate >= startDate.Value.Date);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(so => so.OrderDate <= endDate.Value.Date);
                }

                #endregion

                //Executes a query to get the total count of rows
                totalRows = query.Count();

                //Executes the query with the results
                return
                    (from so in query
                     let customer = so.Customer
                     let person = customer.Person
                     orderby so.SalesOrderNumber  //For paging to work an orderby clause is mandatory
                     select new SalesOrderListEntity //Projection
                     {
                         CustomerFirstName = person.FirstName,
                         CustomerLastName = person.LastName,
                         CustomerTitle = person.Title,
                         Freight = so.Freight,
                         NumberOfLineItems = so.SalesOrderDetail.Count(),
                         OrderDate = so.OrderDate,
                         SalesOrderId = so.SalesOrderID,
                         SalesOrderNumber = so.SalesOrderNumber,
                         SubTotal = so.SubTotal,
                         Tax = so.TaxAmt,
                         Total = so.TotalDue
                     })
                     .Skip(pageNumber - 1) //Paging
                     .Take(pageSize)
                     .ToList(); //Materialization
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salesOrderId"></param>
        /// <returns></returns>
        public SalesOrderEntity GetSalesOrder(int salesOrderId)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                //One query with a couple of joins
                SalesOrderEntity salesOrder =
                    (from so in db.SalesOrderHeader
                     let billingAddress = so.Address
                     let shippingAddress = so.Address1
                     let customer = so.Customer.Person
                     where so.SalesOrderID == salesOrderId
                     select new SalesOrderEntity
                     {
                         BillingAddress = new AddressEntity
                         {
                             AddressLine1 = billingAddress.AddressLine1,
                             AddressLine2 = billingAddress.AddressLine2,
                             City = billingAddress.City,
                             Country = billingAddress.StateProvince.CountryRegion.Name, //try not to abuse this
                             PostalCode = billingAddress.PostalCode,
                             State = billingAddress.StateProvince.Name
                         },
                         Comments = so.Comment,
                         CustomerFirstName = customer.FirstName,
                         CustomerLastName = customer.LastName,
                         CustomerTitle = customer.Title,
                         DueDate = so.DueDate,
                         Freight = so.Freight,
                         OrderDate = so.OrderDate,
                         SalesOrderId = so.SalesOrderID,
                         SalesOrderNumber = so.SalesOrderNumber,
                         ShipDate = so.ShipDate,
                         ShippingAddress = new AddressEntity
                         {
                             AddressLine1 = shippingAddress.AddressLine1,
                             AddressLine2 = shippingAddress.AddressLine2,
                             City = shippingAddress.City,
                             Country = shippingAddress.StateProvince.CountryRegion.Name,
                             PostalCode = shippingAddress.PostalCode,
                             State = shippingAddress.StateProvince.Name
                         },
                         Status = so.Status,
                         SubTotal = so.SubTotal,
                         Tax = so.TaxAmt,
                         Total = so.TotalDue
                     }).Single();

                //Second query with another couple of joins
                salesOrder.Items =
                    (from li in db.SalesOrderDetail
                     where li.SalesOrderID == salesOrderId
                     select new SalesOrderItemEntity
                     {
                         CarrierTrackingNumber = li.CarrierTrackingNumber,
                         ProductId = li.ProductID,
                         ProductName = li.SpecialOfferProduct.Product.Name,
                         Quantity = li.OrderQty,
                         SalesOrderItemId = li.SalesOrderDetailID,
                         Total = li.LineTotal,
                         UnitPrice = li.UnitPrice,
                         UnitPriceDiscount = li.UnitPriceDiscount
                     }).ToList();

                return salesOrder;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<ProductSalesSummaryItem> GetSalesSummaryByProduct(string category, DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize, out int totalRows)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                //We're going to use an anonymous type here
                var query =
                    (from soli in db.SalesOrderDetail
                     let so = soli.SalesOrderHeader
                     let product = soli.SpecialOfferProduct.Product
                     select new
                     {
                         ProductId = soli.ProductID,
                         OrderDate = so.OrderDate,
                         TotalSale = soli.LineTotal,
                         ProductQuantity = soli.OrderQty,
                         UnitPrice = soli.UnitPrice
                     });

                #region Filters

                if (startDate.HasValue)
                {
                    query = query.Where(p => p.OrderDate >= startDate.Value.Date);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(p => p.OrderDate <= endDate.Value.Date);
                }

                #endregion

                var grouped =
                    (from sales in query
                     group sales by sales.ProductId into grp
                     select new
                     {
                         ProductId = grp.Key,
                         TotalSales = grp.Sum(p => p.TotalSale),
                         TotalQuantity = grp.Sum(p => p.ProductQuantity),
                         AverageSalePrice = grp.Average(p => p.UnitPrice)
                     });

                IQueryable<ProductSalesSummaryItem> result =
                    (from product in db.Product
                     join sales in grouped on product.ProductID equals sales.ProductId
                     let subCat = product.ProductSubcategory
                     let cat = subCat.ProductCategory
                     orderby product.Name
                     select new ProductSalesSummaryItem
                     {
                         AverageSalesPrice = sales.AverageSalePrice,
                         Category = cat.Name,
                         CategoryId = subCat.ProductCategoryID,
                         ListPrice = product.ListPrice,
                         Name = product.Name,
                         Number = product.ProductNumber,
                         ProductId = product.ProductID,
                         StandardCost = product.StandardCost,
                         SubCategory = subCat.Name,
                         SubCategoryId = product.ProductSubcategoryID,
                         TotalSalesRevenue = sales.TotalSales,
                         TotalSoldItems = sales.TotalQuantity
                     });

                #region Final Filtering

                if (!string.IsNullOrWhiteSpace(category))
                {
                    result = result.Where(p => p.Category.StartsWith(category));
                }

                #endregion

                totalRows = result.Count();

                return result.Skip(pageNumber - 1).Take(pageSize).ToList();
            }
        }
    }
}
