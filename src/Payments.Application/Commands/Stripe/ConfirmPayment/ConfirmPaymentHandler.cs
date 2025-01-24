using EA.IntegrationEvents.Integration.PaymentConfirmed;
using MediatR;
using Microsoft.AspNetCore.Http;
using Payments.Application.Responses;
using Payments.Application.Services;
using Payments.Domain.Enums;
using Payments.Domain.Repositories;
using Stripe;

namespace Payments.Application.Commands.Stripe.ConfirmPayment;

public sealed class ConfirmPaymentHandler(IHttpContextAccessor httpContextAccessor,
                                          IMediator mediator,
                                          IPaymentRepository paymentRepository,
                                          IStripeService stripeService,
                                          IUnitOfWork unitOfWork)
                  : IRequestHandler<ConfirmPaymentCommand, Response<ConfirmPaymentCommand>>
{
    private readonly IMediator _mediator = mediator;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IStripeService _stripeService = stripeService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

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

            if (stripeEvent.Type.Equals("charge.succeeded", StringComparison.OrdinalIgnoreCase)
                && stripeEvent.Data.Object is Charge charge
                && charge.Metadata.TryGetValue("order", out var orderNumber))
            {
                var payment = await _paymentRepository.GetByOrderCodeAsync(orderNumber!);
                if (payment is null) return new(null, 404);

                payment.SetAsPaid();

                var transaction = await _stripeService.GetTransactionsByOrderCodeAsync(orderNumber!);
                payment.Transaction.SetExternalReference(transaction[0].Id);

                _paymentRepository.Update(payment);
                await _unitOfWork.CompleteAsync();
                
                await _mediator.Publish(new PaymentConfirmedIntegrationEvent(true, orderNumber), cancellationToken);

                return new(null, 204);
            }

            return new(null, 400);
        }
        catch
        {
            return new(null, 500);
        }
    }
}
