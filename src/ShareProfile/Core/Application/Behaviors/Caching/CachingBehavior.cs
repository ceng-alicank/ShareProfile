using Application.Services.CachingService;
using MediatR;
using Newtonsoft.Json;
using System.Text;

namespace Application.Behaviors.Caching
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICachableRequest
    {
        private readonly ICacheService _cacheService;
        public CachingBehavior(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
                                            RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            if (request.BypassCache) return await next();

            byte[]? cachedResponse = await _cacheService.GetCache(request.CacheKey, cancellationToken);
            if (cachedResponse != null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
            }
            else
            {
                response = await next();
                byte[] serializeData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                await _cacheService.AddCache( request.CacheKey, serializeData , cancellationToken);
            }

            return response;
        }
    }
}
