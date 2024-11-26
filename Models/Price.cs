using Examples.Domain.Exceptions;

namespace Examples.Domain.Models
{
    public record Price
    {
        public decimal Value { get; }

        public Price(decimal value)
        {
            if (value <= 0)
            {
                throw new InvalidPriceException($"{value} is an invalid price.");
            }
            Value = value;
        }

        public override string ToString() => $"{Value:0.##}";
    }
} 