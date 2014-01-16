namespace DefinityFirst.Core.Entities.Simple
{
    public class SalesOrderItemEntity
    {
        public int SalesOrderItemId { get; set; }
        public string CarrierTrackingNumber { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal Total { get; set; }
    }
}
