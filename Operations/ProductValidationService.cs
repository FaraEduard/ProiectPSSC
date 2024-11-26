using Examples.Domain.Models;
using Examples.Domain.Exceptions;

namespace Examples.Domain.Operations
{
    public class ProductValidationService
    {
        public Product.ValidatedProduct ValidateProduct(Product.UnvalidatedProduct product)
        {
            try
            {
                var id = new ProductId(product.Id);
                var price = new Price(decimal.Parse(product.Price));
                var stock = new Stock(int.Parse(product.Stock));

                return new Product.ValidatedProduct(id, product.Name, price, stock);
            }
            catch (Exception ex)
            {
                throw new InvalidProductException($"Produsul {product.Name} nu este valid: {ex.Message}");
            }
        }

        public List<Product.ValidatedProduct> ValidateProducts(IReadOnlyCollection<Product.UnvalidatedProduct> products)
        {
            var validatedProducts = new List<Product.ValidatedProduct>();
            var validationErrors = new List<string>();

            foreach (var product in products)
            {
                try
                {
                    // Validarea produsului
                    var id = new ProductId(product.Id);
                    var price = new Price(decimal.Parse(product.Price));
                    var stock = new Stock(int.Parse(product.Stock));

                    // Adăugăm produsul validat
                    validatedProducts.Add(new Product.ValidatedProduct(id, product.Name, price, stock));
                }
                catch (Exception ex)
                {
                    // Adăugăm eroare
                    validationErrors.Add($"Produsul {product.Name} nu este valid: {ex.Message}");
                }
            }

            if (validationErrors.Count > 0)
            {
                throw new InvalidProductException(string.Join("\n", validationErrors));
            }

            return validatedProducts;
        }
    }
}

