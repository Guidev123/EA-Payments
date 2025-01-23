using MediatR;
using Payments.Application.Responses;

namespace Payments.Application.Commands.Stripe.ConfirmPayment;

public record ConfirmPaymentCommand(string? WebhoockKey) : IRequest<Response<ConfirmPaymentCommand>>;
