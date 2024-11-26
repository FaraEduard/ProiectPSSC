using System.Collections.Generic;
using Examples.Domain.Exceptions;
using Examples.Domain.Models;
using Examples.Domain.Operations;

namespace Examples.Domain.Operations
{
    public class CartValidationService
    {
        public Cart.ValidatedCart ValidateCart(Cart.UnvalidatedCart unvalidatedCart, ProductValidationService productValidationService)
        {
            var validatedProducts = productValidationService.ValidateProducts(unvalidatedCart.ProductsList);
            return new Cart.ValidatedCart(validatedProducts);
        }
    }
}