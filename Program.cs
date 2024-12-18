using ShoppingCartApp;
using Azure.Messaging.ServiceBus;
using Microsoft.Data.SqlClient;
using Examples.Domain.Models;
using ConsoleApp3.Models_For_Database;

namespace ProiectPSSC
{
    class Program
    {
        public static List<IOrder> Orders = new();

        public static List<Prices_DB> Prices_DB = DatabaseAccess.GetPrices();
        static void Main(string[] args)
        { 
            var plasareComanda = new PlasareComanda();
            plasareComanda.Start();

            var orderCancellation = new OrderCancellation();
            orderCancellation.Start();

            Console.WriteLine("\n=== Lista de comenzi plasate ===");
            foreach (var order in Orders)
            {
                Console.WriteLine($"Comanda ID: {order.OrderId}, Client: {order.CustomerName}, Data: {order.OrderDate}, Status: {order.Status}");
            }
            
        }
    }
    
}

