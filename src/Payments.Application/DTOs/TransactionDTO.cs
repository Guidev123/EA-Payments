namespace Payments.Application.DTOs;

public class TransactionDTO
{
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string? ExternalReference { get; set; } = string.Empty;
    public ShoppingCartDTO ShoppingCart { get; set; } = null!;
}
