using Payments.Domain.DomainObjects;

namespace Payments.Domain.Entities;

public class Transaction : Entity
{
    public Transaction(Guid paymentId, decimal amount)
    {
        PaymentId = paymentId;
        Amount = amount;
    }

    public Guid PaymentId { get; private set; }
    public decimal Amount { get; private set; }
    public string? ExternalReference { get; private set; } = string.Empty;
    public List<Product> Products { get; private set; } = null!;
    public Payment? Payment { get; private set; } = null!;

    public void SetProducts(List<Product> products) => Products = products;
    protected Transaction() { }
}
