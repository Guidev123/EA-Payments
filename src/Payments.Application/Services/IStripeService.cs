using Payments.Application.Commands.Stripe.CreateSession;
using Payments.Application.DTOs;

namespace Payments.Application.Services;

public interface IStripeService
{
    Task<string?> CreateSessionAsync(CreateSessionCommand command);
    Task<List<StripeTransactionDTO>> GetTransactionsByOrderCodeAsync(string number);
}
