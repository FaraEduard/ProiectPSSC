using System;

namespace Examples.Domain.Exceptions
{
    internal class InvalidCartOperationException : Exception
    {
        public InvalidCartOperationException()
        {
        }

        public InvalidCartOperationException(string? message) : base(message)
        {
        }

        public InvalidCartOperationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}