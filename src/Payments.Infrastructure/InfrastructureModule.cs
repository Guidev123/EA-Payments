using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payments.Infrastructure.Persistence;
using Payments.Infrastructure.Persistence.Configurations.Interceptors;

namespace Payments.Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
    }

    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PaymentDbContext>(options =>
        {
            options
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            .AddInterceptors(new SoftDeleteInterceptor());
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {

    }   
}
