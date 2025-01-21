namespace Payments.Application.DTOs;

public record StripeTransactionDTO(string Id, string Email, long Amount, long AmountCaptured,
                                   string Status, bool Paid, bool Refunded);
