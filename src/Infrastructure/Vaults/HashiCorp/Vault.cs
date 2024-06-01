using Vault;
using Vault.Client;
using Vault.Model;

namespace ca.Infrastructure.Vaults.HashiCorp;
public static class Vault
{

    public static string GetConnectionsKeys(this VaultClient vaultClient)
    {

        var resp = vaultClient.Secrets.DatabaseListConnections();

        return resp.Data.ToJson();
    }

    public static string GetConnectionString(this VaultClient vaultClient, string name)
    {
        var resp = vaultClient.Secrets.DatabaseReadConnectionConfiguration(name).Data.ToString() ?? string.Empty;
        var model = System.Text.Json.JsonSerializer.Deserialize<EngineModels>(resp) ?? new();
        return System.Web.HttpUtility.UrlDecode(model.connection_details.connection_url);
    }


}
