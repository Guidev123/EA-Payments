using EA.IntegrationEvents.Integration.PaymentConfirmed;
using MediatR;
using SharedLib.MessageBus;

namespace Payments.Application.Events;

public sealed class PaymentConfirmedEventHandler(IMessageBus bus)
                  : INotificationHandler<PaymentConfirmedIntegrationEvent>
{
    private readonly IMessageBus _bus = bus;
    public async Task Handle(PaymentConfirmedIntegrationEvent notification, CancellationToken cancellationToken)
        => await _bus.PublishAsync(notification);
}
