namespace Application.Services.CachingService
{
    public interface ICacheService
    {
        Task AddCache(string key, byte[] serializeData, CancellationToken CancellationToken);
        Task RemoveCache(string key , CancellationToken cancellationToken);
        Task<byte[]> GetCache(string key , CancellationToken cancellationToken);
    }
}
