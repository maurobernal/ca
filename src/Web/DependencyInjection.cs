using Azure.Identity;
using ca.Application.Common.Interfaces;
using ca.Infrastructure.Data;
using ca.Web.Services;
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
}
