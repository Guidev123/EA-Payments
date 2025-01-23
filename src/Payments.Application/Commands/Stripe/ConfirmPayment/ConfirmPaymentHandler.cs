using MediatR;
using Microsoft.AspNetCore.Http;
using Payments.Application.Events.PaymentConfirmed;
using Payments.Application.Responses;
using Payments.Domain.Enums;
using Stripe;

namespace Payments.Application.Commands.Stripe.ConfirmPayment;

public sealed class ConfirmPaymentHandler(IHttpContextAccessor httpContextAccessor, IMediator mediator)
                  : IRequestHandler<ConfirmPaymentCommand, Response<ConfirmPaymentCommand>>
{
    private readonly IMediator _mediator = mediator;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Response<ConfirmPaymentCommand>> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
    {
        var context = _httpContextAccessor.HttpContext;

        if (context is null)
            return new(null, 500);

        var json = await new StreamReader(context.Request.Body).ReadToEndAsync(cancellationToken);

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                context.Request.Headers["Stripe-Signature"],
                request.WebhoockKey,
                throwOnApiVersionMismatch: false
            );

            var paymentStatus = stripeEvent.Type;

            if (paymentStatus.Equals("charge.succeeded", StringComparison.OrdinalIgnoreCase))
                await _mediator.Publish(new PaymentConfirmedEvent(EPaymentStatus.Paid), cancellationToken);

            else return new(null, 400);

            return new(null, 204);
        }
        catch
        {
            return new(null, 500);
        }
    }
}
