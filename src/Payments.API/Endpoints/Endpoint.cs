using Payments.API.Endpoints.Stripe;

namespace Payments.API.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("api/v1/stripe")
            .WithTags("Stripe")
            .MapEndpoint<WebhookConfirmPaymentEndpoint>()
            .MapEndpoint<CreateSessionEndpoint>()
            .MapEndpoint<GetTransactionsByOrderCodeEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
                where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
