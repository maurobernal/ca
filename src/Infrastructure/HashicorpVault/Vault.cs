using Vault;
using Vault.Client;
using Vault.Model;

namespace ca.Infrastructure.HashicorpVault;
public static class Vault
{

    public static string GetMsSQLServer(this VaultClient vaultClient, string name)
    {
        var secretConnection = vaultClient.Secrets.DatabaseReadConnectionConfiguration(name);
        string jsonString = secretConnection.Data.ToString() ?? string.Empty;
        VaultMsSql secretConnection2 = System.Text.Json.JsonSerializer.Deserialize<VaultMsSql>(jsonString) ?? new();
        return System.Web.HttpUtility.UrlDecode(secretConnection2.connection_details.connection_url);

    }

    public static string GetDatabases(this VaultClient vaultClient)
    {
        var resp = vaultClient.Secrets.DatabaseListConnections();
        return resp.Data.ToJson();
    }

    public static string GetSecret(this VaultClient vaultClient, string path, string mount = "secret")
    {
        VaultResponse<KvV2ReadResponse> resp = vaultClient.Secrets.KvV2Read(path, mount);
        return resp.Data.ToJson();
    }

    public static string WriteSecret(this VaultClient vaultClient, string path, string key, string value, string mount = "secret")
    {
        var secretData = new Dictionary<string, string> { { key, value } };
        var kvRequestData = new KvV2WriteRequest(secretData);
        var resp = vaultClient.Secrets.KvV2Write(path, kvRequestData, mount);
        return resp.Data.ToJson();

    }
}
