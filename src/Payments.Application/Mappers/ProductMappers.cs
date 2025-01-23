using Payments.Application.DTOs;
using Payments.Domain.Entities;

namespace Payments.Application.Mappers;

public static class ProductMappers
{
    public static Product MapToEntity(this ProductDTO dto, Guid transactionId) =>
        new(dto.Name, dto.Description, dto.Price, transactionId);
}
