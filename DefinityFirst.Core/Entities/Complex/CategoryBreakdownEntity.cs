using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinityFirst.Core.Entities.Complex
{
    public class CategoryBreakdownEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductSubCategoryEntity> SubCategories { get; set; }
    }
}
