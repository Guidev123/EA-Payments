using Microsoft.AspNetCore.Authentication.JwtBearer;
using Payments.API.Endpoints;
using SharedLib.Tokens.AspNet;
using SharedLib.Tokens.Extensions;

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
        services.AddCors();
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseSwaggerConfig();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("Total");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapEndpoints();

        app.UseJwksDiscovery();
    }

    public class JwksSettings
    {
        public string JwksEndpoint { get; set; } = string.Empty;
    }
}
