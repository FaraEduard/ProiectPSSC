using System;

namespace Examples.Domain.Models
{
    public class CanceledOrder : IOrder
    {
        public Guid OrderId { get; private set; }
        public string CustomerName { get; private set; }
        public DateTime OrderDate { get; private set; }
        public string Status => "Canceled";

        public CanceledOrder(Guid orderId, string customerName, DateTime orderDate)
        {
            OrderId = orderId;
            CustomerName = customerName;
            OrderDate = orderDate;
        }

        public IOrder Cancel()
        {
            throw new InvalidOperationException("Comanda a fost deja anulată.");
        }
    }

    public class ShippedOrder : IOrder
    {
        public Guid OrderId { get; private set; }
        public string CustomerName { get; private set; }
        public DateTime OrderDate { get; private set; }
        public string Status => "Shipped";

        public ShippedOrder(Guid orderId, string customerName, DateTime orderDate)
        {
            OrderId = orderId;
            CustomerName = customerName;
            OrderDate = orderDate;
        }

        public IOrder Cancel()
        {
            throw new InvalidOperationException("Comanda nu poate fi anulată deoarece a fost deja expediată.");
        }
    }

    public class PendingOrder : IOrder
    {
        public Guid OrderId { get; private set; }
        public string CustomerName { get; private set; }
        public DateTime OrderDate { get; private set; }
        public string Status => "Pending";

        public PendingOrder(Guid orderId, string customerName, DateTime orderDate)
        {
            OrderId = orderId;
            CustomerName = customerName;
            OrderDate = orderDate;
        }

        public IOrder Cancel()
        {
            // Transformăm comanda într-o stare anulată
            return new CanceledOrder(OrderId, CustomerName, OrderDate);
        }
    }

}