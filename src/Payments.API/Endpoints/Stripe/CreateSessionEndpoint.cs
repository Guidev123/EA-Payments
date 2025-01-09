using Payments.Application.Responses;

namespace Payments.API.Endpoints.Stripe;

public sealed class CreateSessionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
                => app.MapPost("/session", HandleAsync)
                    .Produces<Response<string?>>();

    private static async Task<IResult> HandleAsync()
    {
        return TypedResults.Ok("Hello World");
    }
}
