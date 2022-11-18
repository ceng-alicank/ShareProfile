using Application.Services.CachingService;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Persistence.CachingService
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<RedisCacheService> _logger;

        private readonly CacheSettings _cacheSettings;

        public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger, IConfiguration configuration)
        {
            _cache = cache;
            _logger = logger;
            _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>(); ;
        }
        public async Task SetCache(string key, byte[] serializeData, CancellationToken cancellationToken)
        {
            var slidingExpiration = TimeSpan.FromDays(_cacheSettings.SlidingExpiration);
            DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = slidingExpiration };
            await _cache.SetAsync(key, serializeData, cacheOptions, cancellationToken);
            _logger.LogInformation($"Added to Cache -> {key}");
        }

        public async Task<byte[]> GetCache(string key, CancellationToken cancellationToken)
        {
            byte[]? cachedResponse = await _cache.GetAsync(key, cancellationToken);
            _logger.LogInformation($"Fetched from Cache -> {key}");
            return cachedResponse;
        }

        public async Task RemoveCache(string key , CancellationToken cancellationToken)
        {
            await _cache.RemoveAsync(key, cancellationToken);
            _logger.LogInformation($"Removed Cache -> {key}");
        }
    }
}
