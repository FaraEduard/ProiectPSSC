namespace Examples.Domain.Models
{
    public interface ICustomer
    {
        Guid CartId { get; set; }
        string Nume { get; set; }
        string Email { get; set; }
    }
}
