using Payments.Application.Commands.Stripe.CreateSession;

namespace Payments.Application.Services;

public interface IStripeService
{
    Task<string?> CreateSessionAsync(CreateSessionCommand command);
}
