using System;

namespace Examples.Domain.Models
{
    public static class Product
    {
        public interface IProduct { }

        public record UnvalidatedProduct(string Name, string Price, string Stock) : IProduct
        {
            public int Id;
        }

        public record ValidatedProduct(ProductId Id, string Name, Price Price, Stock Stock) : IProduct;

        public record UnavailableProduct( string Name, string Reason) : IProduct
        {
            int Id;
        }
    }

    public record UnvalidatedProduct(string Name, string Price, string Stock);

    public record ValidatedProduct(ProductId Id, string Name, Price Price, Stock Stock);
}
