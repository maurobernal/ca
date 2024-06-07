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
        var vaultClient = sp.GetRequiredService<VaultCustom>();
        var vaultInfisical = sp.GetRequiredService<InfisicalCustom>();

        var webui_host = vaultInfisical.GetKey("WEBUI_HOST");
        var webui_user = vaultInfisical.GetKey  ("WEBUI_USER");
        Console.WriteLine($"host:{webui_host}  user:{webui_user}");

        Console.WriteLine($"{vaultInfisical.GetConnectionsKeys()}");

        Console.WriteLine(vaultClient.GetConnectionsKeys());

        var connectionString = vaultClient.GetKey("Motor1");

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

        var config = configuration.GetSection("Config:Vault").Value ?? string.Empty;
        if (config == "Hashicorp")
        {
            services.AddSingleton<IVault>((sp) => sp.GetRequiredService<VaultCustom>());
        }
        else
        {
            services.AddSingleton<IVault>((sp) => sp.GetRequiredService<InfisicalCustom>());
        }
        

        

        return services;
    }
}
