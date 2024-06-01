using ca.Application.Common.Interfaces;
using ca.Domain.Constants;
using ca.Infrastructure.Data;
using ca.Infrastructure.Data.Interceptors;
using ca.Infrastructure.Identity;
using ca.Infrastructure.Vaults.HashiCorp;
using ca.Infrastructure.Vaults.Infisical;
using Infisical.Sdk;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Vault;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {


        var sp = services.BuildServiceProvider();
        var vaultClient = sp.GetRequiredService<VaultClient>();
        var vaultInfisical = sp.GetRequiredService<InfisicalClient>();

        var webui_host = vaultInfisical.GetSecrets("WEBUI_HOST");
        var webui_user = vaultInfisical.GetSecrets("WEBUI_USER");
        Console.WriteLine($"host:{webui_host}  user:{webui_user}");

        Console.WriteLine($"{vaultInfisical.GetListSecret()}");

        Console.WriteLine(vaultClient.GetConnectionsKeys());

        var connectionString = vaultClient.GetConnectionString("Motor1");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        return services;
    }
}
