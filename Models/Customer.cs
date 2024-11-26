using System;

namespace Examples.Domain.Models
{
    public record UnvalidatedCustomer : ICustomer
    {
        public Guid CartId { get; set; }
        public string Nume { get; set; }
        public string Email { get; set; }

        public UnvalidatedCustomer(Guid cartId, string nume, string email)
        {
            CartId = cartId;
            Nume = nume;
            Email = email;
        }
    }

    public record ValidatedCustomer : ICustomer
    {
        public Guid CartId { get; set; }
        public string Nume { get; set; }
        public string Email { get; set; }

        public ValidatedCustomer(Guid cartId, string nume, string email)
        {
            CartId = cartId;
            Nume = nume;
            Email = email;
        }
    }
}
