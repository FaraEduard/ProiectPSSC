using Examples.Domain.Models;
using Examples.Domain.Exceptions;
using System;
using System.Collections.Generic;
using Examples.Domain.Operations;
using System.Linq.Expressions;
using ShoppingCartApp;

namespace Examples.Domain.Operations
{
    public class CartService
    {
        private readonly List<Product.ValidatedProduct> _cart = new();
        private ICustomer _customer;

        //int totalPrice

        // void calculatePrice()
        //{
        //    foreach(var element : _cart)
        //    {totalPrice = totalprice + e.Price * cantitate}
        //}

        // Constructorul va primi un customer validat
        public CartService(ICustomer customer)
        {
            _customer = customer;
        }

        public void AddProduct(Product.UnvalidatedProduct unvalidatedProduct)
        {
            var validationService = new ProductValidationService();
            var validatedProduct = validationService.ValidateProduct(unvalidatedProduct);

            _cart.Add(validatedProduct);
        }

        public (IReadOnlyCollection<Product.ValidatedProduct> Products, int Count) GetCart()
        {
            return (_cart.AsReadOnly(), _cart.Count);
        }

        public decimal Calculate_Price()
        {
            decimal total =0;

            foreach(var prod in _cart)
            total = total + (prod.Price.Value * prod.Stock.Value);

            return total;
        }

        public void PlaceOrder(Guid order_id, DateTime data)
        {
            if (_cart.Count == 0)
            {
                throw new InvalidCartOperationException("Nu poti plasa comanda pentru un cos gol.");
            }

            if (_customer is not ValidatedCustomer)
            {
                throw new InvalidCustomerException("Clientul nu este validat. Nu se poate plasa comanda.");
            }

            // Se insereaza in baza de date fiecare item
            foreach(var produs in _cart)
            {
                var id_produs = produs.Id;
                var nume_produs = produs.Name;
                var pret_produs = produs.Price.Value;
                var cantitate_produs = produs.Stock.Value;

                DatabaseAccess.InsertProduct(id_produs.Value,nume_produs,pret_produs,cantitate_produs,order_id.ToString());
            }

            //Aici se insereaza in DB Comanda
            var nume_customer = _customer.Nume;
            string status = "Pending";
            var pret = Calculate_Price();
            DatabaseAccess.InsertOrder(order_id.ToString(), nume_customer, data, status, pret);

            _cart.Clear();
        }

        // Funcție pentru a verifica dacă coșul este valid
        public void VerifyCart()
        {
            if (_customer is ValidatedCustomer)
            {
                Console.WriteLine("Coșul a fost verificat cu succes.");
            }
            else
            {
                Console.WriteLine("Coșul nu poate fi verificat, deoarece clientul nu este validat.");
                throw new InvalidCartOperationException("Atentie, clientul nu este validat!");
            }
        }
        public void UpdateProductQuantity(IOrder order, ProductId productId, int newQuantity)
        {
            // Găsim produsul din coș
            var cartItem = _cart.FirstOrDefault(item => item.Id == productId);

            if (cartItem == null)
            {
                throw new Exception("Produsul nu a fost găsit în coș.");
            }

            if (newQuantity <= 0)
            {
                // Dacă noua cantitate este 0 sau mai mică, eliminăm produsul din coș
                _cart.Remove(cartItem);
                Console.WriteLine("Produsul a fost eliminat din coș.");
            }
            else
            {
                // Actualizează cantitatea produsului
                var updatedCartItem = cartItem with { Stock = new Stock(newQuantity) }; // Asumăm că Stock poate fi setat cu un string

                // Găsește indexul produsului vechi în listă și îl înlocuiește cu produsul actualizat
                int index = _cart.IndexOf(cartItem);
                if (index != -1)
                {
                    _cart[index] = updatedCartItem;
                    Console.WriteLine("Cantitatea produsului a fost actualizată.");
                }
                else
                {
                    Console.WriteLine("Eroare la actualizarea produsului.");
                }
            }
        }
        /*private void DisplayCartItems(IOrder order)
        {
            Console.WriteLine("\nProdusele din coș:");
            foreach (var product in order.Cart)
            {
                Console.WriteLine($"- ID: {product.ProductId} | Nume: {product.Name} | Cantitate: {product.Stock.Quantity}");
            }
        }
        */

    }
}
