using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinityFirst.Core.Entities.Simple
{
    public class ProductEntity : ProductInfoEntity
    {
        public int ? SubCategoryId { get; set; }
        public int ? CategoryId { get; set; }
        public int ? ProductModelId { get; set; }
        public string Number { get; set; }
        public string Color { get; set; }
        public string SubCategory { get; set; }
        public string Category { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public string SizeUnitMeasureName { get; set; }
        public string ProductModel { get; set; }
    }
}
