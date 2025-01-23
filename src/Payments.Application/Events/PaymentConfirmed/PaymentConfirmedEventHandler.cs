using MediatR;
using Payments.Domain.Enums;
using SharedLib.MessageBus;

namespace Payments.Application.Events.PaymentConfirmed;

public sealed class PaymentConfirmedEventHandler(IMessageBus bus) : INotificationHandler<PaymentConfirmedEvent>
{
    private readonly IMessageBus _bus = bus;
    public async Task Handle(PaymentConfirmedEvent notification, CancellationToken cancellationToken)
        => await _bus.PublishAsync(notification);
}
