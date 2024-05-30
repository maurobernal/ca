using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Vault;

namespace ca.Infrastructure;
public static class ConfigurationExtensions
{
    public static string? GetConnectionString(this IConfiguration configuration, string name, VaultClient vaultClient, bool vault)
    {
        return configuration?.GetSection("ConnectionStrings")[name];
    }


}
