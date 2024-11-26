using System;

namespace Examples.Domain.Exceptions
{
    internal class InvalidProductIdException : Exception
    {
        public InvalidProductIdException()
        {
        }

        public InvalidProductIdException(string? message) : base(message)
        {
        }

        public InvalidProductIdException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}