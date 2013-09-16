using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinityFirst.Core.Entities.Listings
{
    public class SalesOrderListEntity
    {
        public int SalesOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string SalesOrderNumber { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Freight { get; set; }
        public decimal Total { get; set; }
        public int NumberOfLineItems { get; set; }
        public string CustomerTitle { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
    }
}
