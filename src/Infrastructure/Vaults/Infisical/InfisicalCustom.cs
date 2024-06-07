
using System;
using ca.Application.Common.Interfaces;
using Infisical.Sdk;
using Microsoft.Extensions.Hosting;

namespace ca.Infrastructure.Vaults.Infisical;
public class InfisicalCustom : IVault
{
    private readonly InfisicalClient _client;
    private readonly string _environment = "dev";
    public InfisicalCustom(string address, string token, string environment)
    {
        var settings  = new ClientSettings();
        settings.AccessToken = token;
        settings.SiteUrl = address;
        _environment = environment;
        _client = new InfisicalClient(settings);
    }


    public string GetConnectionsKeys()
    {
        
        var options = new ListSecretsOptions();
        options.ProjectId = string.Empty;
        options.Environment = _environment;

        var res = _client.ListSecrets(options);
        var secretsKey = string.Empty;
        foreach (var item in res)
        {
            secretsKey += item.SecretKey + ",";
        }
        return secretsKey ?? string.Empty;
    }

    public string GetKey(string name)
    {
        var options = new GetSecretOptions();
        options.ProjectId = string.Empty;
        options.Environment = _environment;
        options.SecretName = name;

        var res = _client.GetSecret(options);

        return res.SecretValue ?? string.Empty;
    }
}
