using Payments.Domain.DomainObjects;
using Payments.Domain.Enums;
using Payments.Domain.ValueObjects;

namespace Payments.Domain.Entities;

public class Payment : Entity, IAggregateRoot
{
    public Payment(Guid customerId, string email,
                   string orderCode, decimal total, EPaymentType paymentType,
                   EPaymentGateway paymentGateway, Transaction transaction)
    {
        CustomerId = customerId;
        Email = new Email(email);
        OrderCode = orderCode;
        Total = total;
        Type = paymentType;
        Gateway = paymentGateway;
        Status = EPaymentStatus.WaitingPayment;
        Transaction = transaction;
    }
    public Guid CustomerId { get; private set; }
    public Email Email { get; private set; } = null!;
    public string OrderCode { get; private set; } = string.Empty;
    public decimal Total { get; private set; }
    public EPaymentType Type { get; private set; }
    public EPaymentGateway Gateway { get; private set; }
    public EPaymentStatus Status { get; private set; }
    public Transaction Transaction { get; private set; } = null!;
    protected Payment() { }
}
