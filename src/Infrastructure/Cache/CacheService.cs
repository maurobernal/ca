using ca.Application.Common.Exceptions;
using ca.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;
namespace ca.Infrastructure.Cache;
public class CacheService : ICacheService
{
    private readonly IDatabaseAsync _cache;
    private readonly IConnectionMultiplexer _redis;
    public CacheService(IConnectionMultiplexer redis)
    {
        try
        {
            _redis= redis;
            _cache = redis.GetDatabase();

        }
        catch (Exception ex)
        {

            throw new ApiCacheException(ex.Message);
        }
        
    }

    public async Task<T> GetDataAsync<T>(string key) where T : new()
        {
        try
        {
            var valueT = await _cache.StringGetAsync(key);

            if (!string.IsNullOrEmpty(valueT))
            {
                var resDes =  JsonSerializer.Deserialize<T>(valueT.ToString());
                if(resDes != null)
                return resDes;
            }

            return new T(); 
        }
        catch (Exception ex)
        {
            throw new ApiCacheException(ex.Message);
        }
    }

    public async Task<string?> GetDataAsync(string key) 
    {
        try
        {
            return await _cache.StringGetAsync(key);

        }
        catch (Exception ex)
        {
            throw new ApiCacheException(ex.Message);
        }
    }

    public async Task<bool> RemoveDataAsync(string key)
    {
        try
        {
            bool existeKey = await _cache.KeyExistsAsync(key);
            if (existeKey)
            {
                return await _cache.KeyDeleteAsync(key);
            }
            return false;
        }
        catch (Exception ex)
        {
            throw new ApiCacheException(ex.Message);
        }
    }

    public async Task<bool> SetDataAsync<T>(string key, T value)
    {
        try
        {

        var expirationTime = DateTime.Now.AddDays(1).TimeOfDay;

        bool isSet = await _cache.StringSetAsync(
            key, 
            JsonSerializer.Serialize(value), expirationTime
            );

        return isSet;
        }
        catch (Exception ex)
        {
            throw new ApiCacheException(ex.Message);
        }
    }

    public async Task<bool> SetDataAsync<T>(string key, T value, TimeSpan expirationTime)
    {
        try
        {
            bool isSet = await _cache.StringSetAsync(
                key,
                JsonSerializer.Serialize(value), expirationTime
                );

            return isSet;
        }
        catch (Exception ex)
        {
            throw new ApiCacheException(ex.Message);
        }
    }
}
