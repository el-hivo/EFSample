namespace DefinityFirst.Core.Entities.Listings
{
    public class ProductSalesSummaryItem
    {
        public int ProductId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string SubCategory { get; set; }
        public string Category { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public decimal TotalSalesRevenue { get; set; }
        public decimal TotalSoldItems { get; set; }
        public decimal AverageSalesPrice { get; set; }
    }
}
