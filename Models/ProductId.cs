using System;
using Examples.Domain.Exceptions;

namespace Examples.Domain.Models
{
    public record ProductId
    {
        public int Value { get; }

        public ProductId(int value)
        {
            if (value == 0)
            {
                throw new InvalidProductIdException("Product ID cannot be empty.");
            }
            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}