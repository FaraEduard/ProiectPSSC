using System;

namespace Examples.Domain.Exceptions
{
    internal class InvalidCartIdException : Exception
    {
        public InvalidCartIdException()
        {
        }

        public InvalidCartIdException(string? message) : base(message)
        {
        }

        public InvalidCartIdException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
