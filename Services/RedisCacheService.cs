using System.Text.Json;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Caching.Distributed;

namespace TraineeManagement.api.Services;

public class RedisCacheService
{
    private readonly IDistributedCache _cache;

    private readonly DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
        SlidingExpiration = TimeSpan.FromMinutes(10)
    };

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetKeyAsync<T>(string key)
    {
        string? cachedData = await _cache.GetStringAsync(key);
        return (cachedData==null) ? default : JsonSerializer.Deserialize<T>(cachedData);
    }

    public async Task SetKeyAsync<T>(string key, T value)
    {
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), cacheOptions);
    }

    public async Task DeleteKeyAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}