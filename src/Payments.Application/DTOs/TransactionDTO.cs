namespace Payments.Application.DTOs;

public record TransactionDTO(Guid PaymentId, decimal Amount,
                             string ExternalReference, ShoppingCartDTO ShoppingCart);
