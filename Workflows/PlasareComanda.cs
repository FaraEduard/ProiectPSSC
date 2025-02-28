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
                Console.WriteLine("3. Verificare cos");
                Console.WriteLine("4. Plasare comanda");
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
                        cartService.VerifyCart();  // Verifică coșul
                        break;

                    case "4":
                        var pendingOrder = PlaceOrder(cartService, validatedCustomer.Nume);
                        if (pendingOrder != null)
                        {
                            Program.Orders.Add(pendingOrder);
                            Console.WriteLine($"Comanda a fost adăugată cu succes. ID: {pendingOrder.OrderId}");
                        }
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
            Console.Clear();
            Console.WriteLine("=== Adauga produs in cos ===");

            Console.Write("Numele produsului: ");
            string name = Console.ReadLine() ?? "N/A";

            // Cautam Id si pret dupa numele produsului.
            try
            {
                int i=0;
                foreach(var element in Program.Prices_DB)
                {
                    if(element.Nume == name)
                    {
                        string priceInput = element.Pret.ToString();
                        int Id = element.Id;

                        Console.Write("Cantitatea produsului dorit: ");
                        string stockInput = Console.ReadLine() ?? "N/A";

                        var unvalidatedProduct = new Product.UnvalidatedProduct(name, priceInput, stockInput);
                        unvalidatedProduct.Id = Id;

                        cartService.AddProduct(unvalidatedProduct);

                        Console.WriteLine($"- {unvalidatedProduct.Id} | {unvalidatedProduct.Name} | Pret: {unvalidatedProduct.Price:0.##} | Cantitate: {unvalidatedProduct.Stock}");
                        Console.WriteLine("Produsul a fost adaugat in cos. ");
                        i=10;
                        break;
                    }
                }
                if(i != 10)
                throw new Exception("Produsul nu exista.");
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
                    Console.WriteLine($"- {product.Id} | {product.Name} | Pret: {product.Price.Value:0.##} | Stoc: {product.Stock.Value}");
                }
            }
        }

        static PendingOrder? PlaceOrder(CartService cartService, string customerName)
        {
            Console.Clear();
            Console.WriteLine("=== Plasare comanda ===");

            try
            {
                // Creăm un obiect PendingOrder
                var orderId = Guid.NewGuid();
                var orderDate = DateTime.Now;

                //In PlaceOrder are loc si inserarea in DB a comenzii cat si a produselor
                cartService.PlaceOrder(orderId, orderDate);

                //Updatarea Comenzilor din DB in program
                Program.Orders_DB.Clear();
                Program.Orders_DB = DatabaseAccess.GetOrders();

                //Updatarea Itemelor din DB in program
                Program.Products_DB.Clear();
                Program.Products_DB = DatabaseAccess.GetProducts();

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
