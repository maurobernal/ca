using ca.Application.Common.Interfaces;
using Vault;
using Vault.Client;
using Vault.Model;

namespace ca.Infrastructure.Vaults.HashiCorp;
public class VaultCustom: IVault
{

    private readonly VaultClient vaultClient;

    public VaultCustom(string address, string token)
    {
        vaultClient = new VaultClient(new VaultConfiguration(address));
        vaultClient.SetToken(token);
    }
    public string GetConnectionsKeys()
    {
        VaultResponse<StandardListResponse> resp = new();
        try
        {
           resp = vaultClient.Secrets.DatabaseListConnections();
        }
        catch (Exception)
        {

            resp.Data = new();
        }
        
           return resp.Data.ToJson();
    }

    public string GetKey(string name)
    {
        var resp = vaultClient.Secrets.DatabaseReadConnectionConfiguration(name).Data.ToString() ?? string.Empty;
        var model = System.Text.Json.JsonSerializer.Deserialize<EngineModels>(resp) ?? new();
        return System.Web.HttpUtility.UrlDecode(model.connection_details.connection_url);
    }

}
