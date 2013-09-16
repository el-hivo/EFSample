using DefinityFirst.Core.Entities.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinityFirst.Core.Entities
{
    public class SalesOrderEntity
    {
        public int SalesOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string SalesOrderNumber { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Freight { get; set; }
        public decimal Total { get; set; }
        public string CustomerTitle { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public int Status { get; set; }
        public string Comments { get; set; }
        public AddressEntity BillingAddress { get; set; }
        public AddressEntity ShippingAddress { get; set; }
        public List<SalesOrderItemEntity> Items { get; set; }
    }
}
