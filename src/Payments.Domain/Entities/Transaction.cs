using Payments.Domain.DomainObjects;

namespace Payments.Domain.Entities;

public class Transaction : Entity
{
    public Transaction(Guid paymentId, decimal amount, ShoppingCart shoppingCart)
    {
        PaymentId = paymentId;
        Amount = amount;
        ShoppingCart = shoppingCart;
    }

    public Guid PaymentId { get; private set; }
    public decimal Amount { get; private set; }
    public string? ExternalReference { get; private set; } = string.Empty;
    public ShoppingCart ShoppingCart { get; private set; } = null!;
    public Payment? Payment { get; private set; } = null!;
    protected Transaction() { }
}
