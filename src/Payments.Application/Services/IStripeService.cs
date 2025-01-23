using Payments.Application.Commands.Stripe.Create;
using Payments.Application.DTOs;

namespace Payments.Application.Services;

public interface IStripeService
{
    Task<string?> CreateSessionAsync(CreatePaymentCommand command);
    Task<List<StripeTransactionDTO>> GetTransactionsByOrderCodeAsync(string number);
}
