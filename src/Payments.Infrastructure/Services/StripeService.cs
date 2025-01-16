using Microsoft.Extensions.Options;
using Payments.Application.Commands.Stripe.CreateSession;
using Payments.Application.Services;
using Payments.Infrastructure.Models;
using Stripe;
using Stripe.Checkout;

namespace Payments.Infrastructure.Services;

public sealed class StripeService(IOptions<StripeSettings> stripeSettings) : IStripeService
{
    private readonly StripeSettings _stripeSettings = stripeSettings.Value;
    public async Task<string?> CreateSessionAsync(CreateSessionCommand command)
    {
        var client = new StripeClient(_stripeSettings.ApiKey);

        var options = new SessionCreateOptions
        {
            CustomerEmail = command.Email,
            ClientReferenceId = command.CustomerId.ToString(),

            PaymentIntentData = new SessionPaymentIntentDataOptions
            {
                Metadata = new Dictionary<string, string>
                {
                            { "order", command.OrderCode }
                        }
            },
            PaymentMethodTypes = [_stripeSettings.PaymentMethodTypes],
            LineItems =
            [
                new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "BRL",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = command.Transaction.ShoppingCart.Name,
                                    Description = command.Transaction.ShoppingCart.Description
                                },
                                UnitAmount = (int)command.Total,
                            },
                            Quantity = 1
                        }
            ],
            Mode = _stripeSettings.StripeMode,
            SuccessUrl = $"{_stripeSettings.FrontendUrl}/orders/{command.OrderCode}/confirm",
            CancelUrl = $"{_stripeSettings.FrontendUrl}/orders/{command.OrderCode}/cancel",
        };

        var service = new SessionService(client);
        var session = await service.CreateAsync(options);
        session.Url = session.Url;
        return session.Id;
    }
}
