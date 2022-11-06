using Application.Services.CachingService;
using MediatR;

namespace Application.Behaviors.Caching
{
    public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICacheRemoverRequest
    {
        private readonly ICacheService _cacheService;
        public CacheRemovingBehavior(
                ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
                                            RequestHandlerDelegate<TResponse> next)
        {
            if (request.BypassCache) return await next();
            await _cacheService.RemoveCache(request.CacheKey, cancellationToken);
            return await next();
        }
    }
}
