using Azure.Identity;
using ca.Application.Common.Interfaces;
using ca.Infrastructure.Data;
using ca.Infrastructure.Vaults.HashiCorp;
using ca.Infrastructure.Vaults.Infisical;
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
            string token = "myroot";
            VaultCustom client = new  VaultCustom(address, token);          
            return client;        
        });
        return services;
    }

    public static IServiceCollection AddInfisical(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton((ServiceProvider) =>
        {

            string address = configuration.GetConnectionString("Infisical") ?? string.Empty;
            string token = "st.cfcd5319-d26f-4f98-9804-b5f1559eec7a.03718c2fc787296a8ee3e37366fddf29.309667a20fd1d1134d249d52dbde797e" ?? string.Empty;
            var cliente = new InfisicalCustom(address, token, "dev");
            return cliente;
        });
        return services;
    }
}
