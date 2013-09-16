using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinityFirst.Core.Exceptions
{
    public class RecordDoesNotExistException: Exception
    {
        public RecordDoesNotExistException(string message) : base(message) { }
    }
}
