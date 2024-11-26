using System;
using System.Collections.Generic;

namespace Examples.Domain.Models
{
    public static class Cart
    {
        public interface ICart { }

        public record UnvalidatedCart(IReadOnlyCollection<Product.UnvalidatedProduct> ProductsList) : ICart;

        public record ValidatedCart(IReadOnlyCollection<Product.ValidatedProduct> ProductsList) : ICart;

        public record PurchasedCart(IReadOnlyCollection<Product.ValidatedProduct> ProductsList, DateTime PurchasedDate) : ICart;
    }
}