using Payments.Application.Commands.Stripe.Create;
using Payments.Domain.Entities;

namespace Payments.Application.Mappers;
public static class PaymentMappers
{
    public static Payment MapToEntity(this CreatePaymentCommand command) =>
        new(command.CustomerId, command.Email, command.OrderCode,
            command.Total, command.PaymentType, command.Gateway,
            command.Transaction.MapToEntity());
}
