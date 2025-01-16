using Microsoft.Extensions.DependencyInjection;

namespace Payments.Application;

public static class ApplicationModule
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddHandlers();
    }

    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(ApplicationModule).Assembly));
    }
}
