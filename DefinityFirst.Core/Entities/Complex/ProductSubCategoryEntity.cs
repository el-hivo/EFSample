using DefinityFirst.Core.Entities.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinityFirst.Core.Entities.Complex
{
    public class ProductSubCategoryEntity
    {
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public List<ProductInfoEntity> Products { get; set; }
    }
}
