using System;
using System.Collections.Generic;
using Examples.Domain.Models;
using ProiectPSSC;

namespace ShoppingCartApp
{
    public class OrderCancellation
    {
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("=== Anulare Comanda ===");

            // Afișăm comenzile disponibile
            DisplayOrders();

            // Selectăm comanda de anulat
            Console.Write("Introduceti ID-ul comenzii pe care doriti sa o anulati: ");
            string orderIdInput = Console.ReadLine() ?? "N/A";

            if (Guid.TryParse(orderIdInput, out Guid orderId))
            {
                // Găsim comanda după ID în lista globală din Program
                var order = Program.Orders.Find(o => o.OrderId == orderId);
                if (order != null)
                {
                    try
                    {
                        // Anulăm comanda și actualizăm starea în lista globală
                        var canceledOrder = order.Cancel();
                        Program.Orders.Remove(order); // Eliminăm comanda curentă                  
                        Program.Orders.Add(canceledOrder); // Adăugăm comanda anulată

                        Console.WriteLine($"Comanda cu ID-ul {order.OrderId} a fost anulată cu succes.");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"Eroare: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Comanda nu a fost găsită.");
                }
            }
            else
            {
                Console.WriteLine("ID-ul introdus este invalid.");
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
