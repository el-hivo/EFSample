using DefinityFirst.Core.Entities.Simple;
using System.Collections.Generic;

namespace DefinityFirst.Core.Entities.Complex
{
    public class ProductSubCategoryEntity
    {
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public List<ProductInfoEntity> Products { get; set; }
    }
}
