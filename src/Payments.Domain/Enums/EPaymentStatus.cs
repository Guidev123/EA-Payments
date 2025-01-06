namespace Payments.Domain.Enums;

public enum EPaymentStatus
{
    Authorized,
    Paid,
    Refunded,
    Denied,
    WaitingPayment
}