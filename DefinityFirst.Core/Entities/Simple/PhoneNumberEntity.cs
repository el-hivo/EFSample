using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinityFirst.Core.Entities.Simple
{
    public class PhoneNumberEntity
    {
        public string PhoneNumber { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public string PhoneNumberType { get; set; }
    }
}
