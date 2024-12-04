using System;

namespace Examples.Domain.Models
{
    public interface IOrder
    {
        Guid OrderId { get; }
        string CustomerName { get; }
        DateTime OrderDate { get; }
        string Status { get; }

        // Metodă pentru anularea comenzii
        IOrder Cancel();
    }
}