using System;
using Examples.Domain.Models;
using Examples.Domain.Operations;
using Examples.Domain.Exceptions;
using ProiectPSSC;

namespace ShoppingCartApp
{
    public class PlasareComanda
    {
        public void Start()
        {
            var customerValidationService = new CustomerValidationService();

            // Citim datele clientului de la tastatură
            var unvalidatedCustomer = ReadCustomerData();

            // Validăm customerul
            ICustomer validatedCustomer = customerValidationService.ValidateCustomer(unvalidatedCustomer);

            // Creăm CartService cu un customer validat
            var cartService = new CartService(validatedCustomer);

            // Aici poți adăuga produse în coș
            string? input;

            do
            {
                Console.Clear();
                Console.WriteLine("===== Meniu =====");
                Console.WriteLine("1. Adauga produs in cos");
                Console.WriteLine("2. Vizualizare cos");
                Console.WriteLine("3. Plasare comanda");
                Console.WriteLine("4. Verificare cos");
                Console.WriteLine("5. Iesire");
                Console.Write("Alege o optiune: ");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddProductToCart(cartService);
                        break;

                    case "2":
                        ViewCart(cartService);
                        break;

                    case "3":
                        var pendingOrder = PlaceOrder(cartService, validatedCustomer.Nume);
                        if (pendingOrder != null)
                        {
                            Program.Orders.Add(pendingOrder);
                            Console.WriteLine($"Comanda a fost adăugată cu succes. ID: {pendingOrder.OrderId}");
                        }
                        break;
                    case "4":
                        cartService.VerifyCart();  // Verifică coșul
                        break;

                    case "5":
                        Console.WriteLine("Iesire... La revedere!");
                        break;

                    default:
                        Console.WriteLine("Optiune invalida. Incearca din nou.");
                        break;
                }

                if (input != "5")
                {
                    Console.WriteLine("\nApasa orice tasta pentru a continua...");
                    Console.ReadKey();
                }

            } while (input != "5");

            //Securizare plata

            //Generare factura

            //Initializare Livrare

            //Confirmare primire comanda 
        }

        private UnvalidatedCustomer ReadCustomerData()
        {
            Console.Clear();
            Console.WriteLine("=== Inregistrare Client ===");

            // Citirea numelui
                Console.Write("Introduceti numele complet: ");
                string name = Convert.ToString(Console.ReadLine()) ?? "N/A";
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Numele nu poate fi gol. Te rog sa il introduci din nou.");
                }

            // Citirea adresei de email
            string email;
                Console.Write("Introduceti adresa de email (de forma ...@gmail.com sau ...@yahoo.com): ");
                email = Convert.ToString(Console.ReadLine()) ?? "N/A";
                if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
                {
                    throw new InvalidEmailException("Email invalid. Te rog sa introduci un email valid.");
                }

            // Crearea unui UnvalidatedCustomer cu datele introduse
            return new UnvalidatedCustomer(Guid.NewGuid(), name, email);
        }

        // Funcție pentru validarea emailului
        private bool IsValidEmail(string email)
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9._%+-]+@(gmail\.com|yahoo\.com)$");
            return emailRegex.IsMatch(email);
        }


        static void AddProductToCart(CartService cartService)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Adauga produs in cos ===");

                Console.Write("Numele produsului: ");
                string name = Console.ReadLine() ?? "N/A";

                Console.Write("Pretul produsului: ");
                string priceInput = Console.ReadLine() ?? "N/A";
                string result = priceInput.Replace('.', ',');

                Console.Write("Stocul produsului: ");
                string stockInput = Console.ReadLine() ?? "N/A";

                var unvalidatedProduct = new Product.UnvalidatedProduct(Guid.NewGuid(), name, result, stockInput);
                cartService.AddProduct(unvalidatedProduct);

                Console.WriteLine("Produsul a fost adaugat in cos.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la adaugarea produsului: {ex.Message}");
            }
        }

        static void ViewCart(CartService cartService)
        {
            Console.Clear();
            Console.WriteLine("=== Vizualizare cos ===");

            var cart = cartService.GetCart();
            if (cart.Count == 0)
            {
                Console.WriteLine("Cosul este gol.");
            }
            else
            {
                Console.WriteLine("Produsele din cos:");
                foreach (var product in cart.Products)
                {
                    Console.WriteLine($"- {product.Name} | Pret: {product.Price.Value:0.##} | Stoc: {product.Stock.Value}");
                }
            }
        }

                static PendingOrder? PlaceOrder(CartService cartService, string customerName)
        {
            Console.Clear();
            Console.WriteLine("=== Plasare comanda ===");

            try
            {
                cartService.PlaceOrder();

                // Creăm un obiect PendingOrder
                var orderId = Guid.NewGuid();
                var orderDate = DateTime.Now;

                Console.WriteLine("Comanda a fost plasata cu succes. Cosul a fost golit.");
                return new PendingOrder(orderId, customerName, orderDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la plasarea comenzii: {ex.Message}");
                return null; // Returnăm null dacă procesarea eșuează
            }
        }

    }
}
