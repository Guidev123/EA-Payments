using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLib.MessageBus;

namespace Payments.Application;

public static class ApplicationModule
{
    public const string EVENT_TYPE_STRIPE = "payment_intent.succeeded";
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHandlers();
        services.AddMessageBusConfiguration(configuration);
    }

    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(ApplicationModule).Assembly));
    }

    public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
}
