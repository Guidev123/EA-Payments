using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        var appSettings = appSettingsSection.Get<JwksSettings>() ?? new(string.Empty);
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
    }

    public static void UseSecurity(this IApplicationBuilder app)
    {
        app.UseSwaggerConfig();

        app.UseHttpsRedirection();

        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseJwksDiscovery();
    }

    public class JwksSettings(string JwksEndpoint)
    {
        public string JwksEndpoint { get; set; } = JwksEndpoint;
    }
}
