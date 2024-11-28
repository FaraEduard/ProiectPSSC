using System;

namespace Examples.Domain.Exceptions
{
    internal class OutOfStockException : Exception
    {
        public OutOfStockException()
        {
        }

        public OutOfStockException(string? message) : base(message)
        {
        }

        public OutOfStockException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}