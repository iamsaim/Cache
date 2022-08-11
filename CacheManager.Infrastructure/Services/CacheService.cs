using CacheManager.Application.Common.Contants;
using CacheManager.Application.Common.Contracts;
using CacheManager.Application.Common.Dto.Response;
using CacheManager.Application.Common.Entity;
using Microsoft.Extensions.Logging;

namespace CacheManager.Infrastructure.Services;

public class CacheService: ICacheService
{
    private readonly ILogger<CacheService> _logger;
    private readonly IDictionary<Guid, CacheItem> _cache;

    public CacheService(ILogger<CacheService> logger)
    {
        _logger = logger;
        _cache = new Dictionary<Guid, CacheItem>(5);
    }

    public bool AddItem(Guid Key,CacheItem item)
    {
        if (_cache.ContainsKey(Key))
        {
            _cache.Remove(Key);
        }

        MaintainCacheSize();
        _cache.Add(Key, item);
        return true;
    }

    public IDictionary<Guid, CacheItem> GetAllItems()
    {
        return _cache;
    }

    public CacheItem GetItem(Guid key)
    {
        var item = _cache.FirstOrDefault(x => x.Key == key);
        UpdateLastUsed(key, item.Value);
        return item.Value;
    }

    private void MaintainCacheSize()
    {
        if (_cache.Count == Contants.MAX_CACHE_COUNT)
        {
            var item = _cache.OrderBy(x => x.Value.LastUsed).FirstOrDefault();
            _cache.Remove(item);
            _logger.LogInformation("Item removed from cache with key: {@key}", item.Key);
        }
    }

    private void UpdateLastUsed(Guid Key,CacheItem item)
    {
        item.LastUsed = DateTimeOffset.UtcNow;
        _cache[Key] = item;
    }
}