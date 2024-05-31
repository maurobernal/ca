namespace ca.Application.Common.Interfaces;
public interface ICacheService
{
    //Get
    public Task<T> GetDataAsync<T>(string key) where T : new();
    
    public Task<string?> GetDataAsync(string key) ;

    //Set
    public Task<bool> SetDataAsync<T>(string key, T value);
    public Task<bool> SetDataAsync<T>(string key, T value, TimeSpan expirationTime);

    //Remove
    public Task<bool> RemoveDataAsync(string key);


}
