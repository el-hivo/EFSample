using System.Collections.Generic;

namespace DefinityFirst.Core.Entities.Complex
{
    public class CategoryBreakdownEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductSubCategoryEntity> SubCategories { get; set; }
    }
}
