/*using System;
using System.Collections.Generic;
using Examples.Domain.Models;
using ProiectPSSC;
using static Examples.Domain.Models.Cart;

namespace ShoppingCartApp
{
    public class ModificareComanda
    {
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("=== Modificare Comanda ===");

            // Afișăm comenzile disponibile
            DisplayOrders();

            // Selectăm comanda de anulat
            Console.Write("Introduceti ID-ul comenzii pe care doriti sa o modificati: ");
            string orderIdInput = Console.ReadLine() ?? "N/A";

            if (Guid.TryParse(orderIdInput, out Guid orderId))
            {
                // Găsim comanda după ID în lista globală din Program
                var order = Program.Orders.Find(o => o.OrderId == orderId);
                if (order != null)
                {
                    Console.WriteLine($"Comanda cu ID-ul {order.OrderId} a fost găsită.");

                    // Afișăm produsele din coșul comenzii
                    //DisplayCartItems(order);

                    // Selectăm produsul care urmează să fie modificat
                    Console.Write("Introduceti ID-ul produsului pe care doriti sa-l modificati: ");
                    string productIdInput = Console.ReadLine() ?? "N/A";

                    if (Guid.TryParse(productIdInput, out Guid productId))
                    {
                   
                        if (product != null)
                        {
                            Console.WriteLine($"Produsul {product.Name} a fost găsit.");

                            // Introducem noua cantitate
                            Console.Write("Introduceti noua cantitate: ");
                            if (int.TryParse(Console.ReadLine(), out int newQuantity))
                            {
                                try
                                {
                                    // Apelăm metoda pentru actualizarea cantității
                                   // UpdateProductQuantity(productId, newQuantity); // Apelul metodei pentru actualizare
                                    Console.WriteLine("Cantitatea produsului a fost actualizată cu succes.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Eroare: {ex.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Cantitatea introdusă este invalidă.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Produsul nu a fost găsit în coș.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID-ul produsului este invalid.");
                    }
                }
                else
                {
                    Console.WriteLine("Comanda nu a fost găsită.");
                }
            }
            else
            {
                Console.WriteLine("ID-ul comenzii introdus este invalid.");
            }

            Console.WriteLine("\nApasa orice tasta pentru a reveni la meniu...");
            Console.ReadKey();
        }

        private void DisplayOrders()
        {
            Console.WriteLine("Comenzile disponibile:");
            foreach (var order in Program.Orders)
            {
                Console.WriteLine($"- ID: {order.OrderId} | Status: {order.Status} | Client: {order.CustomerName} | Data: {order.OrderDate:dd/MM/yyyy}");
            }
        }
    }
    
}
*/