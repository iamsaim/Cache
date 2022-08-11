using CacheManager.Application.Common.Dto;
using CacheManager.Application.Common.Dto.Response;
using CacheManager.Application.Common.Entity;

namespace CacheManager.Application.Common.Contracts;

public interface ICacheService
{
    public bool AddItem(Guid Key,CacheItem item);
    public CacheItem GetItem(Guid key);
    public IDictionary<Guid,CacheItem> GetAllItems();
}