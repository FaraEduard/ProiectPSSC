using System;

namespace Examples.Domain.Exceptions
{
    internal class InvalidCustomerException : Exception
    {
        public InvalidCustomerException()
        {
        }

        public InvalidCustomerException(string? message) : base(message)
        {
        }

        public InvalidCustomerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}