using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Commands.Stripe.CreateSession;
using Payments.Application.Responses;

namespace Payments.API.Endpoints.Stripe;

public sealed class CreateSessionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
                => app.MapPost("/session", HandleAsync)
                    .Produces<Response<string?>>();

    private static async Task<IResult> HandleAsync([FromServices] IMediator mediator, [FromBody] CreateSessionCommand command)
    {
        var result = await mediator.Send(command);
        return result.IsSuccess && result.Data is not null
            ? Results.Created($"/{result.Data.seesion}", result.Data)
            : Results.BadRequest(result);
    }
}
