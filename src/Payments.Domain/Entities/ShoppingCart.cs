using Payments.Domain.DomainObjects;

namespace Payments.Domain.Entities;

public class ShoppingCart : Entity
{
    public ShoppingCart(string name, string description, decimal price, Guid transactionId)
    {
        Name = name;
        Description = description;
        Price = price;
        TransactionId = transactionId;
    }
    public Guid TransactionId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public Transaction? Transaction { get; private set; } = null!;
    protected ShoppingCart() { }
}
