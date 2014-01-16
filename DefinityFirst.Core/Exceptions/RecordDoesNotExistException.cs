using System;

namespace DefinityFirst.Core.Exceptions
{
    public class RecordDoesNotExistException: Exception
    {
        public RecordDoesNotExistException(string message) : base(message) { }
    }
}
