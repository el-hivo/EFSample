using System;

namespace DefinityFirst.Core.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessRuleException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public BusinessRuleException(string message) : base(message) { }
    }
}
