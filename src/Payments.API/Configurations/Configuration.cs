using Payments.Infrastructure;

namespace Payments.API.Configurations;

public static class Configuration
{
    public static void AddModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddInfra(builder.Configuration);
    }
}
