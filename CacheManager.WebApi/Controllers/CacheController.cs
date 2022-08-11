using CacheManager.Application.Common.Contracts;
using CacheManager.Application.Common.Dto;
using CacheManager.Application.Common.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CacheManager.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/cache")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [Route("All")]
        [HttpGet]
        [ProducesResponseType(typeof(IDictionary<Guid,CacheItem>), StatusCodes.Status200OK)]
        public  IActionResult GetAllItems()
        {
            var cache =  _cacheService.GetAllItems();
            return Ok(cache);
        }

        [Route("{key}")]
        [HttpGet]
        [ProducesResponseType(typeof(CacheItem), StatusCodes.Status200OK)]
        public IActionResult GetItem(Guid key)
        {
            var item = _cacheService.GetItem(key);
            return Ok(item);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddItem(CacheManagerDTO dto)
        {
            var response =  _cacheService.AddItem(dto.Key, 
                new CacheItem { 
                    Value = dto.Value,
                    LastUsed = DateTimeOffset.UtcNow
            });
            return Ok(response);
        }

    }
}
