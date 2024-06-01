
using Infisical.Sdk;

namespace ca.Infrastructure.Vaults.Infisical;
public static class Infisical
{
    public static string GetSecrets(
        this InfisicalClient client, 
        string name, 
        string environment ="dev")
    {
        var options = new GetSecretOptions();
        options.ProjectId = string.Empty;
        options.Environment = environment;
        options.SecretName = name;

        var res = client.GetSecret(options);

        return res.SecretValue ?? string.Empty;
    }

    public static string GetListSecret(
        this InfisicalClient client,
        string environment = "dev")
    {
        var options = new ListSecretsOptions();
        options.ProjectId = string.Empty;
        options.Environment = environment;

        var res = client.ListSecrets(options);
        var secretsKey = string.Empty;
        foreach (var item in res)
        {
            secretsKey += item.SecretKey + ",";
        }
        return secretsKey ?? string.Empty;
    }


}
