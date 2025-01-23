//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Payments.Application.Responses;
//using Stripe;

//namespace Payments.Application.Commands.Stripe.ConfirmPayment;

//public sealed class ConfirmPaymentHandler(HttpContext context)
//                  : IRequestHandler<ConfirmPaymentCommand, Response<ConfirmPaymentCommand>>
//{
//    public async Task<Response<ConfirmPaymentCommand>> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
//    {
//        var json = await new StreamReader(context.Request.Body).ReadToEndAsync(cancellationToken);

//        try
//        {
//            var stripeEvent = EventUtility.ConstructEvent(
//                json,
//                context.Request.Headers["Stripe-Signature"],
//                request.WebhoockKey
//            );

//            if (stripeEvent.Type == ApplicationModule.EVENT_TYPE_STRIPE)
//            {

//            }

//            else
//            {

//            }

//            return new(null, 204);
//        }
//        catch
//        {
//            return new(null, 500);
//        }
//    }
//}
