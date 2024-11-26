using System;

namespace Examples.Domain.Exceptions
{
    internal class InvalidStockException : Exception
    {
        public InvalidStockException()
        {
        }

        public InvalidStockException(string? message) : base(message)
        {
        }

        public InvalidStockException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}