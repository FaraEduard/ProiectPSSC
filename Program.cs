using ShoppingCartApp;
using Azure.Messaging.ServiceBus;
using Microsoft.Data.SqlClient;
using Examples.Domain.Models;
using ConsoleApp3.Models_For_Database;
using ShoppingCartApp;

namespace ProiectPSSC
{
    class Program
    {
        //New Orders
        public static List<IOrder> Orders = new();

        //Data uploaded from DB
        public static List<Prices_DB> Prices_DB = DatabaseAccess.GetPrices();
        public static List<Orders_DB> Orders_DB = DatabaseAccess.GetOrders();
        public static List<Products_DB> Products_DB = DatabaseAccess.GetProducts();

        //Aici iti las functiile pe care sa le folosesti din DatabaseAcces.cs (verificate, ready to use):
        // - public static void RemoveProductByNameAndOrderId(string productName, string orderId)
        // - public static void AddProductInOrder(string orderId)
        // - public static void UpdateOrderTotal(string orderId)
        // - public static void CancelOrderIfPriceIsZero(string orderId)

        static void Main(string[] args)
        { 
            var plasareComanda = new PlasareComanda();
            plasareComanda.Start();

            var orderCancellation = new OrderCancellation();
            orderCancellation.Start();


            //Afisarea comenzilor la finalul programului (only for debugging process)
            Console.WriteLine("\n=== Lista de comenzi  ===");
            foreach (var order in Orders)
            {
                Console.WriteLine($"Comanda ID: {order.OrderId}, Client: {order.CustomerName}, Data: {order.OrderDate}, Status: {order.Status}");
            }

            foreach (var order in Orders_DB)
            {
                Console.WriteLine($"- ID: {order.ID} | Status: {order.Status} | Client: {order.NumeC_ustomer} | Data: {order.Data:dd/MM/yyyy}");
            }
        }
    }
    
}

