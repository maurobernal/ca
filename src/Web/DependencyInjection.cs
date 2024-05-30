using ca.Application.Common.Interfaces;
using ca.Infrastructure.Data;
using ca.Web.Services;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;
using Vault.Client;
using Vault;

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

    public static IServiceCollection AddHashiVaultServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton((serviceProvider) =>
        {
            //Vault
            VaultConfiguration configVault = new VaultConfiguration(configuration.GetConnectionString("Vault"));
            VaultClient vaultClient = new VaultClient(configVault);
            vaultClient.SetToken("00000000-0000-0000-0000-000000000000");
            return vaultClient;
        });

        return services;

    }


}
