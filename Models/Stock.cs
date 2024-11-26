using Examples.Domain.Exceptions;

public record Stock
    {
        public int Value { get; }

        public Stock(int value)
        {
            if (value < 0)
            {
                throw new InvalidStockException($"{value} is an invalid stock value.");
            }
            Value = value;
        }

        public override string ToString() => $"{Value} units";
    }