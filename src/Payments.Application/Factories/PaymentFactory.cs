using Payments.Application.Commands.Stripe.Create;
using Payments.Application.Mappers;
using Payments.Domain.Entities;

namespace Payments.Application.Factories;

public static class PaymentFactory
{
    public static Payment CreatePayment(CreatePaymentCommand command)
    {
        var payment = command.MapToEntity();
        var transaction = command.Transaction.MapToEntity(payment.Id);
        payment.SetTransaction(transaction);
        transaction.SetProducts(command.Transaction.Products.Select(x => x.MapToEntity(transaction.Id)).ToList());

        return payment;
    }
}
