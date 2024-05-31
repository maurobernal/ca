
using ca.Application.Common.Bases;
using ca.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Runtime;
using System.Text;
using System.Text.Json;

namespace ca.Application.Common.Behaviours;
public class CacheBehaviour<TRequest, TResponse>(ICacheService _cache) : IPipelineBehavior<TRequest, TResponse>
     where TRequest : BaseDto
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response;
        
        var cachedResponse = await _cache.GetDataAsync(request.Id.ToString());
        if (cachedResponse != null)
        {
            var type = next.GetType();

            response = JsonSerializer.Deserialize<TResponse>(cachedResponse)!;
        }
        else
        {
            response = await GetResponseAndAddToCache();
        
        }


        // save in cache
        async Task<TResponse> GetResponseAndAddToCache()
        {
            response = await next();
            var slidingExpiration = TimeSpan.FromHours(2);
            var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
            var serializedData = JsonSerializer.Serialize(response, new JsonSerializerOptions() { WriteIndented = false });
            await _cache.SetDataAsync($"{request.Id}", serializedData);
            return response;
        }

        return response;
        
    }
}
