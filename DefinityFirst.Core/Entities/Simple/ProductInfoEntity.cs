using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinityFirst.Core.Entities.Simple
{
    public class ProductInfoEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
    }
}
