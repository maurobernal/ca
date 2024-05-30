namespace ca.Infrastructure.HashicorpVault;

public class ConnectionDetails
{
    public string backend { get; set; } = string.Empty;
    public string connection_url { get; set; } = string.Empty;
    public string max_connection_lifetime { get; set; } = string.Empty;
    public int max_idle_connections { get; set; }
    public int max_open_connections { get; set; }
}

public class VaultMsSql
{
    public ConnectionDetails connection_details { get; set; } = new ConnectionDetails();
    public string password_policy { get; set; } = string.Empty;
    public string plugin_name { get; set; } = string.Empty;
    public string plugin_version { get; set; } = string.Empty;

}
