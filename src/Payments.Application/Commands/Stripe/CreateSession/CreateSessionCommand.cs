using MediatR;
using Payments.Application.DTOs;
using Payments.Application.Responses;
using Payments.Domain.Enums;

namespace Payments.Application.Commands.Stripe.CreateSession;

public class CreateSessionCommand : IRequest<Response<CreateSessionResponse>>
{
    public CreateSessionCommand(string email, string orderCode,
                                decimal total, EPaymentGateway gateway,
                                 ShoppingCartDTO shoppingCart)
    {
        Email = email;
        OrderCode = orderCode;
        Total = total;
        Gateway = gateway;
        ShoppingCart = shoppingCart;
    }

    public Guid? CustomerId { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string OrderCode { get; private set; } = string.Empty;
    public decimal Total { get; private set; }
    public EPaymentGateway Gateway { get; private set; }
    public ShoppingCartDTO ShoppingCart { get; private set; } = null!;
    public void SetCustomerId(Guid customerId) => CustomerId = customerId;
}
