namespace ca.Infrastructure.HashiCorp;
public class EngineModels
{
    public Connection_Details connection_details { get; set; } = new();
    public string password_policy { get; set; } = string.Empty;
    public string plugin_name { get; set; }  = string.Empty;
    public string plugin_version { get; set; } = string.Empty;
}

public class Connection_Details
{
    public string backend { get; set; } = string.Empty;
    public string connection_url { get; set; } = string.Empty;
    public string max_connection_lifetime { get; set; } = string.Empty;
    public int max_idle_connections { get; set; } 
    public int max_open_connections { get; set; }
}
