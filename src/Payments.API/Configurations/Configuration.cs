using Payments.API.Middlewares;
using Payments.Application;
using Payments.Infrastructure;
using Payments.Infrastructure.Models;

namespace Payments.API.Configurations;

public static class Configuration
{
    public static void AddModules(this WebApplicationBuilder builder)
    {
        builder.AddCustomerMiddlewares();
        builder.AddSettingsModels();
        builder.Services.AddApplication();
        builder.Services.AddInfra(builder.Configuration);
    }

    public static void AddCustomerMiddlewares(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<GlobalExceptionMiddleware>();
    }

    public static void AddSettingsModels(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection(nameof(StripeSettings)));
    }
}
