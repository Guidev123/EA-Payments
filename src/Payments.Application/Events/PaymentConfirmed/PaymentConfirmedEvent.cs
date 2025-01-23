using MediatR;
using Payments.Domain.Enums;

namespace Payments.Application.Events.PaymentConfirmed;

public class PaymentConfirmedEvent : INotification
{
    public EPaymentStatus Status { get; private set; }

    public PaymentConfirmedEvent(EPaymentStatus status) => Status = status;
}
