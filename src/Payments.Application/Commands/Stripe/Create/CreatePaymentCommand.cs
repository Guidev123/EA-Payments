using MediatR;
using Payments.Application.DTOs;
using Payments.Application.Responses;
using Payments.Domain.Enums;

namespace Payments.Application.Commands.Stripe.Create;

public class CreatePaymentCommand : IRequest<Response<CreatePaymentResponse>>
{
    public CreatePaymentCommand(string orderCode,
                                decimal total, EPaymentGateway gateway,
                                EPaymentType paymentType, TransactionDTO transaction)
    {
        OrderCode = orderCode;
        Total = total;
        Gateway = gateway;
        Transaction = transaction;
        PaymentType = paymentType;
    }

    public Guid CustomerId { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string OrderCode { get; private set; } = string.Empty;
    public decimal Total { get; private set; }
    public EPaymentGateway Gateway { get; private set; }
    public EPaymentType PaymentType { get; private set; }
    public TransactionDTO Transaction { get; private set; } = null!;
    public void SetCustomerCredentials(Guid customerId, string email)
    {
        CustomerId = customerId;
        Email = email;
    }
}
