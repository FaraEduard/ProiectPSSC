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
                string order_id_string = orderId.ToString();

                var order_db = Program.Orders_DB.Find(o => o.ID == order_id_string);

                //Cautare in comenzile incarcarte in baza de date
                if(order_db != null)
                {
                    try
                    {

                        //DatabaseAccess.RemoveOrder(order_id_string); - In caz ca se doreste stergerea comenzii.
                        //DatabaseAccess.RemoveProductsByOrderId(order_id_string); - In caz ca se doreste stergerea itemelor dupa order id.

                        //Modificarea statusului comenzii (itemele raman in DB)
                        DatabaseAccess.UpdateOrderStatusToCanceled(order_id_string);

                        Program.Orders_DB.Clear();
                        Program.Orders_DB = DatabaseAccess.GetOrders();

                        Console.WriteLine($"Comanda cu ID-ul {order_id_string} a fost anulată cu succes.");
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
            foreach (var order in Program.Orders_DB)
            {
                Console.WriteLine($"- ID: {order.ID} | Status: {order.Status} | Client: {order.NumeC_ustomer} | Data: {order.Data:dd/MM/yyyy}");
            }
        }
    }
}
