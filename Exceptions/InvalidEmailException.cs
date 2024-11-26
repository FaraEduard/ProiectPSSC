using System;

namespace Examples.Domain.Exceptions
{
    internal class InvalidEmailException : Exception
    {
        public InvalidEmailException()
        {
        }

        public InvalidEmailException(string? message) : base(message)
        {
        }

        public InvalidEmailException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}