using MediatR;
using Payments.Application.Queries.Stripe.GetTransactionByOrderCode;
using Payments.Application.Responses;

namespace Payments.API.Endpoints.Stripe;

public class GetTransactionsByOrderCodeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{number}", HandleAsync)
            .Produces<Response<dynamic>>().RequireAuthorization();

    private static async Task<IResult> HandleAsync(IMediator mediator,
                                                   string number)
    {
        var result = await mediator.Send(new GetTransactionByOrderCodeQuery(number));
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
