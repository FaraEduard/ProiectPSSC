using System;
using System.Collections.Generic;
using Examples.Domain.Models;
using ProiectPSSC;

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

            // Selectăm comanda de modificat
            Console.Write("Introduceti ID-ul comenzii pe care doriti sa o modificati: ");
            string orderIdInput = Console.ReadLine() ?? "N/A";

            if (Guid.TryParse(orderIdInput, out Guid orderId))
            {
                string order_id_string = orderId.ToString();

                var order1_db = Program.Orders_DB.Find(o => o.ID == order_id_string);

                //Cautare in comenzile din baza de date
                if (order1_db != null)
                {

                    try
                    {
                        Console.WriteLine("Produse din comanda selectată:");
                        foreach (var product in Program.Products_DB)
                        {
                            Console.WriteLine($"- {product.ID} | {product.Nume} | Pret: {product.Pret} | Cantitate: {product.Cantitate}");
                        }

                        // Pasul 4: Modificarea produselor din comandă
                        Console.WriteLine("\n1. Adaugă produs");
                        Console.WriteLine("2. Elimină produs");
                        Console.Write("Alege o opțiune: ");
                        string? input = Console.ReadLine();

                        switch (input)
                        {
                            case "1":
                                DatabaseAccess.AddProductInOrder(order_id_string);
                                break;

                            case "2":
                                Console.WriteLine("Dati numele produsului: ");
                                string ordername = Console.ReadLine() ?? "N/A";
                                DatabaseAccess.RemoveProductByNameAndOrderId(ordername, order_id_string);
                                break;

                            default:
                                Console.WriteLine("Opțiune invalidă.");
                                return;
                        }

                        // Pasul 5: Actualizarea comenzii în baza de date
                        DatabaseAccess.UpdateOrderTotal(order_id_string);
                        DatabaseAccess.CancelOrderIfPriceIsZero(order_id_string);

                        // Actualizarea locală a datelor
                        Program.Orders_DB.Clear();
                        Program.Orders_DB = DatabaseAccess.GetOrders();

                        Console.WriteLine("\nComanda a fost modificată cu succes.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Eroare la modificarea comenzii: {ex.Message}");
                    }
                }

            }
            
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