using System;

namespace DefinityFirst.Core.Exceptions
{
    public class DuplicateRecordException : Exception
    {
        public DuplicateRecordException(string message) : base(message) { }
    }
}
