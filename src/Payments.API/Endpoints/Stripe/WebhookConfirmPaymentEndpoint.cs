
using MediatR;
using Microsoft.Extensions.Options;
using Payments.Application.Commands.Stripe.ConfirmPayment;
using Payments.Infrastructure.Models;
using Stripe;

namespace Payments.API.Endpoints.Stripe;

public class WebhookConfirmPaymentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/webhook-confirm-payment", HandleAsync);

    private static async Task<IResult> HandleAsync(IMediator mediator, IOptions<StripeSettings> stripeSettings)
    {
        var result = await mediator.Send(new ConfirmPaymentCommand(stripeSettings.Value.WebhookSecret));
        return result.IsSuccess 
            ? TypedResults.NoContent()
            : TypedResults.BadRequest();
    }
}