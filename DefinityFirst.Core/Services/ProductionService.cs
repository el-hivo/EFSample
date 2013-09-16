using DefinityFirst.Core.Entities;
using DefinityFirst.Sample.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DefinityFirst.Core.Entities.Simple;
using DefinityFirst.Core.Exceptions;
using DefinityFirst.Core.Entities.Complex;

namespace DefinityFirst.Core.Services
{
    public class ProductionService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <param name="categoryId"></param>
        /// <param name="color"></param>
        /// <param name="productModel"></param>
        /// <param name="maxCost"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<ProductEntity> GetProducts(int ? subCategoryId, int ? categoryId, string color, string productModel, decimal ? maxCost, int pageNumber, int pageSize, out int totalRows)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                //We will first build the query, exactly as we need it
                //This serves the purpose of having the resulting columns available for filtering
                IQueryable<ProductEntity> query =
                    (from prod in db.Product
                     let subcat = prod.ProductSubcategory
                     let cat = subcat.ProductCategory
                     let mu = prod.UnitMeasure
                     let model = prod.ProductModel
                     orderby prod.Name
                     select new ProductEntity
                     {
                         Category = cat.Name,
                         CategoryId = subcat.ProductCategoryID,
                         Color = prod.Color,
                         ListPrice = prod.ListPrice,
                         Name = prod.Name,
                         Number = prod.ProductNumber,
                         ProductId = prod.ProductID,
                         ProductModel = model.Name,
                         ProductModelId = prod.ProductModelID,
                         SizeUnitMeasureCode = prod.SizeUnitMeasureCode,
                         SizeUnitMeasureName = mu.Name,
                         StandardCost = prod.StandardCost,
                         SubCategory = subcat.Name,
                         SubCategoryId = prod.ProductSubcategoryID
                     });

                #region Apply filters

                if (subCategoryId.HasValue)
                {
                    query = query.Where(p => p.SubCategoryId == subCategoryId);
                }

                if (categoryId.HasValue)
                {
                    query = query.Where(p => p.CategoryId == categoryId);
                }

                if (!string.IsNullOrWhiteSpace(color))
                {
                    query = query.Where(p => p.Color.StartsWith(color));
                }

                if (maxCost.HasValue)
                {
                    query = query.Where(p => p.StandardCost <= maxCost);
                }

                if (!string.IsNullOrWhiteSpace(productModel))
                {
                    query = query.Where(p => p.ProductModel.StartsWith(productModel));
                }

                #endregion

                //Total number of rows
                totalRows = query.Count();

                //Paging
                query = query.Skip(pageNumber - 1).Take(pageSize);
                
                //Materialization
                return query.ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCategoryId"></param>
        /// <returns></returns>
        public CategoryBreakdownEntity GetCategoryBreakdown(int productCategoryId)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                ProductCategory category = db.ProductCategory.Find(productCategoryId);

                //Bring down the subcategories related to the category
                db.Entry(category)
                  .Collection(c => c.ProductSubcategory)
                  .Load();

                //ids of subcategories
                IQueryable<int ?> subCats =
                    category.ProductSubcategory
                            .Select(s => (int ?)s.ProductSubcategoryID)
                            .AsQueryable();

                //List of products that belong to any of the subcategories retrieved
                List<Product> products =
                    (from p in db.Product
                     where subCats.Contains(p.ProductSubcategoryID)
                     select p).ToList();

                return new CategoryBreakdownEntity
                {
                    CategoryId = category.ProductCategoryID,
                    CategoryName = category.Name,
                    SubCategories = 
                        (from sc in category.ProductSubcategory
                         select new ProductSubCategoryEntity
                         {
                             SubCategoryId = sc.ProductSubcategoryID,
                             Name = sc.Name,
                             Products = 
                                (from p in sc.Product
                                 select new ProductInfoEntity
                                 {
                                     ListPrice = p.ListPrice,
                                     Name = p.Name,
                                     ProductId = p.ProductID,
                                     StandardCost = p.StandardCost
                                 }).ToList()
                         }).ToList()
                };
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="prodCat"></param>
        public void SaveProductCategory(ProductCategoryEntity prodCat)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                //Let's first validate the name is unique
                if(db.ProductCategory.Any(p => p.Name == prodCat.Name && p.ProductCategoryID != prodCat.Id))
                {
                    throw new DuplicateRecordException(string.Format("The product category {0}, already exists", prodCat.Name));
                }
                
                ProductCategory dbCategory;

                if (prodCat.IsNew())
                {
                    dbCategory = new ProductCategory();
                    dbCategory.rowguid = Guid.NewGuid();
                    db.ProductCategory.Add(dbCategory);
                }
                else
                {
                    dbCategory = db.ProductCategory.Find(prodCat.Id);
                }

                dbCategory.Name = prodCat.Name;
                dbCategory.ModifiedDate = DateTime.Now;

                db.SaveChanges();

                prodCat.Id = dbCategory.ProductCategoryID;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCategoryId"></param>
        public void DeleteProductCategory(int productCategoryId)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                ProductCategory prodCat = db.ProductCategory.Find(productCategoryId);

                if (prodCat == null)
                {
                    throw new RecordDoesNotExistException(string.Format("Product category {0} does not exist", productCategoryId));
                }

                db.ProductCategory.Remove(prodCat);

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCategoryId"></param>
        /// <returns></returns>
        public ProductCategoryEntity GetProductCategory(int productCategoryId)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                return
                    (from pc in db.ProductCategory
                     where pc.ProductCategoryID == productCategoryId
                     select new ProductCategoryEntity
                     {
                         Id = pc.ProductCategoryID,
                         Name = pc.Name
                     }).Single();
            }
        }
    }
}
