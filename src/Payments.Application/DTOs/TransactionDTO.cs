namespace Payments.Application.DTOs;

public record TransactionDTO(decimal Amount)
{
    public List<ProductDTO> Products { get; set; } = [];
}
