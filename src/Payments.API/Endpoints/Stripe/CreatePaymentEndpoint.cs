using MediatR;
using Payments.Application.Commands.Stripe.Create;
using Payments.Application.Responses;

namespace Payments.API.Endpoints.Stripe;

public sealed class CreatePaymentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
                => app.MapPost("/", HandleAsync)
                    .Produces<Response<CreatePaymentResponse?>>().RequireAuthorization();

    private static async Task<IResult> HandleAsync(IMediator mediator,
                                                   CreatePaymentCommand command)
    {
        var result = await mediator.Send(command);
        return result.IsSuccess && result.Data is not null
            ? Results.Created($"/{result.Data.Session}", result)
            : Results.BadRequest(result);
    }
}
