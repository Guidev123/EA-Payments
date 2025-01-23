using Payments.Application.DTOs;
using Payments.Domain.Entities;

namespace Payments.Application.Mappers;

public static class ShoppingCartMappers
{
    public static ShoppingCart MapToEntity(this ShoppingCartDTO dto) =>
        new(dto.Name, dto.Description, dto.Price, dto.TransactionId);
}
