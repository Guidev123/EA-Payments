using Payments.API.Endpoints.Stripe;

namespace Payments.API.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.WithTags("api/v1/payments")
            .WithTags("Payments")
            .RequireAuthorization()
            .MapEndpoint<CreateSessionEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
                where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
