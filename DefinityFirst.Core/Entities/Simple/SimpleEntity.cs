using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinityFirst.Core.Entities.Simple
{
    public class SimpleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public bool IsNew() {
            return Id <= 0;
        }
    }
}
