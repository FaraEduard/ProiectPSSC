using System;
using System.Text.RegularExpressions;
using Examples.Domain.Exceptions;
using Examples.Domain.Models;

namespace Examples.Domain.Operations
{
    public class CustomerValidationService
    {
        public ICustomer ValidateCustomer(UnvalidatedCustomer unvalidatedCustomer)
        {
            // Validare pentru CartId
            if (unvalidatedCustomer.CartId == Guid.Empty)
            {
                throw new InvalidCartIdException("CartId nu poate fi gol.");
            }

            // Validare pentru Nume
            if (string.IsNullOrEmpty(unvalidatedCustomer.Nume))
            {
                throw new InvalidCustomerException("Numele nu poate fi gol.");
            }

            // Validare pentru Email cu Regex
            if (string.IsNullOrEmpty(unvalidatedCustomer.Email) || !IsValidEmail(unvalidatedCustomer.Email))
            {
                throw new InvalidEmailException("Emailul nu este valid. Trebuie sa fie de forma ...@gmail.com sau ...@yahoo.com.");
            }

            // Dacă totul e valid, întoarcem un client validat
            return new ValidatedCustomer(unvalidatedCustomer.CartId, unvalidatedCustomer.Nume, unvalidatedCustomer.Email);
        }

        // Funcție pentru validarea emailului
        private bool IsValidEmail(string email)
        {
            // Regex pentru a valida emailurile de tipul ...@gmail.com sau ...@yahoo.com
            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@(gmail\.com|yahoo\.com)$");
            return emailRegex.IsMatch(email);
        }
    }
}
