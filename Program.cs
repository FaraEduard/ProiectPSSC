using ShoppingCartApp;
using Azure.Messaging.ServiceBus;
using Microsoft.Data.SqlClient;
using Examples.Domain.Models;

namespace ProiectPSSC
{
    class Program
    {
        public static List<IOrder> Orders = new List<IOrder>();
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

