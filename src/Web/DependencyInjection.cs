using Azure.Identity;
using ca.Application.Common.Interfaces;
using ca.Infrastructure.Data;
using ca.Web.Services;
using Infisical.Sdk;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;
using Vault;
using Vault.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        
        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "ca API";

            // Add JWT
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });
            

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });
        
        return services;
    }

    public static IServiceCollection AddVault(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton((ServiceProvider) => 
        {

            string address = configuration.GetConnectionString("Vault")?? string.Empty;
            VaultConfiguration config = new(address);
            VaultClient client = new VaultClient(config);
            client.SetToken("myroot");
            return client;        
        });


        return services;
    }

    public static IServiceCollection AddInfisical(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton((ServiceProvider) =>
        {

            string address = configuration.GetConnectionString("Infisical") ?? string.Empty;
            string token = "st.40ae2c1b-ccb2-408d-b48a-330e04569937.56f81852cc96c0e34fc8961b32c0c823.7a111e48e722bd3164d2588cdf710bde" ?? string.Empty;
            var settings = new ClientSettings();
            settings.AccessToken = token;
            settings.SiteUrl = address;
            var cliente = new InfisicalClient(settings);

            return cliente;
        });


        return services;
    }
}
