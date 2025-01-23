using Payments.Domain.DomainObjects;

namespace Payments.Domain.Entities;

public class Product : Entity
{
    public Product(string name, string description, decimal price, Guid transactionId)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Guid TransactionId { get; private set; }
    public Transaction? Transaction { get; private set; } = null!;
}
