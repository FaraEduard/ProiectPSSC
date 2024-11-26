using System;

namespace Examples.Domain.Models
{
    public static class Product
    {
        public interface IProduct { }

        public record UnvalidatedProduct(Guid Id, string Name, string Price, string Stock) : IProduct;

        public record ValidatedProduct(ProductId Id, string Name, Price Price, Stock Stock) : IProduct;

        public record UnavailableProduct(Guid Id, string Name, string Reason) : IProduct;
    }

    public record UnvalidatedProduct(string Name, string Price, string Stock);

    public record ValidatedProduct(ProductId Id, string Name, Price Price, Stock Stock);
}
