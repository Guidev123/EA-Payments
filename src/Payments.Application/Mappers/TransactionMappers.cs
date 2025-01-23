using Payments.Application.DTOs;
using Payments.Domain.Entities;

namespace Payments.Application.Mappers;

public static class TransactionMappers
{
    public static Transaction MapToEntity(this TransactionDTO dto) =>
        new(dto.PaymentId, dto.Amount, dto.ShoppingCart.MapToEntity());
}
