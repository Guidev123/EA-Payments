using MediatR;
using Payments.Application.DTOs;
using Payments.Application.Responses;
using Payments.Application.Services;

namespace Payments.Application.Queries.Stripe.GetTransactionByOrderCode;

public sealed class GetTransactionByOrderCodeHandler(IStripeService stripeService)
    : IRequestHandler<GetTransactionByOrderCodeQuery, Response<List<StripeTransactionDTO>?>>
{
    private readonly IStripeService _stripeService = stripeService;
    public async Task<Response<List<StripeTransactionDTO>?>> Handle(GetTransactionByOrderCodeQuery request, CancellationToken cancellationToken)
    {
        var result = await _stripeService.GetTransactionsByOrderCodeAsync(request.Code);
        return result is null
            ? new(null, 404, "Payment not found!")
            : new(result, 200, "Transaction retrivied with success!");
    }
}
