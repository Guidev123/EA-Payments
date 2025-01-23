namespace Payments.Application.DTOs;

public class ShoppingCartDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid TransactionId { get; set; }
}
