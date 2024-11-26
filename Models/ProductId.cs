using System;
using Examples.Domain.Exceptions;

namespace Examples.Domain.Models
{
    public record ProductId
    {
        public Guid Value { get; }

        public ProductId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidProductIdException("Product ID cannot be empty.");
            }
            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}