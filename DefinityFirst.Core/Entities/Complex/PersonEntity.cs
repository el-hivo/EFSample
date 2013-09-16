using DefinityFirst.Core.Entities.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinityFirst.Core.Entities.Complex
{
    public class PersonEntity
    {
        public int PersonId { get; set; }
        public string PersonType { get; set; }
        public bool NameStyle { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public int EmailPromotion { get; set; }
        public List<PhoneNumberEntity> Phones { get; set; }
    }
}
