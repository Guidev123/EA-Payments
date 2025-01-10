using MediatR;
using Payments.Application.DTOs;
using Payments.Application.Responses;
using Payments.Domain.Enums;

namespace Payments.Application.Commands.Stripe.CreateSession;

public class CreateSessionCommand : IRequest<Response<CreateSessionResponse>>
{
    public Guid CustomerId { get; private set; }
    public string Document { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string OrderCode { get; private set; } = string.Empty;
    public decimal Total { get; private set; }
    public EPaymentType Type { get; private set; }
    public EPaymentGateway Gateway { get; private set; }
    public EPaymentStatus Status { get; private set; }
    public TransactionDTO Transaction { get; private set; } = null!;
    public void SetCustomerId(Guid customerId) => CustomerId = customerId;
}
