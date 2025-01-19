using Microsoft.AspNetCore.Authentication.JwtBearer;
using NetDevPack.Security.JwtExtensions;
using Payments.API.Endpoints;

namespace Payments.API.Configurations;

public static class SecurityConfiguration
{
    public static void AddJwtConfiguration(this IServiceCollection services,
            IConfiguration configuration)

    {
        var appSettingsSection = configuration.GetSection(nameof(JwksSettings));
        services.Configure<JwksSettings>(appSettingsSection);

        var appSettings = appSettingsSection.Get<JwksSettings>() ?? new();
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.SetJwksOptions(new JwkOptions(appSettings.JwksEndpoint));
        });
        services.AddAuthorization();
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseSwaggerConfig();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapEndpoints();
    }

    public class JwksSettings
    {
        public string JwksEndpoint { get; set; } = string.Empty;
    }
}
